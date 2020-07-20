import {Component, Input, OnInit} from '@angular/core'
import { User } from '../Models/User';
import { HttpClient } from '@angular/common/http'

@Component({
    selector: 'user',
    templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {
    @Input()user: User;
    editing: boolean = false;
    deleted: boolean = false;
    private url='https://localhost:44312/api/User';

    constructor (private client: HttpClient) {}

    ngOnInit(): void { }
    
    UpdateProject():void {
        this.editing = true;
    }

    DeleteProject(): void {
        this.client.delete(this.url+'/' + this.user.userId).subscribe( (data: boolean) => this.deleted = data)
    }

    Save(): void {
        this.client.put(this.url + '/' + this.user.userId, this.user).subscribe((data: User) => this.user)
    }

    Cancel():void {
        this.editing =false;
    }
}