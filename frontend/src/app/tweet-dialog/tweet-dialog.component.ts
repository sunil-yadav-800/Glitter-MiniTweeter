import { Component, OnInit, Inject } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { ServiceService } from '../service.service';
import { Router } from '@angular/router';
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
    @Inject(MAT_DIALOG_DATA) public data: any, private service: ServiceService, private router: Router) { }

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
    console.log(this.tweetContent);
    console.log(this.loggedInUser?.id);
    const payload = {"UserId":this.loggedInUser?.id, "message":this.tweetContent};
    this.service.addTweet(payload).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        alert("tweet added successfully");
        window.location.reload();
      }
      else
      {
        alert(result?.message);
      }
    },(err)=>{
      alert("err");
    });
  }
  onUpdate()
  {
    let payload = {
      "Id" : this.data?.tweet?.id,
      "UserId" : this.data?.tweet?.userId,
      "message" : this.tweetContent,
      "CreatedOn" : this.data?.tweet?.createdOn
    };
    this.service.editTweet(payload).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        alert("Tweet updated successfully");
        window.location.reload();
      }
      else{
        alert(result?.message);
      }

    },(err)=>{
      alert("err");
    });
  }

}
