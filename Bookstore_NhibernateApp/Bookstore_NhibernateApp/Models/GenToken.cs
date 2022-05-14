using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Bookstore_NhibernateApp.Models
{
    public class GenToken
    {
        public static string GenerateJWTToken(string email, int UserId)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mahafuzahmed@555000666445321989867656878667567ahfajg"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim("Email", email),
                new Claim("Id", UserId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                "",
                "",
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}