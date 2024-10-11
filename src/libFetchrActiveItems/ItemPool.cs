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

		public static List<string> GetItemsFromSameCategories(FetchrVersion fetchrVersion, string referenceItemName)
		{
			List<string> results = [];

            List<ItemData> flatItemPool = ActiveItems.Get(fetchrVersion);

			ItemData referenceItem = flatItemPool.SingleOrDefault(x => x.Item.Id == "minecraft:" + referenceItemName);

			if (referenceItem != null)
			{
				List<ItemData> itemsFound = [];

				foreach (CategoryData referenceCategory in referenceItem.Categories)
				{
                    foreach (ItemData item in flatItemPool)
					{
						foreach(CategoryData itemCategory in item.Categories)
						{
							if (referenceCategory.Id == itemCategory.Id) itemsFound.Add(item);
						}
					}
				}

				foreach (ItemData itemFound in itemsFound)
				{
					string itemId = itemFound.Item.Id.Replace("minecraft:", "");
					if (results.Contains(itemId) == false) results.Add(itemId);
				}
			}

			return results;
        }
    }
}