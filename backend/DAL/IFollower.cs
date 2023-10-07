using backend.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public interface IFollower
    {
        public void follow(int otherUserId, int loggedInUserId);
        public void unFollow(int otherUserId, int loggedInUserId);
        public int followerCount(int userId);
        public int followingCount(int userId);
        public List<followerDto> getAllFollowers(int userId);
        public List<followerDto> getAllFollowings(int userId);
    }
}
