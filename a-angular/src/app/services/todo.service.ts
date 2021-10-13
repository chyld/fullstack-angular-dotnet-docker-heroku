import { Injectable } from '@angular/core';
import { environment as env } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  constructor(private http: HttpClient) {}

  getTodos() {
    return this.http.get(`${env.API_BASE_URL}/todos`);
  }

  addTodo(todo: any) {
    return this.http.post(`${env.API_BASE_URL}/todos`, todo);
  }
}
