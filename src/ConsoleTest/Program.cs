using libFetchrActiveItems;
using libFetchrActiveItems.DataStructures;
using libFetchrVersion;
using libMinecraftVersion;
using System;
using System.Collections.Generic;

namespace ConsoleTest
{
	internal class Program
	{
        private static readonly FetchrVersionData fv_5_1_3 = new(FetchrVersion.Fetchr_5_1_3);
        private static readonly FetchrVersionData fv_5_1_4_mc_1_21_0 = new(FetchrVersion.Fetchr_5_1_4, FetchrMinecraftVersion.Minecraft_1_21);
        private static readonly FetchrVersionData fv_5_1_4_mc_1_21_3 = new(FetchrVersion.Fetchr_5_1_4, FetchrMinecraftVersion.Minecraft_1_21_3);
        
        static void Main()
		{
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            List<ItemData> f_5_1_3 = ActiveItems.Get(fv_5_1_3);

            List<ItemData> f_5_1_4_mc_1_21_0 = ActiveItems.Get(fv_5_1_4_mc_1_21_0);
            List<ItemData> f_5_1_4_mc_1_21_3 = ActiveItems.Get(fv_5_1_4_mc_1_21_3);

            Dictionary<ItemPoolCategory, List<ItemData>> itemPool = ItemPool.GetItemPool(fv_5_1_4_mc_1_21_3);
            Dictionary<ItemPoolCategory, List<ItemData>> sortedItemPool = ItemPool.GetSortedItemPool(fv_5_1_4_mc_1_21_3);

            List<string> samecat_v5_1_3 = ItemPool.GetItemsFromSameCategories(new FetchrVersionData(FetchrVersion.Fetchr_5_1_3), "spruce_sapling");
            List<string> samecat_v5_1_4_mc_1_21_2 = ItemPool.GetItemsFromSameCategories(new FetchrVersionData(FetchrVersion.Fetchr_5_1_4, FetchrMinecraftVersion.Minecraft_1_21_3), "spruce_sapling");
#pragma warning restore IDE0059 // Unnecessary assignment of a value

            Console.WriteLine();
            foreach (ItemPoolCategory c in sortedItemPool.Keys)
			{
                Console.WriteLine();
                Console.WriteLine(c.TranslationName);
                foreach (ItemPoolItem i in sortedItemPool[c])
                {
                    Console.WriteLine("    " + i.TranslationName);
                }
			}

        }
    }
}
