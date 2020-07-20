import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core'
import { Task } from '../Models/Task';
import { Puttask } from '../Models/puttask'
import { HttpClient, HttpHeaders } from '@angular/common/http'

@Component({
    selector: 'usertask',
    templateUrl: './usertask.component.html',
    styleUrls: ['./usertask.component.css']
})
export class UserTaskComponent implements OnInit {
    @Input() projectId: number;
    @Input() token: string;
    @Input() userRating: number;
    @Output() close = new EventEmitter();
    selectTask: string = null;
    tasks: Task[];


    userTasks: boolean;
    private url='https://localhost:44312/api/UserTask';

    constructor (private client: HttpClient) {}

    ngOnInit(): void {
        
    }

    onClose(): void {
        this.close.emit();
    }

    CheckStatus(Task: Task): boolean {
        if(Task.taskStatus == 'finished') return true;
        else return false;
    }

    async LeaveTask(task: Task) {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': "user",
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        let resp = await this.client.delete(this.url+'/'+ task.taskId, requestOption).toPromise();
    }

    async FinishTask(task: Task) {
        let putt: Puttask= new Puttask();
        putt.taskId = task.taskId;
        putt.createDate = task.taskCreateDate;
        putt.disription = task.taskDiscritpion;
        putt.projectId = task.projectId;
        putt.status = task.taskStatus;
        putt.taskName = task.taskName;
        putt.taskParentID = task.taskParentID;
        putt.estimateDate =  task.taskEstimeteDate;
        putt.ratingPoints = task.ratingPoints;
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': "user",
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        let resp = await this.client.put(this.url, putt, requestOption).toPromise();
    }

    onChange(v: string): void {
        
        if(v == "projectTasks") {
            this.userTasks = false;
            const headrDict = {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'Access-Control-Allow-Headers': 'Content-Type',
                'choise': "project",
                'Authorization': 'Bearer '+ this.token
            }
    
            const requestOption = {
                headers: new HttpHeaders(headrDict)
            }
            this.client.get(this.url+'/'+this.projectId, requestOption).subscribe((data: Task[])=> this.tasks = data );
        }
        if(v == "usertasks") {
            this.userTasks = true;
            const headrDict = {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'Access-Control-Allow-Headers': 'Content-Type',
                'choise': "user",
                'Authorization': 'Bearer '+ this.token
            }
    
            const requestOption = {
                headers: new HttpHeaders(headrDict)
            }

            this.client.get(this.url+'/'+this.projectId, requestOption).subscribe((data: Task[])=> this.tasks = data );
        }
    }

    async ClaimTask(task: Task) {
        let putt: Puttask= new Puttask();
        putt.taskId = task.taskId;
        putt.createDate = task.taskCreateDate;
        putt.disription = task.taskDiscritpion;
        putt.projectId = task.projectId;
        putt.status = task.taskStatus;
        putt.taskName = task.taskName;
        putt.taskParentID = task.taskParentID;
        putt.estimateDate =  task.taskEstimeteDate;
        putt.ratingPoints = task.ratingPoints;
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'choise': "user",
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }
        
        let resp = await this.client.post(this.url, putt, requestOption).toPromise();
    }
}