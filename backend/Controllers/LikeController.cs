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
    public class LikeController : Controller
    {
        private readonly ILike likee;

        public LikeController(ILike _like)
        {
            likee = _like;
        }
        [HttpGet("like/{tweetId}/{userId}")]
        public IActionResult like(int tweetId, int userId)
        {
            try
            {
                likee.like(tweetId, userId);
                return Ok(new { successful = true, message = "liked" });
            }
            catch(Exception ex)
            {
                return Ok(new { successful = false, errorMessage = ex.ToString() });
            }
        }

        [HttpGet("disLike/{tweetId}/{userId}")]
        public IActionResult disLike(int tweetId, int userId)
        {
            try
            {
                likee.disLike(tweetId, userId);
                return Ok(new { successful = true, message = "disliked" });
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, errorMessage = ex.ToString() });
            }
        }
    }
}
