using backend.CustomFilter;
using backend.DAL;
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
    public class SearchController : Controller
    {
        private readonly ISearch search;

        public SearchController(ISearch _search)
        {
            search = _search;
        }
        [HttpGet("searchPeople/{searchTerm}/{userId}")]
        public IActionResult searchPeople(string searchTerm, int userId)
        {
            try
            {
                var peoples = search.searchPeople(searchTerm, userId);
                if(peoples.Count > 0 )
                {
                    return Ok(new { successful = true, data = peoples });
                }
                else
                {
                    return Ok(new { successful = true, Message = "NO People(s) found" });
                }
            }
            catch(Exception ex)
            {
                return Ok(new { successful = false, Message = ex.Message });
            }
        }
        [HttpGet("searchTweet/{searchTerm}/{userId}")]
        public IActionResult searchTweet(string searchTerm, int userId)
        {
            try
            {
                var posts = search.searchTweet(searchTerm, userId);
                if (posts.Count > 0)
                {
                    return Ok(new { successful = true, data = posts });
                }
                else
                {
                    return Ok(new { successful = true, Message = "NO People(s) found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { successful = false, Message = ex.Message });
            }
        }
    }
}
