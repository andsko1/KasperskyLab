using KasperskyLab.Helpers;
using KasperskyLab.Models.ViewModels;
using KasperskyLabModels;
using KasperskyLabService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KasperskyLab.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public PartialViewResult ParallelQueue()
		{
			return PartialView("_ParallelQueue");
		}
		[HttpPost]
		public JsonResult ParallelQueue(ParallelQueueModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (model.PopCount > model.PushCount)
						throw new Exception("Количество извлечений не может быть больше количества вставок");
					var service = new ParallelQueueService<int>();
					int Min = -100;
					int Max = 100;

					int[] pushArr = new int[model.PushCount];
					Random randNum = new Random();
					for (int i = 0; i < pushArr.Length; i++)
					{
						pushArr[i] = randNum.Next(Min, Max);
					}
					var resultStates = service.PushAndPopTest(pushArr, model.PopCount).OrderBy(x => x.Date).ThenByDescending(x => x.Action);
					return Json(new
					{
						ok = true,
						resultHtml = this.RenderPartialViewToString("_ParallelQueueResult", resultStates.Select(x =>
						{
							return new QueueStateModel
							{
								Action = x.Action,
								Date = x.Date,
								Element = x.Element.ToString()
							};
						}).ToList())
					});
				}
				catch (Exception e)
				{
					return Json(new
					{
						ok = false,
						errormessage = e.Message
					});
				}
			}
			else
			{
				return Json(new
				{
					ok = false,
					errormessage = "Неверные значения"
				});
			}
		}

		[HttpGet]
		public PartialViewResult StackSearch()
		{
			return PartialView("_StackSearch");
		}

		[HttpPost]
		public JsonResult StackSearch(StackSearchModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var service = new StackSearchService();
					model.Pairs = service.GetPairFor(model.Numbers.ToArray(), model.SearchSum);
					return Json(new
					{
						ok = true,
						resultHtml = this.RenderPartialViewToString("_StackSearchResult", model)
					});
				}
				catch (Exception e)
				{
					return Json(new
					{
						ok = false,
						errormessage = e.Message
					});
				}
			}
			else
			{
				return Json(new
				{
					ok = false,
					errormessage = "Неверные значения"
				});
			}
		}
	}
}