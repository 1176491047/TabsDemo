using System.IO;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using Demo.Blazor.Menus;
using Demo.EntityFrameworkCore;
using Demo.Localization;
using Demo.MultiTenancy;
using Microsoft.OpenApi.Models;
using Demo.Blazor.Components.Layout;
using Volo.Abp;
using Volo.Abp.Account.Pro.Admin.Blazor.Server;
using Volo.Abp.Account.Pro.Public.Blazor.Server;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Public.Web.ExternalProviders;
using Volo.Abp.Account.Public.Web.Impersonation;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Components.Server.LeptonXTheme;
using Volo.Abp.AspNetCore.Components.Server.LeptonXTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.Blazor.Server;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Gdpr.Blazor.Extensions;
using Volo.Abp.Gdpr.Blazor.Server;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Pro.Blazor;
using Volo.Abp.Identity.Pro.Blazor.Server;
using Volo.Abp.LanguageManagement.Blazor.Server;
using Volo.Abp.LeptonX.Shared;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Pro.Blazor.Server;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TextTemplateManagement.Blazor.Server;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas.Host;
using Volo.Saas.Host.Blazor;
using Volo.Saas.Host.Blazor.Server;
using Demo.Blazor.Bundling;
using Microsoft.AspNetCore.Mvc.Versioning;
//using Elsa.Persistence.EntityFramework.Core.Extensions;
//using Elsa.Persistence.EntityFramework.PostgreSql;
//using Elsa;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;

namespace Demo.Blazor;

[DependsOn(
    typeof(DemoApplicationModule),
    typeof(DemoEntityFrameworkCoreModule),
    typeof(DemoHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAccountPublicWebImpersonationModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAccountPublicWebOpenIddictModule),
    typeof(AbpAccountPublicBlazorServerModule),
    typeof(AbpAccountAdminBlazorServerModule),
    typeof(AbpAuditLoggingBlazorServerModule),
    typeof(AbpIdentityProBlazorServerModule),
    typeof(AbpAspNetCoreComponentsServerLeptonXThemeModule),
    typeof(AbpAspNetCoreMvcUiLeptonXThemeModule),
    typeof(AbpOpenIddictProBlazorServerModule),
    typeof(LanguageManagementBlazorServerModule),
    typeof(SaasHostBlazorServerModule),
    typeof(TextTemplateManagementBlazorServerModule),
    typeof(AbpGdprBlazorServerModule)
   )]
