import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core'
import { Project } from '../Models/Project';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Task } from '../Models/Task';
import { User } from '../Models/User';
import { CreateTask } from '../Models/CreateTask';

@Component({
    selector: 'usersproject',
    templateUrl: './usersproject.component.html',
    styleUrls: ['./userprojects.component.css']
})
export class ProjectComponent implements OnInit {
    @Output() projid = new EventEmitter();
    @Output() deletes = new EventEmitter();
    @Output() tasks = new EventEmitter();
    @Input()projects: Project[];
    @Input() token: string;
    @Input() userRating: number;
    alowedRating: number[] = [0];
    selectedRatng: number = 0;
    projecttasks: Task[];
    esimateDate: Date;
    creatingtask: CreateTask = new CreateTask();
    projectusers: User[];
    selectedTask: Task = null;
    secetedUser: User = null;
    selectedUser: User = null;
    selectedTaskk: string = null;
    secetedUserr: string = null;
    selectedUserr: string = null;
    project: Project= new Project();
    deleted: boolean = false;
    manageu: boolean = false;
    managet: boolean = false;
    creattask: boolean = false;
    private url='https://localhost:44312/api/UsersProject';


    constructor (private client: HttpClient) {}

    ngOnInit(): void { }

    SelectTask(i: number) {
        this.selectedTask = this.projecttasks.find(x=> x.taskId == i);
    }

    SelectUser(i: number) {
        this.secetedUser = this.projectusers.find(x=>x.userId == i);
    }

    SelectTaskUser(i: number) {
        this.selectedUser = this.projectusers.find(x=>x.userId == i);
    }

    Delete(id: number): void {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'project',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        this.client.delete(this.url+'/' + id, requestOption).subscribe( (data: boolean) => this.deleted = data);
        this.deletes.emit();
    }

    Save(i:number): void {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'not',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }
        this.client.put(this.url + '/' + this.projects[i].projectId, this.projects[i],requestOption).subscribe((data: Project[]) => this.projects)
        this.deletes.emit();
    }

    Cancel():void {
        this.project = null;
        this.selectedTask = null;
        this.selectedTaskk = null;
        this.selectedUser = null;
        this.selectedUserr = null;
        this.secetedUser = null;
        this.secetedUserr = null;
        this.manageu = false;
        this.managet = false;
    }

    CheckTask(projid: number): void {
        this.projid.emit(projid);
        this.tasks.emit(true);
    }

    async Leave(projId: number) {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'link',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        await this.client.delete(this.url+'/'+projId,requestOption).subscribe((data: boolean)=> this.deleted = true);
        this.deletes.emit();
    }

    async CreateTask(projectr: Project) {
        this.project = projectr;
        this.secetedUser = null;
        this.secetedUserr = null;
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'tasks',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        this.client.get(this.url+'/'+this.project.projectId, requestOption).subscribe( (data: Task[]) => this.projecttasks = data )

        this.creatingtask.projectId = projectr.projectId;
        this.creatingtask.createDate = new Date();
        for(let i=1;i< this.userRating;i++) {
            this.alowedRating.push(i);
        }
        this.creattask = true;
    }

    async SelectRating(rat: number) {
        this.selectedRatng = rat;
    }

    async SaveTask() {
        if(this.selectedTaskk != null) {
            this.creatingtask.parentTaskId = this.projecttasks.find(x=>x.projectTitle == this.selectedTaskk).taskId;
        }
        else{
            this.creatingtask.parentTaskId = 0;
        }
        this.creatingtask.estimateDate = this.esimateDate;
        this.creatingtask.ratingPoints = +this.selectedRatng;
        this.creatingtask.status = 'open';
        this.creatingtask.userId = 0;
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        let s = await this.client.post(this.url, this.creatingtask, requestOption).toPromise();
    }

    async CancelTask(){
        this.creatingtask = new CreateTask();
        this.creattask = false;
        this.project = null;
    }

    ManageUsers():void {
        this.selectedTask = null;
        this.selectedTaskk = null;
        this.selectedUser = null;
        this.selectedUserr = null;
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'users',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        this.client.get(this.url+'/'+this.project.projectId, requestOption).subscribe( (data: User[]) => this.projectusers = data )

        this.manageu = true;
        this.managet = false;
        this.deletes.emit();
    }

    ManageTasks():void {
        this.secetedUser = null;
        this.secetedUserr = null;
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'tasks',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        this.client.get(this.url+'/'+this.project.projectId, requestOption).subscribe( (data: Task[]) => this.projecttasks = data )

        this.manageu = false;
        this.managet = true;
        this.deletes.emit();
    }

    FireUser():void {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'FireUser',
            'UserId': ''+this.secetedUser.userId,
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }
        
        this.client.put(this.url+'/'+this.selectedTask.taskId, this.project, requestOption).subscribe((data: Project) => this.project = data);
        this.deletes.emit();
    }

    ChangeUserOnTask(): void {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': 'SetUser',
            'UserId': ''+this.selectedUser.userId,
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }
        
        this.client.put(this.url+'/'+this.selectedTask.taskId, this.project, requestOption).subscribe((data: Project) => this.project = data);
        this.deletes.emit();
    }

    Update(proj: Project) {
        this.project = proj;
    }
}