using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace libFetchrActiveItems.DataStructures
{
    public class ItemData
    {
        public int WeightDenom { get; set; }
        public string Id { get; set; }
        public string DetectCommand { get; set; }
        public string ClearCommand { get; set; }
        public ItemInnerData Item { get; set; }
        public string Icon { get; set; }
        public int Weight { get; set; }
        public List<CategoryData> Categories { get; set; }
        public List<CategoryData> ActiveCategories { get; set; }
        public string TextComponent { get; set; }
        public int WeightNom { get; set; }
        public string? CommandArgument { get; set; }
        public string? Translation { get; set; }

        public override string ToString() => Id;

        public string TranslationName
        {
            get
            {
                return Translate.ItemNameFromKey(TranslationKey);
            }
        }

        public string TranslationKey
        {
            get
            {
                return Translation ?? GetTranslationKeyFromTextComponent();
            }
        }

        private string? GetTranslationKeyFromTextComponent()
        {
            return JObject.Parse(TextComponent).Value<string>("translate");
        }

        public ItemData DeepCopy()
        {
            ItemData deepCopy = new()
            {
                WeightDenom = WeightDenom,
                Id = Id,
                DetectCommand = DetectCommand,
                ClearCommand = ClearCommand,
                Item = Item.DeepCopy(),
                Icon = Icon,
                Weight = Weight,
                Categories = [],
                ActiveCategories = [],
                TextComponent = TextComponent,
                WeightNom = WeightNom,
                CommandArgument = CommandArgument,
                Translation = Translation
            };

            foreach (CategoryData category in Categories) deepCopy.Categories.Add(category.DeepCopy());
            foreach (CategoryData activeCategory in ActiveCategories) deepCopy.ActiveCategories.Add(activeCategory.DeepCopy());

            return deepCopy;
        }
    }
}
