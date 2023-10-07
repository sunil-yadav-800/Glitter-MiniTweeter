using backend.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace backend.CustomFilter
{
    public class CustomAuthentication : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var result = true;
                if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    result = false;
                }
                string token = string.Empty;
                if (result)
                {
                    token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                    var simplePrinciple = JWTManage.GetPrincipal(token);
                    var identity = simplePrinciple?.Identity as ClaimsIdentity;

                    if (identity == null)
                    {
                        result = false;
                    }
                    if (identity !=null && !identity.IsAuthenticated)
                    {
                        result = false;
                    }

                }
                if (!result)
                {
                    context.ModelState.AddModelError("Unauthorize", "You are not authorized for the request");
                    context.Result = new UnauthorizedObjectResult(context.ModelState);
                }
            }
            catch(Exception ex)
            {
                context.ModelState.AddModelError("Unauthorize", "You are not authorized for the request");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
        //public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        //{
        //    Console.WriteLine("inside custom filter authentcateasync");
        //    HttpRequestMessage request = context.Request;
        //    if (context.ActionContext.Request.Headers.Contains(""))
        //    {
        //        var tokenValue = context.ActionContext.Request.Headers.GetValues("Token").First();
        //        bool IsValid = await IsTokenValid(tokenValue);

        //        if (!IsValid)
        //        {
        //            context.ErrorResult = new AuthenticationFailureResult("Token is not valid", request);
        //        }
        //    }
        //    else
        //    {
        //        context.ErrorResult = new AuthenticationFailureResult("Token hasn't sent", request);
        //    }
        //}
        //public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        //{
        //    Console.WriteLine("inside custom filter challageasync");
        //    var challenge = new AuthenticationHeaderValue("Basic");
        //    context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
        //    return Task.FromResult(0);
        //}
        //private Task<bool> IsTokenValid(string tokenValue)
        //{
        //    Console.WriteLine("inside custom filter istokenvalid");
        //    bool result;
        //    try
        //    {
        //        string username = null;
        //        var simplePrinciple = JWTManage.GetPrincipal(tokenValue);
        //        var identity = simplePrinciple?.Identity as ClaimsIdentity;

        //        if (identity == null)
        //        {
        //            result = false;
        //        }
        //        if (!identity.IsAuthenticated)
        //        {
        //            result = false;
        //        }


        //        result = true;
        //    }
        //    catch (Exception exception)
        //    {
        //        result = false;
        //    }

        //    return Task.FromResult(result);
        //}
    }
}
