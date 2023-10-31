import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {ServiceService} from 'src/app/service.service'

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email:new FormControl('',Validators.required),
    password: new FormControl('',Validators.required)
  });

  constructor(private service:ServiceService, private route:Router, private toastr: ToastrService, private spinner: NgxSpinnerService) 
  { 
  }

  ngOnInit(): void {
  }
  onSubmit()
  {
   this.spinner.show()
   const payload={
    email:this.loginForm.get("email")?.value,
    password:this.loginForm.get("password")?.value
  };
    this.service.login(payload.email,payload.password).subscribe((res:any)=> {
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        var user = JSON.stringify(result?.data);
        //alert("user found");
        this.spinner.hide()
        this.toastr.success("user found");
        this.service.setLocalData("user",user);
        this.service.setLocalData("token",result?.data?.token);
        this.loginForm.reset();
        this.route.navigateByUrl('/playground');
      }
      else{
        //alert("user not found");
        this.spinner.hide()
        this.toastr.error("user not found");
      }
    },(err:any)=>{
      this.spinner.hide()
      this.toastr.error("Error");
    });
    
  }

}
