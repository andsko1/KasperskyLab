using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KasperskyLab.Models.ViewModels
{
	[DisplayName("Стандартная очередь")]
	public class ParallelQueueModel
	{
		[DisplayName("Количество добавлений")]
		[Range(1, 2147483647, ErrorMessage = "Введите число > 1")]
		public int PushCount { get; set; }

		[DisplayName("Количество извлечений")]
		[Range(1, 2147483647, ErrorMessage ="Введите число > 1")]
		public int PopCount { get; set; }
	}
}