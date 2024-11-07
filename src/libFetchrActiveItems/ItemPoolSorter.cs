using libFetchrActiveItems.DataStructures;
using System.Collections.Generic;

namespace libFetchrActiveItems
{
	internal class ItemPoolSorter : IComparer<KeyValuePair<ItemPoolCategory, List<ItemData>>>
	{
		public int Compare(KeyValuePair<ItemPoolCategory, List<ItemData>> x, KeyValuePair<ItemPoolCategory, List<ItemData>> y)
		{
			if (x.Value.Count == y.Value.Count) return x.Key.TranslationName.CompareTo(y.Key.TranslationName);
			return y.Value.Count.CompareTo(x.Value.Count);
		}
	}
}
