# MicroBloging Site
> Sending short messages to anyone who follows you, with the hope that your messages are useful and interesting to someone in your audience.
### Requirements
- Login and register user functionalities.
- Playground.
   - After login user lands on this dashboard page.
   - It **only** has tweets from people he/she is following along with his/her own tweets.
   - The tweet messages are listed in reverse chronological order. i.e. Latest message comes on top.
   - User has choice to like or dislike a tweet.
   - **Only user who created the tweet** has option to Edit/Delete a tweet.
 ![playground](https://github.com/sunil-yadav-800/Glitter-MiniTweeter/blob/main/snapshots/playground.png)
- Compose new tweet.
    - Clicking on **Compose New Tweet** button opens a popover with textbox.
    - Messages can contain Hashtags.
    - On clicking Add, the message gets saved and should be displayed on user Dashboard.
 ![comose new tweet](https://github.com/sunil-yadav-800/Glitter-MiniTweeter/blob/main/snapshots/add%20tweet.png)

- Followers.
   - This tab shows all the followers of current logged in user.
   - Total Number of followers are shown in tab’s heading.
  ![followers](https://github.com/sunil-yadav-800/Glitter-MiniTweeter/blob/main/snapshots/followers.png)
- Following.
   - This tab shows all the users which current logged in user is following.
   - Total Number of users being followed are shown in tab’s heading.
   - User has option to UnFollow.
  ![followings](https://github.com/sunil-yadav-800/Glitter-MiniTweeter/blob/main/snapshots/following.png)
- Search.
  - User can type any text and click search button. This will populate search results in both tabs (provided results are generated).
     - **People:** Search result works on email, first-name and last-name. “Follow”/ “Unfollow” option appears next to searched users.
     - **Post:** Search result works only on hashtags.
  ![search-people](https://github.com/sunil-yadav-800/Glitter-MiniTweeter/blob/main/snapshots/search-people.png)

  ![search-posts](https://github.com/sunil-yadav-800/Glitter-MiniTweeter/blob/main/snapshots/search-posts.png)

### Technology Stack used:
  HTML, CSS, Angular, .net core 5.0, Entity Framework core
