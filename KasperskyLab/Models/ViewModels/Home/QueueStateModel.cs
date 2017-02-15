using KasperskyLabModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KasperskyLab.Models.ViewModels
{
	public class QueueStateModel
	{
		public QueueAction Action { get; set; }

		public string Element { get; set; }

		public DateTime Date { get; set; }
	}
}