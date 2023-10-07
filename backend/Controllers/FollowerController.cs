using backend.CustomFilter;
using backend.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthentication]
    public class FollowerController : Controller
    {
        private readonly IFollower follower;
        public FollowerController(IFollower _follower)
        {
            follower = _follower;
        }
        [HttpGet("follow/{otherUserId}/{loggedInUserId}")]
        public IActionResult follow(int otherUserId, int loggedInUserId)
        {
            try
            {
                follower.follow(otherUserId, loggedInUserId);
                return Ok(new { successful = true, message = "followed" });
            }
            catch(Exception ex)
            {
                return Ok(new { successful = false, errorMessage = ex.ToString()});
            }
        }

        [HttpGet("unFollow/{otherUserId}/{loggedInUserId}")]
        public IActionResult unFollow(int otherUserId, int loggedInUserId)
        {
            try
            {
                follower.unFollow(otherUserId, loggedInUserId);
                return Ok(new { successful = true, message = "unfollowed" });
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, errorMessage = ex.ToString() });
            }
        }
        [HttpGet("followerCount/{userId}")]
        public IActionResult followerCount(int userId)
        {
            try
            {
                int count = follower.followerCount(userId);
                return Ok(new { successful = true, count = count });
            }
            catch(Exception ex)
            {
                return Ok(new {successful = false, errorMessage = ex.ToString() });
            }
        }
        [HttpGet("followingCount/{userId}")]
        public IActionResult followingCount(int userId)
        {
            try
            {
                int count = follower.followingCount(userId);
                return Ok(new { successful = true, count = count });
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, errorMessage = ex.ToString() });
            }
        }

        [HttpGet("getAllFollowers/{userId}")]
        public IActionResult getAllFollowers(int userId)
        {
            try
            {
                var followers = follower.getAllFollowers(userId);
                return Ok(new { successful = true, data = followers});
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, errorMessage = ex.ToString() });
            }
        }

        [HttpGet("getAllFollowings/{userId}")]
        public IActionResult getAllFollowings(int userId)
        {
            try
            {
                var followings = follower.getAllFollowings(userId);
                return Ok(new { successful = true, data = followings });
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, errorMessage = ex.ToString() });
            }
        }
    }
}
