using System.Collections;
using System.Collections.Generic;

namespace Pampero.Tools.Utils
{
    public static class CollectionExtensions
	{
		/// <summary>
		/// Returns the number of entries in this collection or 0 if null.
		/// </summary>
		public static int Count(this IEnumerable a_Collection)
		{
			if (a_Collection == null)
				return 0;

			int count = 0;

			IEnumerator enumerator = a_Collection.GetEnumerator();
			while (enumerator.MoveNext())
				count++;

			return count;
		}

		/// <summary>
		/// Adds new entries from another list to this one.
		/// </summary>
		/// <returns>Returns a reference to this list.</returns>
		public static List<T> AddUnique<T>(this List<T> a_List, List<T> a_ItemsToAdd)
		{
			foreach (T val in a_ItemsToAdd)
			{
				if (!a_List.Contains(val))
				{
					a_List.Add(val);
				}
			}

			return a_List;
		}

		/// <summary>
		/// Removes all entries in another list from this one.
		/// </summary>
		/// <returns>Returns a reference to this list.</returns>
		public static List<T> RemoveAll<T>(this List<T> a_List, List<T> a_ItemsToRemove) where T : System.IEquatable<T>
		{
			foreach (T val in a_ItemsToRemove)
			{
				if (a_List.Contains(val))
				{
					for (int i = 0; i < a_List.Count; i++)
					{
						if (a_List[i].Equals(val))
						{
							a_List.RemoveAt(i);
							i--;
						}
					}
				}
			}

			return a_List;
		}

		/// <summary>
		/// Removes all duplicate entries from this list.
		/// </summary>
		/// <returns>Returns a reference to this list.</returns>
		public static List<T> RemoveDuplicates<T>(this List<T> a_List) where T : System.IEquatable<T>
		{
			for (int i = 0; i < a_List.Count; i++)
			{
				for (int j = (i + 1); j < a_List.Count; j++)
				{
					if (a_List[i].Equals(a_List[j]))
					{
						a_List.RemoveAt(j);
						j--;
					}
				}
			}

			return a_List;
		}

		/// <summary>
		/// Removes all empty & <see langword="null"/> entries from this list.
		/// </summary>
		/// <returns>Returns a reference to this list.</returns>
		public static List<string> Clean(this List<string> a_List)
		{
			a_List.RemoveAll((val) => string.IsNullOrEmpty(val));
			return a_List;
		}

		/// <summary>
		/// Same as <see cref="AddUnique{T}(List{T}, List{T})"/> but operates on a new list.
		/// </summary>
		public static List<T> Combine<T>(this List<T> a_List, List<T> a_ItemsToAdd)
		{
			return new List<T>(a_List).AddUnique(a_ItemsToAdd);
		}

		/// <summary>
		/// Same as <see cref="RemoveAll{T}(List{T}, List{T})"/> but operates on a new list.
		/// </summary>
		public static List<T> Without<T>(this List<T> a_List, List<T> a_ItemsToRemove) where T : System.IEquatable<T>
		{
			return new List<T>(a_List).RemoveAll(a_ItemsToRemove);
		}

		/// <summary>
		/// Same as <see cref="RemoveDuplicates{T}(List{T})"/> but operates on a new list.
		/// </summary>
		public static List<T> WithoutDuplicates<T>(this List<T> a_List) where T : System.IEquatable<T>
		{
			return new List<T>(a_List).RemoveDuplicates();
		}

		/// <summary>
		/// Same as <see cref="Clean(List{string})"/> but operates on a new list.
		/// </summary>
		public static List<string> WithoutEmpties(this List<string> a_List)
		{
			return new List<string>(a_List).Clean();
		}

		/// <summary>
		/// Is another list identical (including order) to this one?
		/// </summary>
		public static bool IsIdentical<T>(this List<T> a_List, List<T> a_OtherList) where T : System.IEquatable<T>
		{
			if (a_List == a_OtherList)
				return true;

			if (a_List.Count != a_OtherList.Count)
				return false;

			for (int i = 0; i < a_List.Count; i++)
			{
				if (!a_List[i].Equals(a_OtherList[i]))
					return false;
			}

			return true;
		}
	}
}