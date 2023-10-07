import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../service.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {
 loggedInUser : any;
 followers : any; 
  constructor(private service: ServiceService) { }

  ngOnInit(): void {
    this.loggedInUser = JSON.parse(this.service.getLocalData("user"));
    this.getAllFollowers();
  }

  getAllFollowers()
  {
    this.service.getAllFollowers(this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          this.followers = result?.data;
        }
        else{
          alert(result?.message);
        }
    },(err)=>{
      alert("err");
    });
  }

  onFollow(otherUserId:any){
    this.service.follow(otherUserId,this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          this.getAllFollowers();
          this.service.subject.next(true);
         console.log(result?.message)
        }
        else{
          alert(result?.message);
        }
    },(err)=>{
      alert("err")
    });
  }
  onUnFollow(otherUserId:any){
    this.service.unFollow(otherUserId,this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          this.getAllFollowers();
          this.service.subject.next(true);
         console.log(result?.message)
        }
        else{
          alert(result?.message);
        }
    },(err)=>{
      alert("err")
    });
  }

}
