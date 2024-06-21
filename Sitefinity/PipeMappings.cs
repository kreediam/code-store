using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using Hawksearch.Publishing;
using Newtonsoft.Json;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.PublishingSystem;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Services.Search.Data;
using Telerik.Sitefinity.Services.Search.Publishing;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityWebApp.Search
{
    public class MyCoolTranslator : Telerik.Sitefinity.Publishing.Translators.TranslatorBase
    {
        public const string TranslatorName = "MyCoolTranslator";

        public override object Translate(object[] data, IDictionary<string, string> translationSettings)
        {
            if (data.Length == 1)
            {
                if (data[0] is ChoiceOption[] source)
                {
                    return source.Select((ChoiceOption c) => c.Text).ToList();
                }
                if (data[0] is ChoiceOption choiceOption)
                {
                    return new List<string> { choiceOption.Text };
                }
                return data[0];
            }
            return data;
        }

        // Telerik.Sitefinity.Publishing.Translators.ConcatenationTranslator
        public object Translate1(object[] data, IDictionary<string, string> translationSettings)
        {
            if (data.Length != 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    string @string = GetString(data[i]);
                    stringBuilder.Append(@string);
                    if (i + 1 < data.Length)
                    {
                        stringBuilder.Append(' ');
                    }
                }
                return stringBuilder.ToString();
            }
            return string.Empty;
        }

        public static string GetString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            if (obj is string)
            {
                return (string)obj;
            }
            TypeConverter converter = TypeDescriptor.GetConverter(obj);
            if (converter != null && converter.CanConvertTo(typeof(string)))
            {
                return converter.ConvertToString(obj);
            }
            return obj.ToString();
        }

        public override string Name => TranslatorName;
    }

    public class CoolDynamicContentInboundPipe : DynamicContentInboundPipe
    {

        public void DoMappings()
        {
            var pipes = PublishingSystemFactory.GetRegisteredPipes();

            PipeTranslatorFactory.RegisterTranslator(new MyCoolTranslator());

            var mappings = PublishingSystemFactory.GetPipeMappings("Telerik.Sitefinity.DynamicTypes.Model.Resources.ResourcePipe", true);
            var mapping = PublishingSystemFactory.CreateMapping("PublicationDate", MyCoolTranslator.TranslatorName, false, "PublicationDate");
            mappings.Add(mapping);


            /*
        [
        "DocumentSearchInboundPipe",
        "ContentInboundPipe",
        "RSSOutboundPipe",
        "ContentOutboundPipe",
        "RSSInboundPipe",
        "Pop3InboundPipe",
        "Twitter",
        "TwitterInboundPipe",
        "ConfigurationsInboundPipe",
        "FormInboundPipe",
        "SearchIndex",
        "PagePipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Accountmanagers.AccountManagerPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Alerts.AlertPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Authors.AuthorPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Contactform.PartnershipTypePipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Contactform.ProductPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Partners.PartnersPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Products.ProductPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Redirects.RedirectPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Resources.NonVideoFilePipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Resources.VideoEmbedPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Resources.ResourcePipe",
        "Telerik.Sitefinity.DynamicTypes.Model.TeamMembers.TeamMemberPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.Testimonials.TestimonialPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.ThumbnailGridAndLightbox.ThumbnailgridandlightboxPipe",
        "Telerik.Sitefinity.DynamicTypes.Model.WistiaVideos.WistiavideoPipe",
        "MediaContentInboundPipe",
        "ContentRecommendations",
        "ForumsInboundPipe"
        ]
            */

            /*
 "PropertyNames": [
        "Title",
        "PublicationDate",
        "LastModified",
        "Link",
        "Username",
        "OwnerFirstName",
        "OwnerLastName",
        "OwnerEmail",
        "Id",
        "OriginalItemId",
        "ExpirationDate",
        "PipeId",
        "ContentType",
        "LifecycleStatus",
        "Description",
        "ResourceKeywords",
        "Image",
        "resourceproducts",
        "resourcecategories",
        "resourceplatform",
        "resourceindustries",
        "resourcetrends",
        "resourceapplications",
        "Gated",
        "salesLifecycleStage",
        "salesforceCampaignId",
        "IsPartnerResource",
        "IncludeInSitemap",
        "Translations",
        "UrlName",
        "Actions",
        "Author",
        "DateCreated"
    ]
             */

        }

        // Telerik.Sitefinity, Version=14.4.8133.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
        // Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleInstallerHelper
        //
        //internal static void RegisterPipeMappings(IDynamicModuleType moduleType)
        //{
        //    string pipeName = ModuleNamesGenerator.GeneratePipeName(moduleType);
        //    List<Mapping> list = new List<Mapping>();
        //    string mainShortTextFieldName = moduleType.MainShortTextFieldName;
        //    list.Add(PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, mainShortTextFieldName));
        //    list.Add(PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"));
        //    list.Add(PublishingSystemFactory.CreateMapping("LastModified", "TransparentTranslator", true, "LastModified"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Username", "TransparentTranslator", false, "Username"));
        //    list.Add(PublishingSystemFactory.CreateMapping("OwnerFirstName", "TransparentTranslator", false, "OwnerFirstName"));
        //    list.Add(PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", false, "OwnerLastName"));
        //    list.Add(PublishingSystemFactory.CreateMapping("OwnerEmail", "TransparentTranslator", false, "OwnerEmail"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"));
        //    list.Add(PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalContentId"));
        //    list.Add(PublishingSystemFactory.CreateMapping("ExpirationDate", "TransparentTranslator", false, "ExpirationDate"));
        //    list.Add(PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", false, "PipeId"));
        //    list.Add(PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", false, "ContentType"));
        //    list.Add(PublishingSystemFactory.CreateMapping("LifecycleStatus", "TransparentTranslator", false, "LifecycleStatus"));
        //    string moduleTypeFieldName;
        //    foreach (IDynamicModuleField field in moduleType.Fields)
        //    {
        //        moduleTypeFieldName = field.Name;
        //        if (!list.Any((Mapping m) => m.DestinationPropertyName == moduleTypeFieldName))
        //        {
        //            list.Add(PublishingSystemFactory.CreateMapping(moduleTypeFieldName, "TransparentTranslator", false, moduleTypeFieldName));
        //        }
        //    }
        //    PublishingSystemFactory.RegisterPipeMappings(pipeName, isInbound: true, list);
        //}

        protected override void SetProperties(WrapperObject wrapperObject, DynamicContent contentItem)
        {
            base.SetProperties(wrapperObject, contentItem);

            // todo: how to customize wrapper object with mappings instead of custom code?

        }
    }

    public class AbcCorpSearchIndexOutboundPipe : CustomSearchIndexOutboundPipe //SearchIndexOutboundPipe
    {
        /// <summary>
        /// This function should be used if CurrentSearchService == "Hawksearch"
        /// </summary>
        internal static void RegisterAbcCorp()
        {
            try
            {
                PublishingSystemFactory.UnregisterPipe(PipeName);
                PublishingSystemFactory.RegisterPipe(PipeName, typeof(AbcCorpSearchIndexOutboundPipe));

                //RegisterPipeMappings();
            }
            catch (Exception exp)
            {
                Log.Write($"Error in RegisterAbcCorp: " + exp.ToString());
            }
        }

        private static void RegisterPipeMappings()
        {
            // https://www.progress.com/documentation/sitefinity-cms/142/for-developers-register-pipe-mappings
            // var mappings = GetDefaultMappings();
            var mappings = PublishingSystemFactory.GetPipeMappings(PipeName, isInbound: false);

            // These are the default mappings
            mappings.Add(PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalItemId"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", true, "PipeId"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", true, "ContentType"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("LastModified", "TransparentTranslator", false, "LastModified"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("Summary", "concatenationtranslator", true, "Summary"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("Content", new string[2] { "concatenationtranslator", "htmlStripperTranslator" }, true, "Content"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("Provider", "TransparentTranslator", true, "Provider"));
            //mappings.Add(PublishingSystemFactory.CreateMapping("SiteIds", "TransparentTranslator", true, "SiteIds"));



            // https://gist.github.com/EvelikoOfficial/eb8295b2452725d3c7554e259c667da3
            // https://gist.github.com/EvelikoOfficial/78526e1006ea02eecd18ba6116ff8e69


            //var linkMapping = new Mapping()
            //{
            //    DestinationPropertyName = "Link",
            //    IsRequired = true,
            //    SourcePropertyNames = new[] { "Link" }
            //};

            //linkMapping.Translations.Add(new PipeMappingTranslation()
            //{
            //    // TranslatorName = Telerik.Sitefinity.Publishing.Translators.UrlShortenerTranslator.TranslatorName
            //    TranslatorName = Telerik.Sitefinity.Publishing.Translators.TaxonomyTitleTranslator.TranslatorName
            //});


            //mappings.Add(linkMapping);




            //PublishingSystemFactory.RegisterPipeMappings(PipeName, isInbound: false, mappings);

        }

        /// <summary>
        /// This function should be used if CurrentSearchService != "Hawksearch"
        /// </summary>
        internal static void RegisterSitefinity()
        {
            try
            {
                PublishingSystemFactory.UnregisterPipe(PipeName);
                PublishingSystemFactory.RegisterPipe(PipeName, typeof(SearchIndexOutboundPipe));
            }
            catch (Exception exp)
            {
                Log.Write($"Error in RegisterSitefinity: " + exp.ToString());
            }
        }

        public override IEnumerable<IDocument> GetConvertedItemsForMapping(WrapperObject wrapperObject)
        {
            try
            {
                string contentType = wrapperObject.GetPropertyOrDefault<string>(PublishingConstants.FieldContentType);
                Guid itemId = wrapperObject.GetPropertyOrDefault<Guid>("Id");

                if (itemId == Guid.Empty)
                {
                    throw new Exception("Failed to get Id field from WrapperObject");
                }

                wrapperObject.SetOrAddProperty("HtmlTitle", "Hello world");
            }
            catch (Exception exp)
            {
                Log.Write($"Error in AbcCorpSearchIndexOutboundPipe: " + exp.ToString());
            }

            // This goes at the end so that SearchIndexOutboundPipe will create a new List<IDocument> from wrapperObject
            return base.GetConvertedItemsForMapping(wrapperObject);
        }
    }
}
