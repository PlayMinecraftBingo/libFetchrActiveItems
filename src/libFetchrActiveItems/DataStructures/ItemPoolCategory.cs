using Newtonsoft.Json.Linq;

namespace libFetchrActiveItems.DataStructures
{
    public record ItemPoolCategory
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string TranslationKey { get; init; }
        public string TranslationName { get; init; }

        public ItemPoolCategory(string id, string name)
        {
            Id = id;
            Name = name;
            TranslationKey = GetTranslationKeyFromName();
            TranslationName = Translate.CategoryNameFromKey(TranslationKey);
        }

        private string? GetTranslationKeyFromName()
        {
            return JObject.Parse(Name).Value<string>("translate");
        }

        public static implicit operator ItemPoolCategory(CategoryData c) => new(c.Id, c.Name);
    }
}