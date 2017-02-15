using KasperskyLab.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KasperskyLab.Models
{
	public class StackSearchModelBinder : IModelBinder
	{

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = new StackSearchModel();
			model.Numbers = new List<double>();
			var doubles = controllerContext.HttpContext.Request.Form["Numbers"]
				.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			var sum = controllerContext.HttpContext.Request.Form["SearchSum"];
			string validSum = sum.Replace('.', ',');
			double outerSum;
			if (double.TryParse(validSum, out outerSum))
			{
				model.SearchSum = outerSum;
			}
			foreach (var item in doubles)
			{
				double tmp;
				string dbl = item.Replace('.', ',');
				if (double.TryParse(dbl, out tmp))
				{
					model.Numbers.Add(tmp);
				}
			}
			return model;
		}
	}
}