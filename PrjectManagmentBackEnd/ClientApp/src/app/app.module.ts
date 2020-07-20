import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { ProjectComponent } from './UsersProject/usersproject.component';
import { UserTaskComponent } from './UserTask/usertask.component';
import { UserComponent } from './User/user.component';
import { RoleComponent } from './Role/role.component';
import { LoginComponent } from './Login/LoginComponent';
import { RegisterComponent} from './Register/RegisterComponent';
import { ProfileComponent } from './Profile/profile.component';
import {AllProjectsComponent} from './AllProjects/allpojects.component'

@NgModule({
  declarations: [
    AppComponent,
    ProjectComponent,
    UserTaskComponent,
    UserComponent,
    RoleComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    AllProjectsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
