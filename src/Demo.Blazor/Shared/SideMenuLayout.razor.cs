using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.AspNetCore.Components.Web.LeptonXTheme.Components.ApplicationLayout.Common;
using Volo.Abp.AspNetCore.Components.Web.Theming.Layout;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.LeptonX.Shared;

namespace Demo.Blazor.Shared
{
    public partial class SideMenuLayout
    {
        [Inject]
        protected IAbpUtilsService UtilsService { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        protected PageLayout PageLayout { get; set; }

        protected PageToolbar Toolbar { get; } = new();


        [Inject]
        protected IOptions<LeptonXThemeOptions> Options { get; set; }

        List<string> UnUseTabsUrlList = new List<string>();

        Dictionary<string, string> needRedirectUrlList = new Dictionary<string, string>();

        /// <summary>
        /// 承载需要加载到TABS组件的其它包含页面的程序集
        /// </summary>
        public Assembly[] otherAssemblys;
        protected override async Task OnInitializedAsync()
        {

            needRedirectUrlList.Add("workflow-instances", "vskyWorkFlowManage");
            needRedirectUrlList.Add("workflow-definitions", "vskyWorkFlowManage");
            needRedirectUrlList.Add("workflow-registry", "vskyWorkFlowManage");
            await base.OnInitializedAsync();

            string needRedirectUrl = needRedirectUrlList.Keys.FirstOrDefault(x => NavigationManager.Uri.Contains(x));
            if (!string.IsNullOrEmpty(needRedirectUrl))
                NavigationManager.NavigateTo(NavigationManager.BaseUri + $"/{needRedirectUrlList[needRedirectUrl]}");


            var assemblys = new List<Assembly>();
            assemblys.Add(Assembly.GetExecutingAssembly());
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.SettingManagement.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.TextTemplateManagement.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.OpenIddict.Pro.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.AuditLogging.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.Identity.Pro.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.LanguageManagement.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Saas.Host.Blazor.dll"))); 
             otherAssemblys = assemblys.ToArray();

            //ABP基础页面 包括设计日志等页面
            UnUseTabsUrlList.Add("Account");
            UnUseTabsUrlList.Add("audit-logs");
            UnUseTabsUrlList.Add("setting-management");
            UnUseTabsUrlList.Add("audit-logs");
            UnUseTabsUrlList.Add("setting-management");
            UnUseTabsUrlList.Add("identity");
            UnUseTabsUrlList.Add("text-templates");
            UnUseTabsUrlList.Add("openIddict");
            

            //ELSA相关页面
            UnUseTabsUrlList.Add("vskyWorkFlowManage");




        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await UtilsService.AddClassToTagAsync("body", GetBodyClassName());
                await JSRuntime.InvokeVoidAsync("initLeptonX", new[] { "side-menu", Options.Value.DefaultStyle });
                await JSRuntime.InvokeVoidAsync("afterLeptonXInitialization", new[] { "side-menu", Options.Value.DefaultStyle });

            }
        }


        //
        private Task OnClickTabItemAsync(TabItem item)
        {

            //language-management/languages    text-templates audit-logs setting-management identity openIddict
            //这些路由是ABP框架中的基础管理模块 不支持静态切换页面 否则无法重新加载位于工具栏的title和新增按钮 只能通过导航的方式刷新页面使其重载
            //if (item.Url.Contains("language-management") || item.Url.Contains("audit-logs") || item.Url.Contains("setting-management") || item.Url.Contains("identity") || item.Url.Contains("openIddict"))
            //    NavigationManager.NavigateTo(item.Url, true);
            //StateHasChanged();
            return Task.CompletedTask;
        }
        private string GetBodyClassName()
        {
            return "lpx-theme-" + Options.Value.DefaultStyle;
        }
    }
}
