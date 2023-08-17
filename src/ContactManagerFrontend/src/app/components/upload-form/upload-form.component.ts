import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DataService } from 'src/app/services/data.service';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-upload-form',
  templateUrl: './upload-form.component.html',
  styleUrls: ['./upload-form.component.css']
})
export class UploadFormComponent implements OnInit {

  private file : File | undefined;

  fileField = new FormControl(null)


  constructor(private service : EmployeesService, private dataService: DataService) { }

  ngOnInit(): void {
  }

  onFileSelected(event : any) {
    this.file = event.target.files[0];
    console.log(this.file?.name)
  }

  upload() {
    if(this.file) this.service.uploadCSVEmployees(this.file).subscribe(() => {
      this.fileField.reset();
      this.dataService.updateEmployeesList();
    });
  }

}
