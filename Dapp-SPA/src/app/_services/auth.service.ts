import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUrl = 'http://localhost:5000/api/auth/';
constructor(private http: HttpClient) { }

login(form: any) {
  return this.http.post(this.baseUrl + 'login', form).pipe(map((res: any) => {
    const user = res;
    if (user) {
      localStorage.setItem('token', user.token);
      localStorage.setItem('userName', user.userName);
    }
  }));
}
register(form: any) {
  return this.http.post(this.baseUrl + 'register', form);
}

}
