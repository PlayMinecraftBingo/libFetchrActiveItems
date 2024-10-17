using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace libFetchrActiveItems.DataStructures
{
    public class ItemData
    {
        public int WeightDenom;
        public string Id;
        public string DetectCommand;
        public string ClearCommand;
        public ItemInnerData Item;
        public string Icon;
        public int Weight;
        public List<CategoryData> Categories;
        public List<CategoryData> ActiveCategories;
        public string TextComponent;
        public int WeightNom;
        public string? CommandArgument;
        public string? Translation;

        public override string ToString() => Id;

        public string? TranslationKey
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
