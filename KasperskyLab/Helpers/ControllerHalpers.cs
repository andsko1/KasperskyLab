using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace KasperskyLab.Helpers
{
	public static class ControllerHalpers
	{
		public static string RenderPartialViewToString(this Controller controller, string viewName, object model, Dictionary<string, object> additionalViewData = null)
		{
			if (string.IsNullOrEmpty(viewName))
				viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
			if (additionalViewData == null)
				additionalViewData = new Dictionary<string, object>();

			controller.ViewData.Model = model;
			foreach (var avd in additionalViewData)
			{
				controller.ViewData[avd.Key] = avd.Value;
			}

			using (StringWriter sw = new StringWriter())
			{
				ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
				ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
				viewResult.View.Render(viewContext, sw);

				return sw.GetStringBuilder().ToString();
			}
		}
		public static string GetDescription(this Enum value, bool getAlternativeIfExists = false)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
			var attr = attribute as DescriptionAttribute;
			return attr == null ? value.ToString() : attr.Description;
		}
	}
}