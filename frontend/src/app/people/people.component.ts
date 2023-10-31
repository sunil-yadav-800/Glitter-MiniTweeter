import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServiceService } from '../service.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {
searchTerm : any
loggedInUser : any
peoples: any
  constructor(private route: ActivatedRoute, private service: ServiceService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.searchTerm = this.route.snapshot.paramMap.get('searchTerm');
    this.loggedInUser = JSON.parse(this.service.getLocalData("user"));
    this.searchPeople(this.searchTerm,this.loggedInUser?.id);
  }

  searchPeople(searchTerm:any, userId:any)
  {
    this.service.searchPeople(searchTerm,userId).subscribe((res)=>{
      var result = JSON.parse(JSON.stringify(res));
        if(result?.successful == true)
        {
         this.peoples = result?.data;
        }
        else{
          //alert(result?.message);
          this.toastr.error(result?.message);
        }
    },(err)=>{
      // alert("err");
      this.toastr.error("Error");
    });
  }

}
