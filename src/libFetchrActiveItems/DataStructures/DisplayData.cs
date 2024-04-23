using System.Collections.Generic;

namespace libFetchrActiveItems.DataStructures
{
	public class DisplayData
    {
        public string Name;
        public List<string> Lore;

        internal DisplayData DeepCopy()
        {
            DisplayData deepCopy = new()
            {
                Name = Name,
                Lore = []
            };

            foreach (var loreItem in Lore) deepCopy.Lore.Add(loreItem);

            return deepCopy;
        }
    }
}
