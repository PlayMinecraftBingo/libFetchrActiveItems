using Newtonsoft.Json.Linq;

namespace libFetchrActiveItems.DataStructures
{
    public record ItemPoolItem
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string TranslationKey { get; init; }
        public string TranslationName { get; init; }
        public int Weight { get; init; }

        public ItemPoolItem(string translationName)
        {
            TranslationName = translationName;
        }

        public static implicit operator ItemPoolItem(ItemData i) => new(i.TranslationName);
    }
}