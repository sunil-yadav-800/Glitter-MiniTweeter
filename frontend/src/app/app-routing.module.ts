import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FollowersComponent } from './followers/followers.component';
import { FollowingComponent } from './following/following.component';
import { LoginComponent } from './login/login.component';
import { PeopleComponent } from './people/people.component';
import { PlaygroundComponent } from './playground/playground.component';
import { PostsComponent } from './posts/posts.component';
import { RegisterComponent } from './register/register.component';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
  {path: '', component: LoginComponent, pathMatch: 'full'},
  {path: 'login', redirectTo:''},
  {path: 'register', component: RegisterComponent},
  {path: 'playground', component: PlaygroundComponent, canActivate: [AuthGuard]},
  {path: 'followers',component: FollowersComponent, canActivate: [AuthGuard]},
  {path: 'following',component:FollowingComponent, canActivate: [AuthGuard]},
  {
    path: 'search',component:SearchComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'people/:searchTerm',
        component : PeopleComponent
      },
      {
        path: 'posts/:searchTerm',
        component : PostsComponent
      }
    ]
  }
  // {path: 'search/people',component:PeopleComponent},
  // {path: 'search/posts',component:PostsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
