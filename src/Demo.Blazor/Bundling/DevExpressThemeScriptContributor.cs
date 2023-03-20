using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Components.Web.LeptonXTheme;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Demo.Blazor.Bundling
{
    public class DevExpressThemeScriptContributor: BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            
            context.Files.AddIfNotContains($"/extensionDevExpress/style-initializer.js");
        }
    }
}
