using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WOrder.Web.Hosts
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            var controllerPermissions = context.ApiDescription.ControllerAttributes()
                .OfType<AbpAuthorizeAttribute>()
                .Select(attr => attr.Permissions);

            var actionPermissions = context.ApiDescription.ActionAttributes()
                .OfType<AbpAuthorizeAttribute>()
                .Select(attr => attr.Permissions);

            var permissions = controllerPermissions.Union(actionPermissions).Distinct()
                .SelectMany(p => p);

            if (permissions.Any())
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>
                    {
                        { "bearerAuth", permissions }
                    }
                };
            }
        }
    }
}
