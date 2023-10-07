import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../service.service';

@Component({
  selector: 'app-following',
  templateUrl: './following.component.html',
  styleUrls: ['./following.component.css']
})
export class FollowingComponent implements OnInit {
  loggedInUser : any;
  followings : any; 
  constructor(private service : ServiceService) { 
    
  }

  ngOnInit(): void {
    this.loggedInUser = JSON.parse(this.service.getLocalData("user"));
    this.getAllFollowings();
  }

  getAllFollowings()
  {
    this.service.getAllFollowings(this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          this.followings = result?.data;
          
        }
        else{
          alert(result?.message);
        }
    },(err)=>{
      alert("err");
    });
  }

  onUnFollow(otherUserId:any){
    this.service.unFollow(otherUserId,this.loggedInUser?.id).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
          this.getAllFollowings();
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
