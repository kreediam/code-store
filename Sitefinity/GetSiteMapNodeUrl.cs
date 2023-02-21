        public static string GetSiteMapNodeUrl(this Guid pageId, Guid siteId)
        {
            var multisiteContext = SystemManager.CurrentContext as MultisiteContext;
            var site = multisiteContext.GetSiteById(siteId);

            using (new SiteRegion(site))
            {
                var node = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(pageId.ToString());
                if (node != null)
                {
                    return NavigationUtilities.ResolveUrl(node);
                }
            }

            return null;
        }
