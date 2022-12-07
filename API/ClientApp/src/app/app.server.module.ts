import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
//import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader'; ModuleMapLoaderModule
import { AppComponent } from './app.component';
import { AppModule } from './app.module';

@NgModule({
    imports: [AppModule, ServerModule],
    bootstrap: [AppComponent]
})
export class AppServerModule { }
