import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {ServiceService} from 'src/app/service.service'
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

  constructor(private service:ServiceService, private route:Router) 
  { 
  }

  ngOnInit(): void {
  }
  onSubmit()
  {
   const payload={
    email:this.loginForm.get("email")?.value,
    password:this.loginForm.get("password")?.value
  };
    this.service.login(payload.email,payload.password).subscribe((res:any)=> {
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        var user = JSON.stringify(result?.data);
        alert("user found");
        this.service.setLocalData("user",user);
        this.service.setLocalData("token",result?.data?.token);
        this.loginForm.reset();
        this.route.navigateByUrl('/playground');
      }
      else{
        alert("user not found");
      }
    },(err:any)=>{
      alert("error");
    });
    
  }

}
