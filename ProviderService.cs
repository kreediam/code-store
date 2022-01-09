using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Services
{
    public class ProviderService
    {
        public string GetDefaultProvider(string dataSourceName)
        {
            string providerName = DynamicModuleManager.GetDefaultProviderName();
            MultisiteContext multisiteContext = SystemManager.CurrentContext as MultisiteContext;
            var provider = multisiteContext.CurrentSite.GetDefaultProvider(dataSourceName);

            if (provider != null)
            {
                providerName = provider.ProviderName;
            }

            return providerName;
        }
    }
}