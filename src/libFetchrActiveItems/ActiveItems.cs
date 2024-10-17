using libFetchrActiveItems.DataStructures;
using libFetchrVersion;
using Newtonsoft.Json;
using SharpNBT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace libFetchrActiveItems
{
	public class ActiveItems
	{
		public static List<ItemData> Get(FetchrVersion version)
		{
			return Get(new FetchrVersionData(version));
        }

		public static List<ItemData> Get(FetchrVersionData version)
		{
			string? activeItemsPath = null;

			switch (version.Fetchr)
			{
				case FetchrVersion.Fetchr_5_0:
					activeItemsPath = "libFetchrActiveItems.v5_0.command_storage_bingo.dat";
					break;
				case FetchrVersion.Fetchr_5_0_1:
					activeItemsPath = "libFetchrActiveItems.v5_0_1.command_storage_bingo.dat";
					break;
				case FetchrVersion.Fetchr_5_1:
					activeItemsPath = "libFetchrActiveItems.v5_1.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_1:
					activeItemsPath = "libFetchrActiveItems.v5_1_1.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_2:
					activeItemsPath = "libFetchrActiveItems.v5_1_2.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_3:
					activeItemsPath = "libFetchrActiveItems.v5_1_3.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_4:
					switch (version.Minecraft)
					{
						case MinecraftVersion.Minecraft_1_21:
						case MinecraftVersion.Minecraft_1_21_1:
							activeItemsPath = "libFetchrActiveItems.v5_1_4_pre_1_21_2.command_storage_fetchr.dat";
							break;
						case MinecraftVersion.Minecraft_1_21_2:
                            activeItemsPath = "libFetchrActiveItems.v5_1_4_from_1_21_2.command_storage_fetchr.dat";
                            break;
					}
					break;
			}

			if (activeItemsPath == null) throw new NotImplementedException("activeItemsPath is null");

			Stream? activeItemsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(activeItemsPath) ?? throw new NotImplementedException("activeItemsStream is null");
			List<ItemData>? activeItems = FromStream(activeItemsStream) ?? throw new NotImplementedException("activeItems is null");

			return activeItems;
		}

		private static List<ItemData>? FromStream(Stream stream)
		{
			CompoundTag rootTag = SharpNBTShim.ReadFromStream(stream, FormatOptions.Java);
			CompoundTag dataTag = (CompoundTag)rootTag["data"];
			CompoundTag contentsTag = (CompoundTag)dataTag["contents"];
			CompoundTag itemsTag = (CompoundTag)contentsTag["items"];
            ListTag activeItemsTag = itemsTag.ContainsKey("active_items") ? (ListTag)itemsTag["active_items"] : (ListTag)itemsTag["activeItems"];

			ListTag activeItemsList = new(null, TagType.Compound, activeItemsTag);
			string activeItemsJson = activeItemsList.ToJson()[1..^1];

			return JsonConvert.DeserializeObject<List<ItemData>>(activeItemsJson);
		}
	}
}
