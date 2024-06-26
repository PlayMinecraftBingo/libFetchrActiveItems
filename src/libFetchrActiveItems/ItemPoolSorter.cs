﻿using libFetchrActiveItems.DataStructures;
using System.Collections.Generic;

namespace libFetchrActiveItems
{
	internal class ItemPoolSorter : IComparer<KeyValuePair<string, List<ItemData>>>
	{
		public int Compare(KeyValuePair<string, List<ItemData>> x, KeyValuePair<string, List<ItemData>> y)
		{
			if (x.Value.Count == y.Value.Count) return Translate.CategoryName(x.Key).CompareTo(Translate.CategoryName(y.Key));
			return y.Value.Count.CompareTo(x.Value.Count);
		}
	}
}
