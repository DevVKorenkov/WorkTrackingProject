using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTrackingSite.Attributes
{
    public class ExtentionFileFilerAttribute : Attribute, IAsyncActionFilter
    {
        MultipartReader _reader;

        MultipartSection _section;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await CheckExtention(context);

            await next();
        }

        private async Task CheckExtention(ActionExecutingContext context)
        {
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(context.HttpContext.Request.ContentType).Boundary).Value;

            _reader = new MultipartReader(boundary, context.HttpContext.Request.Body);

            _section = await _reader.ReadNextSectionAsync();

            var fileName = _section.AsFileSection().FileName;

            if (fileName.Contains(".zip"))
            {

            }
            else
            {

            }
        }
    }
}
