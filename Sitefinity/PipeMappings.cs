using System.Collections.Generic;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Translators;

namespace SitefinityWebApp
{
    public static class OutboundPipeConfig
    {
        public static void Register()
        {
            var mappings = PublishingSystemFactory.GetPipeMappings("SearchIndex", isInbound: false);
            mappings.Add(PublishingSystemFactory.CreateMapping("Answer", "TransparentTranslator", false, "Answer"));

            PublishingSystemFactory.RegisterPipeMappings("SearchIndex", isInbound: false, mappings);
        }

        // Telerik.Sitefinity.Search.Impl, Version=13.3.7628.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
        // Telerik.Sitefinity.Services.Search.Publishing.SearchIndexOutboundPipe

        //public static List<Mapping> GetDefaultMappings()
        //{
        //    List<Mapping> list = new List<Mapping>();
        //    list.Add(PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"));
        //    list.Add(PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalItemId"));
        //    list.Add(PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", true, "PipeId"));
        //    list.Add(PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", true, "ContentType"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"));
        //    list.Add(PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"));
        //    list.Add(PublishingSystemFactory.CreateMapping("LastModified", "TransparentTranslator", false, "LastModified"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Summary", "concatenationtranslator", true, "Summary"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Content", new string[2] { "concatenationtranslator", "htmlStripperTranslator" }, true, "Content"));
        //    list.Add(PublishingSystemFactory.CreateMapping("Provider", "TransparentTranslator", true, "Provider"));
        //    return list;
        //}
    }

    public static class ResourcesInboundPipeConfig
    {
        const string PipeName = "Telerik.Sitefinity.DynamicTypes.Model.Resources.ResourcePipe";

        public static void Register()
        {
            //var pipes = PublishingSystemFactory.GetRegisteredPipes();

            PipeTranslatorFactory.RegisterTranslator(new MyCoolTranslator());

            var mappings = PublishingSystemFactory.GetPipeMappings(PipeName, true);
            var mapping = PublishingSystemFactory.CreateMapping("PublicationDate", MyCoolTranslator.TranslatorName, false, "PublicationDate");
            mappings.Add(mapping);

            PublishingSystemFactory.RegisterPipeMappings(PipeName, isInbound: true, mappings);
        }

        // Telerik.Sitefinity, Version=13.3.7628.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
        // Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleInstallerHelper

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
    }

    public class MyCoolTranslator : Telerik.Sitefinity.Publishing.Translators.TranslatorBase
    {
        public const string TranslatorName = "MyCoolTranslator";

        public override object Translate(object[] data, IDictionary<string, string> translationSettings)
        {
            return data;
        }

        public override string Name => TranslatorName;
    }
}
