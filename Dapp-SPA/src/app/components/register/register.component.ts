import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private service: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.service.register(this.model).subscribe( () => {
      console.log('successfully Registered :)');
    },
    err => {
      console.log('error is==>', err);
    }
    );

  }
  cancel() {
    this.cancelRegister.emit(false);
    console.log('Cancelled');
  }

}
