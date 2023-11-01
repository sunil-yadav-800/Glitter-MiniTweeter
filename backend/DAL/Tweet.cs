using backend.DTos;
using backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public class Tweet : ITweet
    {
        private readonly DataBaseContext db;
        public Tweet(DataBaseContext _db)
        {
            db = _db;
        }
        public void addTweet(Models.Tweet tweet)
        {

            tweet.CreatedOn = DateTime.Now;
            db.tweets.Add(tweet);
            int id = db.SaveChanges();
        }

        public List<tweetDto> getAllTweets(int userId)
        {
            List<tweetDto> tweetsList = new List<tweetDto>();
            var followerIds = db.followers.Where(f => f.FolloweeId == userId).Select(f => f.FollowerId).ToList();
            var tweets = from t in db.tweets
                         join u in db.users on t.UserId equals u.Id
                         where(t.UserId == userId || followerIds.Contains(t.UserId))
                         select new { t, u };
            var orderedTweets = tweets.OrderByDescending(t=>t.t.CreatedOn);
            
            foreach(var tt in orderedTweets)
            {
                var likeResult = db.likes.Where(l => l.TweetId == tt.t.Id && l.UserId == userId).ToList();
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
        public void editTweet(Models.Tweet tweet)
        {
            db.tweets.Update(tweet);
            db.SaveChanges();
        }
        public void deleteTweet(tweetDto tweet)
        {
            var t = new Models.Tweet
            {
                Id = tweet.Id,
                UserId = tweet.userId,
                message = tweet.message,
                CreatedOn = tweet.CreatedOn
            };
            var like = db.likes.FirstOrDefault(l => l.TweetId == tweet.Id && l.UserId == tweet.userId);
            if(like != null)
            {
                db.likes.Remove(like);
            }
            db.tweets.Remove(t);
            db.SaveChanges();
        }
        public tweetDto getTweetById(int id)
        {
           
            tweetDto result = null;
            var tweet = db.tweets.Find(id);
            
            result = new tweetDto
            {
                Id = tweet.Id,
                userId = tweet.UserId,
                message = tweet.message,
                CreatedOn = tweet.CreatedOn
            };
            return result;
        }
    }
}
