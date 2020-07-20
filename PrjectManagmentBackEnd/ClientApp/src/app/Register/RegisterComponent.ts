import {Component, OnInit, EventEmitter, Output} from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { RegisterUser } from '../Models/RegisterUser'

@Component({
    selector: 'register',
    templateUrl: './RegisterComponent.html'
})
export class RegisterComponent implements OnInit {
    @Output() resp = new EventEmitter();
    token: string;
    show: boolean = false;
    user: RegisterUser = new RegisterUser();
    private url='https://localhost:44312/api/User';

    constructor (private client: HttpClient) {}

    ngOnInit(): void { }
    
    async Save() {
        this.user.failed = 1;
        this.user.finished = 1;
        this.user.rating = 0;

        await this.client.post(this.url, this.user).subscribe((data: RegisterUser) => this.user)
        this.resp.emit();
    }
}