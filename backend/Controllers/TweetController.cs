using backend.CustomFilter;
using backend.DAL;
using backend.DTos;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthentication]
    public class TweetController : Controller
    {
        private readonly ITweet Tweet;
        public TweetController(ITweet _tweet)
        {
            this.Tweet = _tweet;
        }
        [HttpPost("addTweet")]
        public IActionResult addTweet(Models.Tweet tweet)
        {
            try
            {
                 Tweet.addTweet(tweet);
                return Ok(new { successful = true, Message = "Tweet added successfully"});
            }
            catch(Exception ex)
            {
                return Ok(new { successful = false, Message = "Errors occured while adding tweet" });
            }
            
                        
        }

        [HttpGet("getAllTweets/{userId}")]
        public IActionResult getAllTweets(int userId)
        {
            try
            {
                var res = Tweet.getAllTweets(userId);
                if (res.Count > 0)
                {
                    return Ok(new { successful = true, data = res });
                }
                else
                {
                    return Ok(new { successful = true, Message = "NO tweet(s) found"});
                }
            }
            catch(Exception ex)
            {
                return Ok(new { successful = false, Message = "Error occured while fetching data from database" });
            }
            
        }
        [HttpPost("editTweet")]
        public IActionResult editTweet(Models.Tweet tweet)
        {
            try
            {
                Tweet.editTweet(tweet);
                return Ok(new { successful = true, Message = "Tweet updated successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, Message = "Errors occured while adding tweet" });
            }


        }

        [HttpGet("deleteTweet/{id}")]
        public IActionResult deleteTweet(int id)
        {
            try
            {
                var tweet = Tweet.getTweetById(id);
                if(tweet != null && tweet.Id > 0)
                {
                    Tweet.deleteTweet(tweet);
                    return Ok(new { successful = true, Message = "Tweet deleted successfully" });
                }
                return Ok(new { successful = false, Message = "Tweet not found" });
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, Message = "Errors occured while deleting tweet" });
            }
        }

        [HttpGet("getTweetById/{id}")]
        public IActionResult getTweetById(int id)
        {
            try
            {
                var tweet = Tweet.getTweetById(id);
                if (tweet != null && tweet.Id > 0)
                {
                    return Ok(new { successful = true, data = tweet });
                }
                return Ok(new { successful = false, Message = "Tweet not found" });
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, Message = "Errors occured while adding tweet" });
            }
        }
    }
    
}
