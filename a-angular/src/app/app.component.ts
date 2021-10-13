import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'a-angular';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get(environment.API_BASE_URL + '/todos').subscribe((data) => {
      console.log(data);
    });
  }
}
