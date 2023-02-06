using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StoreManagement.Common.Helper
{
	public static class EnumHelper
	{
		/// <summary>
		///     Get IEnumerable List of string
		/// </summary>
		/// <typeparam name="T"> T is enumeration</typeparam>
		/// <returns></returns>
		public static IEnumerable<T> GetValues<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		/// <summary>
		/// Get Enumeration Description Attribute
		/// </summary>
		/// <param name="value"></param>
		/// <typeparam name="T">T is Enum</typeparam>
		/// <returns></returns>
		public static string GetEnumDescription<T>(T value)
		{
			if (value == null)
				return string.Empty;

			var field = value.GetType().GetField(value.ToString());
			var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}

		/// <summary>
		///     Parse string to enum by specifying the enum type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}

		/// <summary>
		///     Return the list of string from the enum type passed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<string> GetStringListByEnumType<T>()
		{
			return Enum.GetNames(typeof(T)).ToList();
		}
	}
}
