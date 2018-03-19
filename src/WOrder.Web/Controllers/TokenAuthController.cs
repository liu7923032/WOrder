﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;

using Microsoft.AspNetCore.Mvc;
using WOrder.Authorization;
using WOrder.Authorization.JwtBearer;
using WOrder.Domain.Entities;
using WOrder.UserApp;
using WOrder.Web.Controllers;
using WOrder.Web.Models.TokenAuth;

namespace StandardTrain.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : WOrderControllerBase
    {
        private readonly IUserAppService _logInManager;
        private readonly TokenAuthConfiguration _configuration;

        public TokenAuthController(
            IUserAppService logInManager,
            TokenAuthConfiguration configuration)
        {
            _logInManager = logInManager;
            _configuration = configuration;

        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] LoginModel model)
        {
            var userEntity = await _logInManager.SignAsync(model);


            var accessToken = CreateAccessToken(CreateJwtClaims(userEntity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = userEntity.Id
            };
        }




        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(WOrder_Account account)
        {
            var claims = new List<Claim>();

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, "gsKxGZ012HLL3MI5");
        }
    }
}