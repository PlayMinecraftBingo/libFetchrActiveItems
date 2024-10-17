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
			string? activeItemsPath = null;

			switch (version)
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
			ListTag activeItemsTag = (ListTag)itemsTag["activeItems"];

			ListTag activeItemsList = new(null, TagType.Compound, activeItemsTag);
			string activeItemsJson = activeItemsList.ToJson()[1..^1];

			return JsonConvert.DeserializeObject<List<ItemData>>(activeItemsJson);
		}
	}
}
