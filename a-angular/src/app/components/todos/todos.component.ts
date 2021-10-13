import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { TodoService } from 'src/app/services/todo.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css'],
})
export class TodosComponent implements OnInit {
  todos: any = [];
  title = new FormControl();
  due = new FormControl();
  priority = new FormControl();

  constructor(private todoService: TodoService, private router: Router) {}

  ngOnInit(): void {
    this.todoService.getTodos().subscribe((res) => {
      this.todos = res;
    });
  }

  addTodo() {
    this.todoService
      .addTodo({
        title: this.title.value,
        due: this.due.value,
        priority: this.priority.value,
      })
      .subscribe((res) => {
        console.log(res);
        this.title.setValue('');
        this.due.setValue('');
        this.priority.setValue('');
        this.ngOnInit();
      });
  }
}
