import { Component, OnInit, Input } from '@angular/core';
import { ServiceService } from '../service.service';
import {  Router } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
loggedInUser : any;
followers = 0;
@Input() following = 0;
  constructor(private service: ServiceService, private router:Router) {
    this.service.subject.subscribe((res)=>{
      this.getFollowers();
      this.getFollowing();
    })
   }

  ngOnInit(): void {
    this.loggedInUser = JSON.parse(this.service.getLocalData("user"));
    this.getFollowers();
    this.getFollowing();
  }
  logOut()
  {
    this.service.removeLocalData("user");
    this.service.removeLocalData("token");
    this.router.navigateByUrl('/login');
  }
  getFollowers()
  {
    this.service.followerCount(this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        this.followers = result?.count;
      }
      else
      {
        alert(result?.errorMessage);
      }
    },(err)=>{
      alert("err");
    })
  }
  getFollowing()
  {
    this.service.followingCount(this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
      if(result?.successful == true)
      {
        this.following = result?.count;
      }
      else
      {
        alert(result?.errorMessage);
      }
    },(err)=>{
      alert("err");
    })
  }

}
