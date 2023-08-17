import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Employee } from '../models/employee';
import { EmployeesService } from './employees.service';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private employeesList = new BehaviorSubject<Employee[]>([])
  public employeesList$ = this.employeesList.asObservable();
  constructor( private employees : EmployeesService) {
    this.updateEmployeesList();
  }

  updateEmployeesList() {
    this.employees.listEmployees().subscribe(res => this.employeesList.next(res));
  }
}
