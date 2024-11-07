using System.Collections.Generic;

namespace libFetchrActiveItems.DataStructures
{
    public class CategoryData
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Tags { get; set; }
        public int TotalItemWeight { get; set; }
        public int? Weight { get; set; } = default(int);

        public override string ToString() => Id;

        public CategoryData DeepCopy()
        {
            CategoryData deepCopy = new()
            {
                Name = Name,
                Id = Id,
                Tags = [],
                TotalItemWeight = TotalItemWeight,
                Weight = Weight
            };

            foreach (string tag in Tags) deepCopy.Tags.Add(tag);

            return deepCopy;
        }
    }
}
