using libFetchrActiveItems.DataStructures;
using libFetchrVersion;
using System.Collections.Generic;
using System.Linq;

namespace libFetchrActiveItems
{
	public class ItemPool
	{
		private static readonly ItemPoolSorter itemPoolSorter = new();

		public static Dictionary<string, List<ItemData>> GetItemPool(FetchrVersion fetchrVersion)
		{
			List<ItemData> activeItems = ActiveItems.Get(fetchrVersion);

			Dictionary<string, List<ItemData>> itemPool = [];

			foreach (ItemData item in activeItems)
			{
				foreach (CategoryData category in item.ActiveCategories)
				{
					if (itemPool.ContainsKey(category.Id) == false) itemPool.Add(category.Id, []);
					itemPool[category.Id].Add(item);
				}
			}

			return itemPool;
		}

		public static Dictionary<string, List<ItemData>> GetSortedItemPool(FetchrVersion fetchrVersion)
		{
			Dictionary<string, List<ItemData>> itemPool = GetItemPool(fetchrVersion);

            foreach (string cat in itemPool.Keys)
			{
				itemPool[cat] = [.. itemPool[cat].Order(new ItemWeightSorter(cat))];
            }

			return itemPool.Order(itemPoolSorter).ToDictionary();
		}
	}
}