public class DemoBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(DemoResource),
                typeof(DemoDomainModule).Assembly,
                typeof(DemoDomainSharedModule).Assembly,
                typeof(DemoApplicationModule).Assembly,
                typeof(DemoApplicationContractsModule).Assembly,
                typeof(DemoBlazorModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("Demo");
                options.UseLocalServer();
                options.UseAspNetCore();
            });

            builder.Services.AddDevExpressBlazor();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Configure<AbpAntiForgeryOptions>(options =>
        //{
        //    //关闭POST的XSRF-TOKEN验证
        //    options.AutoValidateIgnoredHttpMethods.Add("POST");
        //});
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        ConfigureElsa(context, configuration);
        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureImpersonation(context, configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureLocalizationServices();
        ConfigureSwaggerServices(context.Services);
        ConfigureExternalProviders(context, configuration);
        ConfigureAutoApiControllers();
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
        ConfigureCookieConsent(context);
        ConfigureTheme();
    }
    private void ConfigureCookieConsent(ServiceConfigurationContext context)
    {
        context.Services.AddAbpCookieConsent(options =>
        {
            options.IsEnabled = true;
            options.CookiePolicyUrl = "/CookiePolicy";
            options.PrivacyPolicyUrl = "/PrivacyPolicy";
        });
    }

    private void ConfigureTheme()
    {
        Configure<LeptonXThemeOptions>(options =>
        {
            options.DefaultStyle = LeptonXStyleNames.System;
            options.Styles.Remove(LeptonXStyleNames.Dim);
            options.Styles.Remove(LeptonXStyleNames.System);
        });
    }
    
    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }
    private static void ConfigureElsa(ServiceConfigurationContext context, IConfiguration configuration)
    {
        //context.Services.AddRazorPages();
        //var elsaSection = configuration.GetSection("Elsa");
        //context.Services
        //    .AddElsa(elsa => elsa
        //        .UseEntityFrameworkPersistence(ef =>
        //            DbContextOptionsBuilderExtensions.UsePostgreSql(ef,
        //                context.Services.GetConfiguration().GetConnectionString("ElsaService")))
        //        .AddHttpActivities(elsaSection.GetSection("Server").Bind)
        //        .AddQuartzTemporalActivities()
        //        .AddJavaScriptActivities()
        //        .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
        //    //.AddWorkflowsFrom<Program>()
        //    ////.AddActivitiesFrom<Program>()
        //    );

        // Elsa API endpoints.
        //context
        //    .Services
        //    //.AddWorkflowContextProvider() //use this when using custom context provide
        //    .AddElsaApiEndpoints()
        //    .AddRazorPages();
        //context.Services.Configure<ApiVersioningOptions>(options => { options.UseApiBehavior = false; }); //Add these lines 
    }
    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            // MVC UI
            options.StyleBundles.Configure(
                LeptonXThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );

            // Blazor UI
            options.StyleBundles.Configure(
                BlazorLeptonXThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/DevExpress.Blazor.Themes/bootstrap-external.bs5.css");
                }
            );


            options
                .ScriptBundles
                .Configure(BlazorLeptonXThemeBundles.Scripts.Global, bundle =>
                {
                    bundle.AddContributors(typeof(DevExpressThemeScriptContributor));
                });
            options
                .ScriptBundles
                .Configure(BlazorLeptonXThemeBundles.Styles.Global, bundle =>
                {
                    bundle.AddContributors(typeof(DevExpressThemeStyleContributor));
                });
        });
    }

    private void ConfigureImpersonation(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.Configure<SaasHostBlazorOptions>(options =>
        {
            options.EnableTenantImpersonation = true;
        });
        context.Services.Configure<AbpIdentityProBlazorOptions>(options =>
        {
            options.EnableUserImpersonation = true;
        });
        context.Services.Configure<AbpAccountOptions>(options =>
        {
            options.TenantAdminUserName = "admin";
            options.ImpersonationTenantPermission = SaasHostPermissions.Tenants.Impersonation;
            options.ImpersonationUserPermission = IdentityPermissions.Users.Impersonation;
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<DemoDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Demo.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<DemoDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Demo.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<DemoApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Demo.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<DemoApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Demo.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<DemoBlazorModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureExternalProviders(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication()
            .AddGoogle(GoogleDefaults.AuthenticationScheme, _ => {})
            .WithDynamicOptions<GoogleOptions, GoogleHandler>(
                GoogleDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ClientId);
                    options.WithProperty(x => x.ClientSecret, isSecret: true);
                }
            )
            .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
            {
                //Personal Microsoft accounts as an example.
                options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
                options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
            })
            .WithDynamicOptions<MicrosoftAccountOptions, MicrosoftAccountHandler>(
                MicrosoftAccountDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ClientId);
                    options.WithProperty(x => x.ClientSecret, isSecret: true);
                }
            )
            .AddTwitter(TwitterDefaults.AuthenticationScheme, options => options.RetrieveUserDetails = true)
            .WithDynamicOptions<TwitterOptions, TwitterHandler>(
                TwitterDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ConsumerKey);
                    options.WithProperty(x => x.ConsumerSecret, isSecret: true);
                }
            );
    }

    private void ConfigureLocalizationServices()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية", "ae"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский", "ru"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", "sk"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        });
    }


    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DemoMenuContributor());
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(DemoBlazorModule).Assembly;
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(DemoApplicationModule).Assembly);
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DemoBlazorModule>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseAbpSecurityHeaders();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
