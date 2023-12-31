﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagementAPI.CustomActionFilters
{
    /// <summary>
    /// check the model is valid or not.
    /// </summary>
	public class ValidateModelAttribute : ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if ( context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
        }

    }
}

