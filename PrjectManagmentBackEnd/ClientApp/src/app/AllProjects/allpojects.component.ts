import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core'
import { Project } from '../Models/Project';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AllProject } from '../Models/AllProject';

@Component({
    selector: 'allp',
    templateUrl: './allprojects.component.html',
    styleUrls: ['./allprojects.component.css']
})
export class AllProjectsComponent implements OnInit {
    projects: Project[];
    @Input() token: string;
    @Output() joined = new EventEmitter();
    cproject: AllProject = new AllProject();
    url: string = 'https://localhost:44312/api/AllProjects';

    creation: boolean = false;
    j: boolean = false;

    constructor (private client: HttpClient) {}
    
    CreateProject(): void {
        this.creation = true;
    }

    Create() {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        this.cproject.status = "open";
        this.client.post(this.url,this.cproject ,requestOption).subscribe((data: Project) => this.cproject = data);
        this.joined.emit();
    }

    Cancel() {
        this.cproject = new Project();
        this.creation = false;
    }

    ngOnInit(): void {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        this.client.get(this.url, requestOption).subscribe((data: Project[]) => this.projects = data);
    }

    Join(projectId: number):void {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }
        this.client.put(this.url, projectId, requestOption).subscribe((data: boolean) => this.j = data);
        this.joined.emit();
    }
}