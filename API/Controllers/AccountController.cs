using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using API.Helpers;
using API.Entities;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, IMapper mapper, ITokenService token)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = token;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO newUser)
        {
            if (await Exists(newUser.Username))
                return BadRequest("UserName Exists");

            var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = newUser.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newUser.Username)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
           await _context.SaveChangesAsync();

            return new UserDTO { 
                Username = newUser.Username,
                Token = _tokenService.CreateToken(user)
            };



        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");


            var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.PasswordHash[i])
                    return Unauthorized("password is invalid");
            }

            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };    
        }
        private async Task<bool> Exists(string username) {

            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    
    }
    


        


    
}
