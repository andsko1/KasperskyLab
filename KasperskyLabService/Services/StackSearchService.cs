using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasperskyLabService
{
	public class StackSearchService
	{
		/// <summary>
		/// Получить пары значений массива, суммы которых равны заданному числу
		/// </summary>
		/// <param name="array">Исходный массив</param>
		/// <param name="sum">Заданное число</param>
		/// <returns></returns>
		public List<Tuple<double, double>> GetPairFor(double[] array, double sum)
		{
			List<Tuple<double, double>> result = new List<Tuple<double, double>>();
			//Key - значение, Value - количество повторений в массиве
			Hashtable hash = new Hashtable();
			for (int i = 0; i < array.Length; i++)
			{
				double addingValue = sum - array[i];
				if (hash.ContainsKey(addingValue) && !hash.ContainsKey(array[i]))
					result.Add(new Tuple<double, double>(array[i], addingValue));
				if (!hash.ContainsKey(array[i]))
					hash.Add(array[i], 1);
			}
			return result;
		}
	}
}
