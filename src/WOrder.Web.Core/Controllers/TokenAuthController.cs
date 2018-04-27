using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Runtime.Security;
using Abp.Web.Models;
using Dark.Common.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WOrder.Authorization;
using WOrder.Domain.Entities;
using WOrder.UserApp;
using WOrder.Web.Core.Model;

namespace WOrder.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : WOrderControllerBase
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IUserAppService _logInManager;
        private readonly TokenAuthConfiguration _configuration;

        public TokenAuthController(
            IUserAppService logInManager,
            TokenAuthConfiguration configuration,
            IHostingEnvironment env)
        {
            _logInManager = logInManager;
            _configuration = configuration;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);

        }


        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromQuery]LoginModel model)
        {
            var userEntity = await _logInManager.SignAsync(model);

            var accessToken = CreateAccessToken(CreateJwtClaims(userEntity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserDto = userEntity.MapTo<UserDto>()
            };
        }

        [HttpGet]
        public async Task<ActionResult> GetAppVersion()
        {
            return await Task.FromResult(Json(new
            {
                VCode = _appConfiguration["AppVersion:VCode"],
                VName = _appConfiguration["AppVersion:VName"],
                Info = _appConfiguration["AppVersion:Info"]
            }
            ));
        }

        [HttpGet]
        public async Task<FileResult> DownApkFile()
        {
            //加载文件
            var addrUrl = "/app/pManage.apk";
            return await Task.FromResult(
                    File(addrUrl, "application/vnd.android.package-archive", Path.GetFileName(addrUrl))
            );
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

        private static List<Claim> CreateJwtClaims(UserDto account)
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
