using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public class Like : ILike
    {
        private readonly DataBaseContext db;
        public Like(DataBaseContext _db)
        {
            db = _db;
        }
        public void disLike(int tweetId, int userId)
        {
            var disliked = db.likes.Where(l => l.TweetId == tweetId && l.UserId == userId).FirstOrDefault();
            db.likes.Remove(disliked);
            db.SaveChanges();
        }

        public void like(int tweetId, int userId)
        {
            var liked = new Models.Like
            {
                UserId = userId,
                TweetId = tweetId
            };
            db.likes.Add(liked);
            db.SaveChanges();
        }
    }
}
