using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.DAL
{
    public class JWTManage
    {
        //private readonly IConfiguration Configuration;
        //public JWTManage(IConfiguration configuration)
        //{
        //    this.Configuration = configuration;
        //}
        //public JWTManage()
        //{

        //}

        public static string GenerateToken(string email)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var secret = configuration.GetValue<string>("JwtSettings:SecretKey");
            //var app = ConfigurationManager.GetSection("JwtSettings");
             //var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            // var secret = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            //var symmetricKey = Convert.FromBase64String(secret);
            var symmetricKey = Encoding.UTF8.GetBytes(secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            //permClaims.Add(new Claim("userid", userId));
            permClaims.Add(new Claim("email",email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(permClaims),

                Expires = now.AddMinutes(Convert.ToInt32(30)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var secret = configuration.GetValue<string>("JwtSettings:SecretKey");
                //var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
                var tokenHandler = new JwtSecurityTokenHandler();

                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;
                var symmetricKey = Encoding.UTF8.GetBytes(secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
