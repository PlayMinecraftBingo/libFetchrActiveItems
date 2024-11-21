using libFetchrActiveItems.ContractResolvers;
using libFetchrActiveItems.DataStructures;
using libFetchrVersion;
using libMinecraftVersion;
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
		public static List<ItemData> Get(FetchrVersionData version)
		{
			string? activeItemsPath = null;

			switch (version.Fetchr)
			{
				case FetchrVersion.Fetchr_5_0:
					activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_0.command_storage_bingo.dat";
					break;
				case FetchrVersion.Fetchr_5_0_1:
					activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_0_1.command_storage_bingo.dat";
					break;
				case FetchrVersion.Fetchr_5_1:
					activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_1.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_1:
					activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_1_1.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_2:
					activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_1_2.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_3:
					activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_1_3.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr_5_1_4:
					switch (version.Minecraft)
					{
						case FetchrMinecraftVersion.Minecraft_1_21:
						case FetchrMinecraftVersion.Minecraft_1_21_1:
							activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_1_4_pre_1_21_2.command_storage_fetchr.dat";
							break;
                        case FetchrMinecraftVersion.Minecraft_1_21_2:
                        case FetchrMinecraftVersion.Minecraft_1_21_3:
                            activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_1_4_from_1_21_2.command_storage_fetchr.dat";
                            break;
                        case FetchrMinecraftVersion.Minecraft_1_21_4:
                            activeItemsPath = "libFetchrActiveItems.DataFiles.Fetchr.v5_1_4_from_1_21_4.command_storage_fetchr.dat";
                            break;
                    }
                    break;
			}

			if (activeItemsPath == null) throw new NotImplementedException("activeItemsPath is null");

			Stream? activeItemsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(activeItemsPath) ?? throw new NotImplementedException("activeItemsStream is null");
			List<ItemData>? activeItems = FromStream(activeItemsStream, version) ?? throw new NotImplementedException("activeItems is null");

			return activeItems;
		}

		private static List<ItemData>? FromStream(Stream stream, FetchrVersionData version)
		{
			CompoundTag rootTag = SharpNBTShim.ReadFromStream(stream, FormatOptions.Java);
			CompoundTag dataTag = (CompoundTag)rootTag["data"];
			CompoundTag contentsTag = (CompoundTag)dataTag["contents"];
			CompoundTag itemsTag = (CompoundTag)contentsTag["items"];
            ListTag activeItemsTag = itemsTag.ContainsKey("active_items") ? (ListTag)itemsTag["active_items"] : (ListTag)itemsTag["activeItems"];

			ListTag activeItemsList = new(null, TagType.Compound, activeItemsTag);
			string activeItemsJson = activeItemsList.ToJson()[1..^1];

			return JsonConvert.DeserializeObject<List<ItemData>>(activeItemsJson, new JsonSerializerSettings() { ContractResolver = GetContractResolver(version) });
		}

        private static ContractResolver_v5_1_4? GetContractResolver(FetchrVersionData version)
        {
            return version.Fetchr switch
            {
                FetchrVersion.Fetchr_5_1_4 => new ContractResolver_v5_1_4(),
                _ => null
            };
        }
    }
}
