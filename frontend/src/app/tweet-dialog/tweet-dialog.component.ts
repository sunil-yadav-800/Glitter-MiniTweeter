import { Component, OnInit, Inject } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { ServiceService } from '../service.service';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'app-tweet-dialog',
  templateUrl: './tweet-dialog.component.html',
  styleUrls: ['./tweet-dialog.component.css']
})
export class TweetDialogComponent implements OnInit {
  tweetContent ='';
  loggedInUser: any;
  tweetTitle : string = 'Compose New Tweet';
  showUpdateBtn = false;
  constructor(public dialogRef: MatDialogRef<TweetDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private service: ServiceService, private router: Router, private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.loggedInUser = JSON.parse(this.service.getLocalData("user"));
    
    this.tweetContent = this.data?.tweet?.message;
    if(this.data?.title)
    {
      this.tweetTitle = this.data?.title
      this.showUpdateBtn = true;
    }
    
  }
  
  onNoClick(): void {
    this.dialogRef.close();
  }
  onSubmit()
  {
    // console.log(this.tweetContent);
    // console.log(this.loggedInUser?.id);
    this.spinner.show();
    const payload = {"UserId":this.loggedInUser?.id, "message":this.tweetContent};
    this.service.addTweet(payload).subscribe((res)=>{
      this.spinner.hide();
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        // alert("tweet added successfully");
        
        this.service.subject.next(true);
        this.toastr.success("Tweet added successfully");
      }
      else
      {
        // alert(result?.message);
        this.toastr.error(result?.message);
      }
    },(err)=>{
      // alert("err");
      this.spinner.hide();
      this.toastr.error("Error");
    });
  }
  onUpdate()
  {
    this.spinner.show();
    let payload = {
      "Id" : this.data?.tweet?.id,
      "UserId" : this.data?.tweet?.userId,
      "message" : this.tweetContent,
      "CreatedOn" : this.data?.tweet?.createdOn
    };
    this.service.editTweet(payload).subscribe((res)=>{
      this.spinner.hide();
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        // alert("Tweet updated successfully");
        
        //window.location.reload();
        this.service.subject.next(true);
        this.toastr.success("Tweet updated successfully");
      }
      else{
        // alert(result?.message);
        this.toastr.error(result?.message);
      }

    },(err)=>{
      // alert("err");
      this.spinner.hide();
      this.toastr.error("Error");
    });
  }

}
