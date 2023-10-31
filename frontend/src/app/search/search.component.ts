import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../service.service';
import { TweetDialogComponent } from '../tweet-dialog/tweet-dialog.component';
import { MatDialog } from '@angular/material/dialog';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
search : any;
loggedInUser : any;
peoples: any;
allTweets : any;
showPeople : boolean = false;
showPost : boolean = false;
totalPeoples = 0;
totalPosts = 0;
  constructor(private service: ServiceService,private dialog:MatDialog, private toastr: ToastrService, private spinner: NgxSpinnerService) {
    this.service.subject.subscribe((res)=>{
      this.searchPeople(this.search, this.loggedInUser?.id);
      this.searchTweet(this.search);
    })
   }

  ngOnInit(): void {
    this.loggedInUser = JSON.parse(this.service.getLocalData("user"));
  }

  searchPeople(searchTerm:any, userId:any)
  {
    this.spinner.show();
    if(searchTerm == "")
    {
      searchTerm = undefined;
    }
    this.service.searchPeople(searchTerm,userId).subscribe((res)=>{
      this.spinner.hide();
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
         this.peoples = result?.data;
         if(this.peoples?.length){
         this.totalPeoples = this.peoples?.length;
         }
         else{
          this.totalPeoples = 0;
         }
        }
        else{
          // alert(result?.message);
          this.toastr.error(result?.message);
        }
    },(err)=>{
      // alert("err:searchPeople");
      this.spinner.hide();
      this.toastr.error("Error:searchPeople");
    });
  }

  searchTweet(searchTerm:any)
  {
    this.spinner.show();
    if(searchTerm == "")
    {
      searchTerm = undefined;
    }
    this.service.searchTweet(searchTerm).subscribe((res)=>{
      this.spinner.hide();
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
         this.allTweets = result?.data;
         if(this.allTweets?.length){
         this.totalPosts = this.allTweets?.length;
         }
         else{
          this.totalPosts = 0;
         }
        }
        else{
          // alert(result?.message);
          this.toastr.error(result?.message);
        }
    },(err)=>{
      // alert("err:searchTweet");
      this.spinner.hide();
      this.toastr.error("Error:searchTweet");
    });
  }
  onSearchClick(){
    this.service.setLocalData("searchTerm",this.search);
    this.searchPeople(this.search, this.loggedInUser?.id);
    this.searchTweet(this.search);
    this.showPeople = true;
    this.showPost = false;
  }
  onPeopleClick(){
    this.showPeople = true;
    this.showPost = false;
  }
  onPostClick(){
    this.showPost = true;
    this.showPeople = false;
  }

  onFollow(otherUserId:any){
    this.service.follow(otherUserId,this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          //window.location.reload();
          this.service.subject.next(true);
          this.search = this.service.getLocalData("searchTerm");
          this.searchPeople(this.search, this.loggedInUser?.id);
          this.searchTweet(this.search);
          this.showPeople = true;
          this.showPost = false;
        }
        else{
          // alert(result?.message);
          this.toastr.error(result?.message);
        }
    },(err)=>{
      // alert("err")
      this.toastr.error("Error");
    });
  }
  onUnFollow(otherUserId:any){
    this.service.unFollow(otherUserId,this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
         // window.location.reload();
         this.service.subject.next(true);
          this.search = this.service.getLocalData("searchTerm");
          this.searchPeople(this.search, this.loggedInUser?.id);
          this.searchTweet(this.search);
          this.showPeople = true;
          this.showPost = false;
        }
        else{
          // alert(result?.message);
          this.toastr.error(result?.message);
        }
    },(err)=>{
      // alert("err")
      this.toastr.error("Error");
    });
  }

  onDelete(id:any)
    {
      this.spinner.show();
      this.service.deleteTweet(id).subscribe((res)=>{
        this.spinner.hide();
        var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          //alert("tweet deleted");
         // window.location.reload();
         this.toastr.success("Tweet deleted successfully");
          this.search = this.service.getLocalData("searchTerm");
          this.searchPeople(this.search, this.loggedInUser?.id);
          this.searchTweet(this.search);
          this.showPeople = false;
          this.showPost = true;
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
          //alert("tweet liked");
         // window.location.reload();
          this.search = this.service.getLocalData("searchTerm");
          this.searchPeople(this.search, this.loggedInUser?.id);
          this.searchTweet(this.search);
          this.showPeople = false;
          this.showPost = true;
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
         // alert("tweet disliked");
         // window.location.reload();
          this.search = this.service.getLocalData("searchTerm");
          this.searchPeople(this.search, this.loggedInUser?.id);
          this.searchTweet(this.search);
          this.showPeople = false;
          this.showPost = true;
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
