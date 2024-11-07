using libFetchrActiveItems.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace libFetchrActiveItems
{
	internal class ItemWeightSorter(ItemPoolCategory category) : IComparer<ItemData>
	{
		public int Compare(ItemData x, ItemData y)
		{
			int xWeight = x.Categories.Single(c => c.Id == category.Id).Weight.GetValueOrDefault();
            int yWeight = y.Categories.Single(c => c.Id == category.Id).Weight.GetValueOrDefault();

            if (xWeight == yWeight) return x.TranslationName.CompareTo(y.TranslationName);
			return yWeight.CompareTo(xWeight);
		}
	}
}
