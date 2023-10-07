using backend.CustomFilter;
using backend.DAL;
using backend.DTos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser user;
       // private readonly IJWTManage jwtManager;

        public UserController(IUser _user)
        {
            user = _user;
            //this.jwtManager = jwtManager;
        }
        [HttpGet("getAllUsers")]
        //[Authorize]
       // [CustomAuthentication]
        public IActionResult getAllUsers()
        {
            var users = user.getAllUsers();
            return Ok(users);
        }
        [HttpGet("getUserById")]
        public IActionResult getUserById(int id)
        {
            var u = user.getUserById(id);
            return Ok(u);
        }
        [HttpPost("addUser")]
        public IActionResult addUser([FromForm]UserDto user)
        {
            try
            {
                var userExists = this.user.checkUserWithEmail(user.Email);
                if(userExists == true)
                {
                    return Ok(new { succesful = false, errorMessage = "UserAlreadyExist"});
                }
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string path = @"D:\POC\Glitter\backend\images";
                    string email = user.Email;
                    string extension = Path.GetExtension(file.FileName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fullname = Path.Combine(path, (email + extension));
                    using (var stream = System.IO.File.Create(fullname))
                    {
                        file.CopyTo(stream);
                    }
                    user.profilePic = fullname;
                }
                this.user.adduser(user);
                return Ok(new { succesful = true, errorMessage = "UserAdded" });
            }
            catch(Exception ex)
            {
                return Ok(new { succesful = false, errorMessage = ex.ToString() });
            }
        }
        [HttpGet("login/{email}/{password}")]
        //[AllowAnnonyms]
        public IActionResult login(string email, string password)
        {
            try
            {
                var user = this.user.login(email, password);
                if(user != null)
                {
                    var token = JWTManage.GenerateToken(user.Email);
                    user.Token = token;
                    return Ok(new {successful = true, data = user });
                }
                else
                {
                    return Ok(new { successful = false, errorMessage = "user not found"});
                }
            }
            catch(Exception ex)
            {
                return Ok(new { successful = false, errorMessage=ex.ToString() });
            }
            
        }
        [HttpGet("deleteUser")]
        public IActionResult deleteUser(int id)
        {
            user.deleteUser(id);
            return Ok( new { Message = "user deleted succcessfully" });
        }
    }
}
