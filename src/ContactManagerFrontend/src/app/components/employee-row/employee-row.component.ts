import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Employee } from 'src/app/models/employee';
import { DataService } from 'src/app/services/data.service';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: '[app-employee-row]',
  templateUrl: './employee-row.component.html',
  styleUrls: ['./employee-row.component.css']
})
export class EmployeeRowComponent implements OnInit {

  @Input() employee : Employee;

  viewMode : boolean = true;

  employeeForm = new FormGroup({
    employeeID: new FormControl(),
    name : new FormControl(),
    dateOfBirth : new FormControl(),
    married : new FormControl(),
    phone : new FormControl(),
    salary : new FormControl()
  });


  constructor(private service : DataService, private employeesService : EmployeesService) { }

  ngOnInit(): void {
  }

  delete() {
    this.employeesService.deleteEmployee(this.employee.employeeID).subscribe(() => this.service.updateEmployeesList());
  }

  edit() {
    this.employeeForm.setValue(this.employee);
    this.toggleMode()
  }

  save() {
    console.log(this.employeeForm.value)
    this.employeesService.updateEmployee(this.employeeForm.value as Employee).subscribe(() => {
      this.employee = this.employeeForm.value as Employee;
      this.toggleMode();
    });
  }

  private toggleMode() {
    this.viewMode = !this.viewMode;
  }

}
