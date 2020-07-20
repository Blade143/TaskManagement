import {Component, Input, OnInit} from '@angular/core'
import { Role } from '../Models/Role';
import { HttpClient } from '@angular/common/http'

@Component({
    selector: 'role',
    templateUrl: './role.component.html'
})
export class RoleComponent implements OnInit {
    @Input()role: Role;
    editing: boolean = false;
    deleted: boolean = false;
    private url='https://localhost:44312/api/Role';

    constructor (private client: HttpClient) {}

    ngOnInit(): void { }
    
    UpdateProject():void {
        this.editing = true;
    }

    DeleteProject(): void {
        this.client.delete(this.url+'/' + this.role.roleId).subscribe( (data: boolean) => this.deleted = data)
    }

    Save(): void {
        this.client.put(this.url + '/' + this.role.roleId, this.role).subscribe((data: Role) => this.role)
    }

    Cancel():void {
        this.editing =false;
    }
}