import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
lgForm: any = {};
token:any;
name:string;
  constructor(private service: AuthService) { }

  ngOnInit() {
  }

  login(){
    console.log(this.lgForm);
    this.service.login(this.lgForm).subscribe( res =>{
        this.token = localStorage.getItem('token');
        this.name = localStorage.getItem('userName');
        console.log(res);
        console.log(this.token);
    },
    err=>{
      console.log(err);
    }
    )
  }

  loggedIn(){
    const token=localStorage.getItem('token');
    return !!token;
  }
  logOut(){
    localStorage.removeItem('token')
  }

}
