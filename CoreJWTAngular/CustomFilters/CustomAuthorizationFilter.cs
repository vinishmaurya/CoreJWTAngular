﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreJWTAngular.CustomFilters
{
    //    public class CustomAuthorizationFilter : IAsyncAuthorizationFilter
    //    {
    //        public AuthorizationPolicy Policy { get; }

    //        public CustomAuthorizationFilter()
    //        {
    //            Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //        }

    //        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    //        {
    //            if (context == null)
    //            {
    //                throw new ArgumentNullException(nameof(context));
    //            }

    //            // Allow Anonymous skips all authorization
    //            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
    //            {
    //                return;
    //            }

    //            var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
    //            var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
    //            var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

    //            if (authorizeResult.Challenged)
    //            {
    //                // Return custom 401 result
    //                context.Result = new JsonResult(new
    //                {
    //                    Message = "Token Validation Has Failed. Request Access Denied"
    //                })
    //                {
    //                    StatusCode = StatusCodes.Status200OK
    //                };
    //            }
    //        }
    //    }

    //public class CustomAuthorizationFilter : IAuthorizeData
    //{
    //    public string Policy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public string Roles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public string AuthenticationSchemes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //}

}
