﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;

namespace WOrder.Web.Core
{
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app)
        {
            return app.Use(async (ctx, next) =>
            {
                if (ctx.User.Identity?.IsAuthenticated != true)
                {
                    var result = await ctx.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                    if (result.Succeeded && result.Principal != null)
                    {
                        ctx.User = result.Principal;
                    }
                }

                await next();
            });
        }
    }
}
