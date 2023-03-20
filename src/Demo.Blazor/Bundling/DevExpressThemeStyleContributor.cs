using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Volo.Abp.AspNetCore.Components.Web.LeptonXTheme;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.LeptonX.Shared;
using System.Collections.Generic;

namespace Demo.Blazor.Bundling
{
    public class DevExpressThemeStyleContributor : BundleContributor
    {
        private const string LEPTONX_STYLE_COOKIE_NAME = "lpx_loaded-css";
        private const string RootPath = "/DevExpress.Blazor.Themes";
        public override Task ConfigureBundleAsync(BundleConfigurationContext context)
        {

            var httpContext = context.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            if (httpContext != null)
            {
                context.Files.AddIfNotContains($"{RootPath}/bootstrap-external.bs5.css");
                
                //var styleName = httpContext.HttpContext?.Request.Cookies[LEPTONX_STYLE_COOKIE_NAME];
                //if (styleName!.Equals(LeptonXStyleNames.Dark))
                //{
                //    context.Files.AddIfNotContains($"{RootPath}/blazing-{styleName}.bs5.css");
                //}
                //else
                //{
                //    styleName = "berry";
                //    context.Files.AddIfNotContains($"{RootPath}/blazing-{styleName}.bs5.css");
                //}
            }
            //return base.ConfigureBundleAsync(context);
            return Task.CompletedTask;
        }
    }
}