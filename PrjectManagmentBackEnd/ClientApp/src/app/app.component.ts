import { Component, OnInit} from '@angular/core';
import { Project } from './Models/Project';
import { Task } from './Models/Task';
import { User } from './Models/User';
import { Role } from './Models/Role';
import { Puttask } from './Models/puttask'
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private url='https://localhost:44312/api/';
  title = 'ClientApp';
  user: User = new User();
  token: string;
  selectedProject: number;
  usersprojects: Project[];
  selectTask: string = "None";
  selectData: Array<string> = [ "ProjectTasks", "MyTasks"]

  profile: boolean = false;
  showProjects: boolean = false;
  logedIn: boolean = false;
  login: boolean = true;
  register: boolean = false; 
  showusertask: boolean = false;

  constructor(private cilent: HttpClient) {}

  ngOnInit(): void {
    this.Ofer();
    this.token = localStorage.getItem("auth_token");
    if(this.token != null && this.token != ""){
      const headrDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Authorization': 'Bearer '+ this.token
      }

      const requestOption = {
        headers: new HttpHeaders(headrDict)
      }

      this.cilent.get(this.url+'User', requestOption).subscribe((data: User) => this.user = data)
      this.LogedIn(true)
    }
  }

  SetProjectId(v: number) {
    this.selectedProject = v;
    this.showusertask = false;
  }

  ShowProjects(): void{
    this.Ofer();
    this.showProjects = true;
  }

  CloseTasks():void{
    this.showusertask = false;
  }

  ShowProfile(): void{
    this.Ofer();
    this.profile = true;
  }

  ShowTasks(v: boolean):void {
    this.showusertask = true;
  }

  Ofer(){
    this.profile = false
    this.showProjects = false
    this.login = false
    this.register = false
    this.showusertask = false;
  }

  LogedIn(vr: boolean): void{
    const headrDict = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Allow-Headers': 'Content-Type',
      'Authorization': 'Bearer '+ this.token
    }

    const requestOption = {
      headers: new HttpHeaders(headrDict)
    }


    this.cilent.get(this.url+'UsersProject', requestOption).subscribe((data: Project[])=> this.usersprojects = data);
    this.Ofer();
    this.logedIn = true;
    this.ShowProfile();
  }

  Login(): void{
    this.Ofer();
    this.login = true;
  }

  SetUser(us: User) {
      this.user = us;
  }

  Register(): void {
    this.Ofer();
    this.register = true;
  }

  LogOff():void {
    this.logedIn = false;
    this.Ofer();
    this.login = true;
  }

  setToken(tkn: string): void {
    this.token = tkn;
  }
}
