using System;
using System.ComponentModel;
using System.Web.Mvc;
using SitefinityWebApp.Mvc.Models.PopupMessage;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "PopupMessage", Title = "Popup Message", SectionName = DevNames.Widgets.DaytonSuperiorSection, CssClass = IconCssClass)]
    public class PopupMessageController : Controller, ICustomWidgetVisualizationExtended
    {
        private PopupMessageViewModel model;

        private const string IconCssClass = "sfCommentsIcn sfMvcIcn";

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PopupMessageViewModel Model
        {
            get
            {
                if (model == null)
                {
                    model = new PopupMessageViewModel();
                    model.UniqueId = "a" + Guid.NewGuid().ToString().Replace("-", ""); // Ids start with a letter
                }

                return model;
            }
        }

        public ActionResult Index()
        {
            return View("PopupMessage." + Model.TemplateName, Model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        public bool IsEmpty => model == null || model.PopupContent.IsNullOrWhitespace();

        public string EmptyLinkText => DevNames.Widgets.ConfigurationLinkText;

        public string WidgetCssClass => IconCssClass;
    }
}
