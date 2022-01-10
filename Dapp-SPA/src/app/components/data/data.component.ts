import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.css']
})
export class DataComponent implements OnInit {
  values: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.GetValues();
  }
  GetValues() {
    this.http.get('http://localhost:5000/api/data').subscribe( response => {
    this.values = response;
    },
    err => {
      console.log(err);
    });
  }
}
