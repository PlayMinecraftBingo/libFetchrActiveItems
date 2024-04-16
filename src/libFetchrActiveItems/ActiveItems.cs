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
				case FetchrVersion.Fetchr50:
					activeItemsPath = "libFetchrActiveItems.v50.command_storage_bingo.dat";
					break;
				case FetchrVersion.Fetchr501:
					activeItemsPath = "libFetchrActiveItems.v501.command_storage_bingo.dat";
					break;
				case FetchrVersion.Fetchr51:
					activeItemsPath = "libFetchrActiveItems.v51.command_storage_fetchr.dat";
					break;
				case FetchrVersion.Fetchr511:
					activeItemsPath = "libFetchrActiveItems.v511.command_storage_fetchr.dat";
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
