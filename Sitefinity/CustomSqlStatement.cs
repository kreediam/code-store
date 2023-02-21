        public IOrderedEnumerable<IGrouping<string, ChurchLocationListingModel>> GetLocationsSql()
        {
            LibrariesManager manager = LibrariesManager.GetManager();
            var context = (manager.Provider as IOpenAccessDataProvider).GetContext();
            var locations = context.ExecuteQuery<ChurchLocationListingModel>(locationsSql);
            var locationGroups = locations.GroupBy(x => x.RegionName).OrderBy(x => x.Key);
            return locationGroups;
        }
