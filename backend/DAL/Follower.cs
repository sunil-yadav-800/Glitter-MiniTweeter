using backend.DTos;
using backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public class Follower : IFollower
    {
        private readonly DataBaseContext db;
        public Follower(DataBaseContext _db)
        {
            db = _db;
        }
        public void follow(int otherUserId, int loggedInUserId)
        {
            var following = new Models.Follower
            {
                FolloweeId = loggedInUserId,
                FollowerId = otherUserId
            };
            db.followers.Add(following);
            db.SaveChanges();
        }

        public void unFollow(int otherUserId, int loggedInUserId)
        {
            var following = db.followers.Where(f => f.FolloweeId == loggedInUserId && f.FollowerId == otherUserId).FirstOrDefault();
            db.followers.Remove(following);
            db.SaveChanges();
        }
        public int followerCount(int userId)
        {
            return db.followers.Where(f => f.FollowerId == userId).Count();
        }
        public int followingCount(int userId)
        {
            return db.followers.Where(f => f.FolloweeId == userId).Count();
        }

        public List<followerDto> getAllFollowers(int userId)
        {
            var result = new List<followerDto>();
            var followers = from f in db.followers
                            join u in db.users on f.FolloweeId equals u.Id
                            where (f.FollowerId == userId)
                            select new { f, u };
            foreach(var ff in followers)
            {
                int followed = db.followers.Where(f => f.FolloweeId == userId && f.FollowerId == ff.u.Id).Count();
                bool isFollowed = followed > 0 ? true : false;
                var imageBytes = File.ReadAllBytes(ff.u.profilePic);
                var imageBase64 = Convert.ToBase64String(imageBytes);
                result.Add(new followerDto
                {
                    userId = ff.u.Id,
                    firstName = ff.u.firstName,
                    lastName = ff.u.lastName,
                    userImage = "data:image/png;base64," + imageBase64,
                    isFollowed = isFollowed
                });
            }
            return result;
        }

        public List<followerDto> getAllFollowings(int userId)
        {
            var result = new List<followerDto>();
            var followings = from f in db.followers
                            join u in db.users on f.FollowerId equals u.Id
                            where (f.FolloweeId == userId)
                            select new { f, u };
            foreach (var ff in followings)
            {
                var imageBytes = File.ReadAllBytes(ff.u.profilePic);
                var imageBase64 = Convert.ToBase64String(imageBytes);
                result.Add(new followerDto
                {
                    userId = ff.u.Id,
                    firstName = ff.u.firstName,
                    lastName = ff.u.lastName,
                    userImage = "data:image/png;base64," + imageBase64,
                    isFollowed = true
                });
            }
            return result;
        }
    }
}
