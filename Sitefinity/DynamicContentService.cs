using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;

namespace SitefinityWebApp.Services
{
    public abstract class DynamicContentService<T> where T : new()
    {
        private string _moduleProvider;
        private string _transactionName;
        private string _typeName;
        private Type _contentType;
        private DynamicModuleManager _dynamicModuleManager;
        private VersionManager _versionManager;

        public DynamicContentService(string typeName, string moduleName)
        {
            var multisiteContext = SystemManager.CurrentContext as MultisiteContext;

            if (multisiteContext != null)
            {
                _moduleProvider = multisiteContext.CurrentSite
                    .GetProviders(moduleName)
                    .Select(p => p.ProviderName)
                    .FirstOrDefault();
            }

            _typeName = typeName;
            _contentType = TypeResolutionService.ResolveType(_typeName);
            _transactionName = (Guid.NewGuid()).ToString();
            _dynamicModuleManager = DynamicModuleManager.GetManager(_moduleProvider, _transactionName);
            _versionManager = VersionManager.GetManager(null, _transactionName);
        }

        protected abstract T BuildModel(DynamicContent item);

        public T GetItem(Guid id)
        {
            var item = GetDynamicContentItem(id);
            T model = BuildModel(item);

            return model;
        }

        public List<T> GetItems(ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            List<T> models = new List<T>();
            var items = GetDynamicContentItems(lifecycleStatus, visible);

            foreach (var item in items)
            {
                models.Add(BuildModel(item));
            }

            return models;
        }

        public IEnumerable<DynamicContent> GetDynamicContentItems(ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            List<T> models = new List<T>();
            var items = _dynamicModuleManager.GetDataItems(_contentType);
            items = items.Where(i => i.Status == lifecycleStatus && i.Visible == visible);

            return items;
        }

        public DynamicContent GetSFItem(Guid id)
        {
            var item = _dynamicModuleManager.GetDataItems(_contentType).FirstOrDefault(x => x.Id.Equals(id));

            return item;
        }

        public DynamicContent GetMaster(DynamicContent item)
        {
            return _dynamicModuleManager.Lifecycle.GetMaster(item) as DynamicContent;
        }

        /// <summary>
        /// Used to get a list of dynamic content items, calls abstract Build Model to map dynamic content items to T
        /// </summary>
        /// <param name="predicate">Where condition for the query for the dynamic content items</param>
        /// <param name="lifecycleStatus">Defaults to Live to only grab published content</param>
        /// <returns>A list of the dynamic content objects, mapped to T</returns>
        public List<T> GetList(Expression<Func<DynamicContent, bool>> predicate, int Take = 0, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live)
        {
            var modelList = new List<T>();
            var items = _dynamicModuleManager.GetDataItems(_contentType).Where(predicate);
            items = items.Where(i => i.Status == lifecycleStatus);

            if (Take > 0)
            {
                items = items.Take(Take);
            }

            foreach (var item in items)
            {
                var model = BuildModel(item);
                modelList.Add(model);
            }

            return modelList;
        }

        public List<T> GetSiblings(Guid parentId, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            var modelList = new List<T>();
            var items = GetDynamicContentSiblings(parentId, lifecycleStatus, visible);

            foreach (var item in items)
            {
                var model = BuildModel(item);
                modelList.Add(model);
            }

            return modelList;
        }

        public List<DynamicContent> GetDynamicContentSiblings(Guid parentId, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            var items = _dynamicModuleManager.GetDataItems(_contentType).Where(x => x.SystemParentId == parentId);
            items = items.Where(i => i.Status == lifecycleStatus && i.Visible == visible);
            return items.ToList();
        }

        public List<T> GetChildren(DynamicContent parentItem, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            var modelList = new List<T>();
            var items = _dynamicModuleManager.GetChildItems(parentItem, _contentType);
            items = items.Where(i => i.Status == lifecycleStatus && i.Visible == visible);

            foreach (var item in items)
            {
                var model = BuildModel(item);
                modelList.Add(model);
            }

            return modelList;
        }

        public T GetItem(Expression<Func<DynamicContent, bool>> predicate, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live)
        {
            var items = _dynamicModuleManager?.GetDataItems(_contentType).Where(predicate) ?? null;
            items = items?.Where(i => i.Status == lifecycleStatus) ?? null;
            var item = items?.FirstOrDefault() ?? null;

            if (item == null)
            {
                return default(T);
            }

            var model = BuildModel(item);

            return model;
        }

        public DynamicContent GetDynamicContentItem(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return null;
            }

            DynamicContent item = _dynamicModuleManager.GetDataItem(_contentType, id);
            if (item == null)
            {
                item = _dynamicModuleManager.GetDataItems(_contentType).FirstOrDefault(x => x.Id == id);
            }

            return item;
        }

        protected DynamicContent CreateDataItem()
        {
            return _dynamicModuleManager.CreateDataItem(_contentType);
        }

        public void CheckoutUpdatePublishCommit(DynamicContent item, Action<DynamicContent> updateFunc)
        {
            var temp = Checkout(item);
            updateFunc(temp);
            var master = CheckIn(temp);
            Publish(master);
            CommitTransaction();
        }

        protected DynamicContent Checkout(DynamicContent contentItem)
        {
            return _dynamicModuleManager.Lifecycle.CheckOut(contentItem) as DynamicContent;
        }

        protected DynamicContent CheckIn(DynamicContent contentItem)
        {
            return _dynamicModuleManager.Lifecycle.CheckIn(contentItem) as DynamicContent;
        }

        protected void Publish(DynamicContent contentItem)
        {
            _dynamicModuleManager.Lifecycle.Publish(contentItem);
            contentItem.SetWorkflowStatus(_dynamicModuleManager.Provider.ApplicationName, ContentUIStatus.Published.ToString());

            _versionManager.CreateVersion(contentItem, true);
        }

        protected void CommitTransaction()
        {
            TransactionManager.CommitTransaction(_transactionName);
        }
    }
}
