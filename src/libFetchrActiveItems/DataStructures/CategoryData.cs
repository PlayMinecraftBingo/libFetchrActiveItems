using System.Collections.Generic;

namespace libFetchrActiveItems.DataStructures
{
	public class CategoryData
    {
        public string Name;
        public string Id;
        public List<string> Tags;
        public int TotalItemWeight;
        public int Weight;

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
