namespace libFetchrActiveItems.DataStructures
{
    public record FetchrItem
    {
        public string FetchrId { get; init; }
        public string MinecraftId { get; init; }
        public string Label { get; init; }
        public string Icon { get; init; }

        public FetchrItem(ItemData item)
        {
            FetchrId = item.Id;
            if (FetchrId.StartsWith("bingo:")) FetchrId = FetchrId[6..];
            if (FetchrId.StartsWith("fetchr:")) FetchrId = FetchrId[7..];

            MinecraftId = item.Item.Id;
            if (MinecraftId.StartsWith("minecraft:")) MinecraftId = MinecraftId[10..];

            Label = item.TranslationName;
            Icon = item.Icon;
        }
    }
}