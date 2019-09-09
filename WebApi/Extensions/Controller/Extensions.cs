using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Extensions.Controller
{
    public static class Extensions
    {

        public static CreatedAtActionResult Created(this ControllerBase controllerBase, string actionName, string id, object value)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("$Filter", $"id eq '{id}'");

            return controllerBase.CreatedAtAction(actionName, dict, value);
        }

        public static CreatedAtActionResult Created(this ControllerBase controllerBase, string actionName, long id, object value)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("$Filter", $"id eq {id}");

            return controllerBase.CreatedAtAction(actionName, dict, value);
        }

        public static IActionResult OkNotFound(this ControllerBase controllerBase, IQueryable queryable)
        {
            if (!queryable.GetEnumerator().MoveNext())
                return controllerBase.NotFound();

            return controllerBase.Ok(queryable);
        }
    }
}
