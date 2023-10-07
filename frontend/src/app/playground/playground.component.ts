import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ServiceService } from '../service.service';
import { TweetDialogComponent } from '../tweet-dialog/tweet-dialog.component';
@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.css']
})
export class PlaygroundComponent implements OnInit {
loggedInUser : any;
allTweets: any;
  constructor(private dialog:MatDialog,private service:ServiceService) { }

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
      this.service.getAllTweets(this.loggedInUser?.id).subscribe((res)=>{
        var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          this.allTweets = result?.data;
        }
        else{
          alert(result?.message);
        }
      },(err)=>{
        alert("err");
      });
    }
    onDelete(id:any)
    {
      this.service.deleteTweet(id).subscribe((res)=>{
        var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
         this.getAllTweets();
        }
        else
        {
          alert(result?.message);
        }
      },(err)=>{
        alert("err");
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
          alert(result?.message);
        }
      },(err)=>{
        alert("err");
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
          alert(result?.message);
        }
      },(err)=>{
        alert("err");
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
