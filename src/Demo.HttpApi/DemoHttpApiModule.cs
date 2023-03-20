using Localization.Resources.AbpUi;
using Demo.Localization;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.LanguageManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Saas.Host;
using Volo.Abp.Localization;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict;
using Microsoft.Extensions.Configuration;
//using Elsa;
//using Elsa.Persistence.EntityFramework.Core.Extensions;
//using Elsa.Persistence.EntityFramework.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Versioning;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;

namespace Demo;

 [DependsOn(
    typeof(DemoApplicationContractsModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpAuditLoggingHttpApiModule),
    typeof(AbpOpenIddictProHttpApiModule),
    typeof(AbpAccountAdminHttpApiModule),
    typeof(LanguageManagementHttpApiModule),
    typeof(SaasHostHttpApiModule),
    typeof(AbpGdprHttpApiModule),
    typeof(AbpAccountPublicHttpApiModule),
    typeof(TextTemplateManagementHttpApiModule)
    )]
public class DemoHttpApiModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAntiForgeryOptions>(options =>
        {
            //关闭POST的XSRF-TOKEN验证
            options.AutoValidateIgnoredHttpMethods.Add("POST");
        });
        var configuration = context.Services.GetConfiguration();
        ConfigureLocalization();
        //ConfigureElsa(context, configuration);
    }
    //private static void ConfigureElsa(ServiceConfigurationContext context, IConfiguration configuration)
    //{
    //    var elsaSection = configuration.GetSection("Elsa");
    //    context.Services
    //        .AddElsa(elsa => elsa
    //            .UseEntityFrameworkPersistence(ef =>
    //                DbContextOptionsBuilderExtensions.UsePostgreSql(ef,
    //                    context.Services.GetConfiguration().GetConnectionString("ElsaService")))
    //            .AddHttpActivities(elsaSection.GetSection("Server").Bind)
    //            .AddQuartzTemporalActivities()
    //            .AddJavaScriptActivities()
    //            .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
    //        //.AddWorkflowsFrom<Program>()
    //        //.AddActivitiesFrom<Program>()
    //        );

    //    // Elsa API endpoints.
    //    context
    //        .Services
    //        //.AddWorkflowContextProvider() //use this when using custom context provide
    //        .AddElsaApiEndpoints()
    //        .AddRazorPages();
    //    context.Services.Configure<ApiVersioningOptions>(options => { options.UseApiBehavior = false; }); //Add these lines 
    //}
    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DemoResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
