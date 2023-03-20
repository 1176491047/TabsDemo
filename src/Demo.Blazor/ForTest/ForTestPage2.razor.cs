using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Blazor.ForTest
{
    public partial class ForTestPage2
    {
        IGrid Grid { get; set; }
        protected List<RecipeValueSettingDto> RecipeList { get; set; }


        [Inject]
        NavigationManager NavigationManager { get; set; }

        public string rootPath { get; set; }

        protected override async Task OnInitializedAsync()
        {

            rootPath = NavigationManager.BaseUri;
            RecipeList = new List<RecipeValueSettingDto>();
            for (int i = 0; i < 20; i++)
            {
                RecipeList.Add(new RecipeValueSettingDto()
                {
                    Key = "测试值1",
                    LimitTemplateName = "测试值2",
                    LowerLimit = 32,
                    RecipeGroupId = Guid.NewGuid(),
                    TopLimit = 31,
                    TotalRecipe = "测试值3",
                    Type = "测试值4"
                });
            }
        }
    }


    public class RecipeValueSettingDto
    {
        public string LimitTemplateName { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 上下限类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public decimal? TopLimit { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public decimal? LowerLimit { get; set; }

        /// <summary>
        /// 主配方Id
        /// </summary>
        public Guid RecipeGroupId { get; set; }

        /// <summary>
        /// 主配方名
        /// </summary>
        public string TotalRecipe { get; set; }
    }

}
