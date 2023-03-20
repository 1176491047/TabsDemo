﻿using Demo.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Demo.Blazor;

public abstract class DemoComponentBase : AbpComponentBase
{
    protected DemoComponentBase()
    {
        LocalizationResource = typeof(DemoResource);
    }
}
