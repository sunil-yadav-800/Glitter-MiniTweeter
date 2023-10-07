using backend.DTos;
using backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public class Search : ISearch
    {
        private readonly DataBaseContext db;
        public Search(DataBaseContext _db)
        {
            db = _db;
        }
        public List<searchDto> searchPeople(string searchTerm, int userId)
        {
            List<searchDto> allUsers = new List<searchDto>();
            var users = db.users.Where(u => (u.Id != userId) && (u.Email.Contains(searchTerm) || u.firstName.Contains(searchTerm) || u.lastName.Contains(searchTerm))).ToList();
            foreach(var u in users)
            {
                var follow = db.followers.Where(f => f.FollowerId == u.Id && f.FolloweeId == userId).ToList();
                bool flag = follow.Count > 0 ? true : false;
                var imageBytes = File.ReadAllBytes(u.profilePic);
                var imageBase64 = Convert.ToBase64String(imageBytes);
                allUsers.Add(new searchDto
                {
                    userId = u.Id,
                    firstName = u.firstName,
                    lastName = u.lastName,
                    userImage = "data:image/png;base64," + imageBase64,
                    isFollowed = flag
                });
            }
            return allUsers;
        }
        public List<tweetDto> searchTweet(string searchTerm)
        {
            List<tweetDto> tweetsList = new List<tweetDto>();
            var tweets = from t in db.tweets
                         join u in db.users on t.UserId equals u.Id
                         where (t.message.Contains("#" + searchTerm))
                         select new { t, u };
            var orderedTweets = tweets.OrderByDescending(t => t.t.CreatedOn);

            foreach (var tt in orderedTweets)
            {
                var likeResult = db.likes.Where(l => l.TweetId == tt.t.Id && l.UserId == tt.u.Id).ToList();
                bool liked = likeResult.Count > 0 ? true : false;
                var imageBytes = File.ReadAllBytes(tt.u.profilePic);
                var imageBase64 = Convert.ToBase64String(imageBytes);
                tweetsList.Add(new tweetDto
                {
                    Id = tt.t.Id,
                    message = tt.t.message,
                    userId = tt.u.Id,
                    firstName = tt.u.firstName,
                    lastName = tt.u.lastName,
                    userImage = "data:image/png;base64," + imageBase64,
                    CreatedOn = tt.t.CreatedOn,
                    isLiked = liked
                });
            }
            return tweetsList;
        }
    }
}
