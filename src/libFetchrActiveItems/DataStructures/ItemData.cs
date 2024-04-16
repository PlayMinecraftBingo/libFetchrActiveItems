using System.Collections.Generic;
using System.Diagnostics;

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

        public override string ToString() => Id;

        [DebuggerStepThrough()]
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
                WeightNom = WeightNom
            };

            foreach (CategoryData category in Categories) deepCopy.Categories.Add(category.DeepCopy());
            foreach (CategoryData activeCategory in ActiveCategories) deepCopy.ActiveCategories.Add(activeCategory.DeepCopy());

            return deepCopy;
        }
    }
}
