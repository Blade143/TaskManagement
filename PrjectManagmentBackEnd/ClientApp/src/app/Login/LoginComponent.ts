import {Component, OnInit, Output,EventEmitter} from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { LoginUser } from '../Models/LoginUser';
import { analyzeAndValidateNgModules } from '@angular/compiler';

@Component({
    selector: 'login',
    templateUrl: './LoginComponent.html'
})
export class LoginComponent implements OnInit {
    @Output() resp = new EventEmitter();
    @Output() setToken = new EventEmitter();
    @Output() setUser = new EventEmitter();
    show: boolean = false;
    user: LoginUser = new LoginUser();
    private url='https://localhost:44312/api/Login';

    constructor (private client: HttpClient) {}

    ngOnInit(): void { }
    
    async Save() {
        const response: any = await this.client.post<any>(this.url, this.user).toPromise();
        localStorage.setItem('auth_token', response.token);
        this.setToken.emit(response.token);
        this.resp.emit(true);
        this.setUser.emit(response.user);
    }
}