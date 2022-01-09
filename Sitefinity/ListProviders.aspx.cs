using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;


namespace SitefinityWebApp
{
    public partial class ListProviders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var context = SystemManager.CurrentContext;

            if (context is MultisiteContext multisiteContext)
            {
                var sites = multisiteContext.GetSites();
                foreach (var site in sites)
                {
                    using (new SiteRegion(site))
                    {
                        var siteTypesAndProvides = this.GetProviders(site);
                    }
                }
            }
            else
            {
                var typesAndProviders = this.GetProviders(context.CurrentSite);
            }
        }

        private IDictionary<string, string[]> GetProviders(ISite site)
        {
            var result = new Dictionary<string, string[]>();

            var dynamicModuleNames = ModuleBuilderManager.GetActiveTypes().Select(t => t.ModuleName).Distinct();
            foreach (var dynamicModule in dynamicModuleNames)
            {
                var typeProviders = site.GetProviders(dynamicModule);
                result.Add(dynamicModule, typeProviders.Select(p => p.ProviderName).ToArray());
            }

            return result;
        }
    }
}
