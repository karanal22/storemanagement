using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StoreManagement.Common.Helper
{
	public static class CsvUtil
	{
		public static void CreateCSV<T>(List<T> list, string filePath)
		{
			using (StreamWriter sw = new StreamWriter(filePath))
			{
				CreateHeader(list, sw);
				CreateRows(list, sw);
			}
		}

		public static List<T> ReadCSVString<T>(string csvcontent, Func<string, T> mapper)
		{
			List<T> values = csvcontent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
										  .Skip(1).Where(v => !string.IsNullOrWhiteSpace(v))
										  .Select(v => mapper(v))
										  .ToList();
			return values;
		}

		public static List<T> ReadCSVFile<T>(string filePath, Func<string, T> mapper)
		{
			List<T> values = File.ReadAllLines(filePath)
										  .Skip(1)
										  .Select(v => mapper(v))
										  .ToList();
			return values;
		}

		private static void CreateHeader<T>(List<T> list, StreamWriter sw)
		{
			PropertyInfo[] properties = typeof(T).GetProperties();
			for (int i = 0; i < properties.Length - 1; i++)
			{

				string propDisplayName = ReflectionExtensions.GetPropertyDisplayName<T>(properties[i].Name);

				sw.Write(propDisplayName + ",");
			}
			var lastPropDisplayName = ReflectionExtensions.GetPropertyDisplayName<T>(properties[properties.Length - 1].Name);
			sw.Write(lastPropDisplayName + sw.NewLine);
		}

		private static void CreateRows<T>(List<T> list, StreamWriter sw)
		{
			foreach (var item in list)
			{
				PropertyInfo[] properties = typeof(T).GetProperties();
				for (int i = 0; i < properties.Length - 1; i++)
				{
					var prop = properties[i];
					sw.Write("\"" + prop.GetValue(item) + "\"" + ",");
				}
				var lastProp = properties[properties.Length - 1];
				sw.Write("\"" + lastProp.GetValue(item) + "\"" + sw.NewLine);
			}
		}

	}
}
