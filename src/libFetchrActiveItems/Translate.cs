using libFetchrActiveItems.DataStructures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace libFetchrActiveItems
{
	public class Translate
	{
		private static readonly Dictionary<string, string> McLang = LoadMcLang();

		public static string? ItemName(string itemId)
		{
			if (itemId == "fetchr.item.blue_trimmed_leather_boots") return "Blue Trimmed Leather Boots";

			if (itemId.Contains(".")) return McLang.GetValueOrDefault(itemId, null);

			string adjustedItemId = "item.minecraft." + itemId;
			string? result = McLang.GetValueOrDefault(adjustedItemId, null);
			if (result != null) return result;

			adjustedItemId = "block.minecraft." + itemId;
			result = McLang.GetValueOrDefault(adjustedItemId, null);
			return result;
		}

		public static string ItemName(ItemData item)
		{
			JObject textComponent = JObject.Parse(item.TextComponent);
			string itemId = textComponent.Value<string>("translate");

			return ItemName(itemId) ?? item.Item.Id;
		}

		private static Dictionary<string, string> LoadMcLang()
		{
			using (StreamReader reader = new(Assembly.GetExecutingAssembly().GetManifestResourceStream("libFetchrActiveItems.Minecraft.en_us.json")))
			{
				string json = reader.ReadToEnd();
				return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
			}
		}

		public static string CategoryName(string categoryId)
		{
			return categoryId switch
			{
				"fetchr:apple" => "Apple",
				"fetchr:amethyst" => "Amethyst",
				"fetchr:basic_iron" => "Basic Iron",
				"fetchr:bow" => "Bow",
				"fetchr:cactus" => "Cactus",
				"fetchr:cherry_grove" => "Cherry",
				"fetchr:clay" => "Clay",
				"fetchr:copper" => "Copper",
				"fetchr:chest_iron" => "Iron with Chest",
				"fetchr:compass" => "Compass",
				"fetchr:deepslate" => "Deepslate",
				"fetchr:diamond" => "Diamond",
				"fetchr:dripstone_cave" => "Dripstone",
				"fetchr:egg" => "Egg",
				"fetchr:nighttime_mob_drops" => "Nighttime Mob Drops",
				"fetchr:fish" => "Fish",
				"fetchr:flint" => "Flint",
				"fetchr:furnace" => "Furnace",
				"fetchr:glow_ink" => "Glow Ink",
				"fetchr:gold_and_ruined_portal" => "Ruined Portals and Gold",
				"fetchr:gunpowder" => "Gunpowder",
				"fetchr:ink" => "Ink",
				"fetchr:jungle" => "Jungle",
				"fetchr:kelp" => "Kelp",
				"fetchr:lapis" => "Lapis Lazuli",
				"fetchr:leather" => "Leather",
				"fetchr:lime" => "Lime",
				"fetchr:lush_cave" => "Lush Cave",
				"fetchr:milk" => "Milk",
				"fetchr:mud" => "Mud",
				"fetchr:mushroom" => "Mushroom",
				"fetchr:piston" => "Piston",
				"fetchr:pumpkin" => "Pumpkin",
				"fetchr:rabbit" => "Rabbit",
				"fetchr:rail" => "Iron Rails",
				"fetchr:redstone" => "Simple Redstone",
				"fetchr:sand" => "Sand",
				"fetchr:sapling" => "Sapling",
				"fetchr:shearable" => "Shearables",
				"fetchr:shipwreck" => "Shipwreck Loot",
				"fetchr:skeleton_drops" => "Skeleton Drops",
				"fetchr:snow" => "Snow",
				"fetchr:spider" => "Spider Eye",
				"fetchr:taiga" => "Taiga",
				"fetchr:wheat" => "Wheat",
				"fetchr:wool" => "Wool",
				_ => categoryId
			};
		}
	}
}
