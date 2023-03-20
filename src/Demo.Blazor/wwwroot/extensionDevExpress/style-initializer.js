(function () {

    function isAlreadyLoaded(id) {
        return document.querySelector(`link[id^="lpx-theme-${id}-"]`)?.id;
    }

    function loadThemeCSS(key, event, cssPrefix) {
        const newThemeId = createId(event.detail.theme, key);
        const previousThemeId = createId(event.detail.previousTheme, key);
        const loadedCSS = isAlreadyLoaded(key);

        if (newThemeId !== loadedCSS) {
            leptonx.replaceStyleWith(
                createStyleUrl(cssPrefix + event.detail.theme),
                newThemeId,
                previousThemeId || loadedCSS
            );
        }
        if (newThemeId.indexOf('dark') > -1) {
            leptonx.replaceStyleWith(
                '/DevExpress.Blazor.Themes/bootstrap-external.bs5.css',
                'dev-blazing-dark',
                ''
            );
            var id = "";
            // delete 之前blazing-berry
            const prevElem =
                document.querySelector(`link[href^="/DevExpress.Blazor.Themes/bootstrap-external.bs5.css${id}"]`);
            if (prevElem) {
                prevElem.remove();
            }

            // 处理自定义背景颜色
            //const customDiv = document.querySelector(`div[id^="custom-${id}"]`);
            //console.log(customDiv);
            //if (customDiv) {
            //    customDiv.style.backgroundColor = "#161616";
            //}

        } else {
            leptonx.replaceStyleWith(
                '/DevExpress.Blazor.Themes/bootstrap-external.bs5.css',
                'dev-blazing-berry',
                'dev-blazing-dark'
            );
            //var id = "";
            //const listDivBerry = document.querySelector(`div[id^="custom-${id}"]`);
            //console.log(listDivBerry);
            //if (listDivBerry) {
            //    listDivBerry.style.backgroundColor = "#fff";
            //}
        }
    }

    function createId(theme, type) {
        return theme && `lpx-theme-${type}-${theme}`;
    }

    window.initLeptonX = function (layout = currentLayout, defaultStyle = "dim") {
        window.currentLayout = layout;

        leptonx.globalConfig.defaultSettings =
        {
            appearance: defaultStyle,
            containerWidth: 'full',
        };

        leptonx.CSSLoadEvent.on(event => {
            loadThemeCSS('bootstrap', event, 'bootstrap-');
            loadThemeCSS('color', event, '');
        });

        leptonx.init.run();
    }

    const oldAfterLeptonXInitialization = window.afterLeptonXInitialization;

    window.afterLeptonXInitialization = function () {
        if (oldAfterLeptonXInitialization) {
            oldAfterLeptonXInitialization();
        }
    }

    function createStyleUrl(theme, type) {
        debugger;
        if (isRtl()) {
            theme = theme + '.rtl';
        }

        if (type) {
            return `/_content/Volo.Abp.AspNetCore.Components.Web.LeptonXTheme/${window.currentLayout}/css/${type}-${theme}.css`
        }
        //return `/_content/Volo.Abp.AspNetCore.Components.Web.LeptonXTheme/${window.currentLayout}/css/${theme}.css`;
        return `/DevExpress.Blazor.Themes/${theme}.css`;
    }

    function isRtl() {
        return document.documentElement.getAttribute('dir') === 'rtl';
    }
})();