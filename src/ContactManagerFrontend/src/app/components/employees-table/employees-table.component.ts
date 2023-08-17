import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee';
import { DataService } from 'src/app/services/data.service';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-employees-table',
  templateUrl: './employees-table.component.html',
  styleUrls: ['./employees-table.component.css']
})
export class EmployeesTableComponent implements OnInit {

  employees: Employee[] | undefined;
  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.dataService.employeesList$.subscribe(res => this.employees = res);
  }

  sort(column: string) {
    this.employees = this.employees?.sort((a, b) => {
      let res = 0;
      switch(column) {
        case 'employeeId':
        res = a.employeeID - b.employeeID;
        break;
      case 'name':
        res = a.name.localeCompare(b.name);
        break;
      case 'phone':
        res = a.phone.localeCompare(b.phone);
        break;
      case 'married':
        res = b.married ? 1 : -1;
        break;
      case 'salary':
        res = a.salary - b.salary;
        break;
      case 'dateOfBirth':
        res = a.dateOfBirth.toString().localeCompare(b.dateOfBirth.toString());
        break;
      };
      return res;
    });
  }

}
