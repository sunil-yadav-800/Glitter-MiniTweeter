import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ServiceService } from '../service.service';
import { TweetDialogComponent } from '../tweet-dialog/tweet-dialog.component';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.css']
})
export class PlaygroundComponent implements OnInit {
loggedInUser : any;
allTweets: any;
  constructor(private dialog:MatDialog,private service:ServiceService, private toastr: ToastrService, private spinner: NgxSpinnerService) { 
    this.service.subject.subscribe((res)=>{
     this.getAllTweets();
    })
  }

  ngOnInit(): void {
    this.loggedInUser = JSON.parse(this.service.getLocalData("user"));
    this.getAllTweets();
  }

    openDialog(): void {
      let dialogRef = this.dialog.open(TweetDialogComponent, {
        width: '500px',
        height: '230px',
        data: { }
      });
      
    }
    getAllTweets()
    {
      this.spinner.show();
      this.service.getAllTweets(this.loggedInUser?.id).subscribe((res)=>{
        var result = JSON.parse(JSON.stringify(res));
        this.spinner.hide();
        if(result?.successful == true)
        {
          this.allTweets = result?.data;
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
    onDelete(id:any)
    {
      this.spinner.show();
      this.service.deleteTweet(id).subscribe((res)=>{
        var result = JSON.parse(JSON.stringify(res));
        this.spinner.hide();
        if(result?.successful == true)
        {
          this.toastr.success("Tweet deleted successfully");
         this.getAllTweets();
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
    onLike(tweetId:any, userId:any)
    {
      this.service.like(tweetId,userId).subscribe((res)=>{
        var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          this.getAllTweets();
        }
        else
        {
          // alert(result?.message);
          this.toastr.error(result?.message);
        }
      },(err)=>{
        // alert("err");
        this.toastr.error("Error");
      });
    }
    onDisLike(tweetId:any, userId:any)
    {
      this.service.disLike(tweetId,userId).subscribe((res)=>{
        var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
         this.getAllTweets();
        }
        else
        {
          // alert(result?.message);
          this.toastr.error(result?.message);
        }
      },(err)=>{
        // alert("err");
        this.toastr.error("Error");
      });
    }
    onEdit(tweet:any)
    {
      let dialogRef = this.dialog.open(TweetDialogComponent, {
        width: '500px',
        height: '230px',
        data: { tweet, title:"Update Tweet" }
      });
    }
}
