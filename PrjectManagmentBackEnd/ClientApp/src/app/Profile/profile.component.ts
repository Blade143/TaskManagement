import {Component, OnInit, Output,EventEmitter, Input} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../Models/User';

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
    @Output() logoff = new EventEmitter();
    @Input()  user : User;
    @Input() token : string;
    private url='https://localhost:44312/api/User';

    public isUpd: boolean = false;

    constructor (private client: HttpClient) {}

    ngOnInit(): void {  }
    
    async Delete() {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }
        const a: boolean = await this.client.delete<boolean>(this.url + '/' + this.user.userId, requestOption).toPromise();
        this.LogOff()
    }

    async Save() {
        const headrDict = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Authorization': 'Bearer '+ this.token
        }

        const requestOption = {
            headers: new HttpHeaders(headrDict)
        }

        await this.client.put(this.url, this.user, requestOption).subscribe((data: User)=> this.user = data);
    }

    async LogOff() {
        this.user = null;
        localStorage.removeItem('auth_token');
        this.logoff.emit();
    }

    Update(): void {
        this.isUpd = true;
    }

    Cancel():void {
        this.isUpd = false;
    }

    Calculate():number{
        return this.user.finished/ (this.user.failed == 0 ? 0.1 : this.user.failed);
    }
}