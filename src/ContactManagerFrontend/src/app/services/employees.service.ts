import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';


@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  private url =  "http://localhost:5192/api/employees"
  constructor(private http : HttpClient) {

  }

  listEmployees() : Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url);
  }

  updateEmployee(emp : Employee) : Observable<any> {
    return this.http.put(this.url, emp);
  }

  deleteEmployee(id : number) : Observable<any> {
    return this.http.delete(this.url + "/" + id);
  }

  uploadCSVEmployees(file : File) : Observable<any> {
    const formData = new FormData();
    formData.append("file", file);
    return this.http.post(this.url, formData);
  }




}
