using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace libFetchrActiveItems
{
    internal class Translate
	{
		private static readonly Dictionary<string, string> McLang = LoadMcLang();

        private static Dictionary<string, string> LoadMcLang()
        {
            using (StreamReader reader = new(Assembly.GetExecutingAssembly().GetManifestResourceStream("libFetchrActiveItems.DataFiles.Minecraft.en_us.json")))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }

		internal static string ItemNameFromKey(string translationKey)
		{
			if (McLang.TryGetValue(translationKey, out string? value))
			{
				return value;
			}
			else
			{
                return translationKey switch
                {
                    "fetchr.item.blue_trimmed_leather_boots" => "Blue Trimmed Leather Boots",
                    "item.minecraft.light_blue_bundle" => "*** Update McLang from 1.21.2 ***",
                    _ => throw new ArgumentOutOfRangeException(nameof(translationKey), "Translation key not found")
                };
            }
		}

        internal static string CategoryNameFromKey(string translationKey)
        {
			return translationKey switch
			{
				"fetchr.category.apple" => "Apple",
				"fetchr.category.amethyst" => "Amethyst",
				"fetchr.category.basic_iron" => "Basic Iron",
				"fetchr.category.bow" => "Bow",
				"fetchr.category.cactus" => "Cactus",
				"fetchr.category.cherry_grove" => "Cherry",
				"fetchr.category.clay" => "Clay",
				"fetchr.category.copper" => "Copper",
				"fetchr.category.chest_iron" => "Iron with Chest",
				"fetchr.category.compass" => "Compass",
				"fetchr.category.deepslate" => "Deepslate",
				"fetchr.category.diamond" => "Diamond",
				"fetchr.category.dripstone_cave" => "Dripstone",
				"fetchr.category.egg" => "Egg",
				"fetchr.category.nighttime_mob_drops" => "Nighttime Mob Drops",
				"fetchr.category.fish" => "Fish",
				"fetchr.category.flint" => "Flint",
				"fetchr.category.furnace" => "Furnace",
				"fetchr.category.glow_ink" => "Glow Ink",
				"fetchr.category.gold_and_ruined_portal" => "Ruined Portals and Gold",
				"fetchr.category.gunpowder" => "Gunpowder",
				"fetchr.category.ink" => "Ink",
				"fetchr.category.jungle" => "Jungle",
				"fetchr.category.kelp" => "Kelp",
				"fetchr.category.lapis" => "Lapis Lazuli",
				"fetchr.category.leather" => "Leather",
				"fetchr.category.lime" => "Lime",
				"fetchr.category.lush_cave" => "Lush Cave",
				"fetchr.category.milk" => "Milk",
				"fetchr.category.mud" => "Mud",
				"fetchr.category.mushroom" => "Mushroom",
				"fetchr.category.piston" => "Piston",
				"fetchr.category.pumpkin" => "Pumpkin",
				"fetchr.category.rabbit" => "Rabbit",
				"fetchr.category.rail" => "Iron Rails",
				"fetchr.category.redstone" => "Simple Redstone",
				"fetchr.category.sand" => "Sand",
				"fetchr.category.sapling" => "Sapling",
				"fetchr.category.shearable" => "Shearables",
				"fetchr.category.shipwreck" => "Shipwreck Loot",
				"fetchr.category.skeleton_drops" => "Skeleton Drops",
				"fetchr.category.snow" => "Snow",
				"fetchr.category.spider" => "Spider Eye",
				"fetchr.category.taiga" => "Taiga",
				"fetchr.category.wheat" => "Wheat",
				"fetchr.category.wool" => "Wool",

                // Added in 5.1.4
                "fetchr.category.armadillo" => "*** Update from Fetchr 5.1.4 ***",
                "fetchr.category.book" => "*** Update from Fetchr 5.1.4 ***",
                "fetchr.category.mangrove" => "*** Update from Fetchr 5.1.4 ***",
                "fetchr.category.oak" => "*** Update from Fetchr 5.1.4 ***",
                "fetchr.category.roots" => "*** Update from Fetchr 5.1.4 ***",
                "fetchr.category.warm_biome" => "*** Update from Fetchr 5.1.4 ***",

                _ => throw new ArgumentOutOfRangeException(nameof(translationKey), "Translation key not found")
			};
		}
    }
}
