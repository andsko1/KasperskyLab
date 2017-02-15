using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KasperskyLab.Models.ViewModels
{
	[DisplayName("Поиск по массиву")]
	public class StackSearchModel
	{
		[DisplayName("Исходный массив")]
		public List<double> Numbers { get; set; }
		[DisplayName("Найденные пары")]
		public List<Tuple<double, double>> Pairs { get; set; }
		[DisplayName("Искомая сумма")]
		public double SearchSum { get; set; }
	}
}