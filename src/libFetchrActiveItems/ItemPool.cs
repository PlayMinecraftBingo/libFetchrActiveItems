using libFetchrActiveItems.DataStructures;
using libFetchrVersion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace libFetchrActiveItems
{
    public class ItemPool
	{
		private static readonly ItemPoolSorter itemPoolSorter = new();

		public static Dictionary<ItemPoolCategory, List<ItemData>> GetItemPool(FetchrVersionData version)
		{
			List<ItemData> activeItems = ActiveItems.Get(version);

			Dictionary<ItemPoolCategory, List<ItemData>> itemPool = [];

			foreach (ItemData item in activeItems)
			{
				foreach (ItemPoolCategory category in item.ActiveCategories)
				{
					if (itemPool.ContainsKey(category) == false) itemPool.Add(category, []);
					itemPool[category].Add(item);
				}
			}

			return itemPool;
		}

        public static Dictionary<ItemPoolCategory, List<ItemData>> GetSortedItemPool(FetchrVersionData version)
		{
			Dictionary<ItemPoolCategory, List<ItemData>> itemPool = GetItemPool(version);

            foreach (ItemPoolCategory category in itemPool.Keys)
			{
				itemPool[category] = [.. itemPool[category].Order(new ItemWeightSorter(category))];
            }

			return itemPool.Order(itemPoolSorter).ToDictionary();
		}

        [Obsolete("Please pass an instance of FetchrVersionData instead")]
        public static List<string> GetItemsFromSameCategories(FetchrVersion version, string referenceItemName)
		{
			return GetItemsFromSameCategories(new FetchrVersionData(version), referenceItemName);
		}

		public static List<string> GetItemsFromSameCategories(FetchrVersionData version, string referenceItemName)
		{
			List<string> results = [];

			List<ItemData> flatItemPool = ActiveItems.Get(version);

			ItemData referenceItem = flatItemPool.SingleOrDefault(x => x.Item.Id == "minecraft:" + referenceItemName);

			if (referenceItem != null)
			{
				List<ItemData> itemsFound = [];

				foreach (CategoryData referenceCategory in referenceItem.Categories)
				{
					foreach (ItemData item in flatItemPool)
					{
						foreach (CategoryData itemCategory in item.Categories)
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