using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasperskyLabModels
{
	public enum QueueAction
	{
		[Description("Извлечение")]
		Pop,
		[Description("Вставка")]
		Push
	}
}
