using System.Collections.Generic;
using System.Diagnostics;

namespace libFetchrActiveItems.DataStructures
{
	public class DisplayData
    {
        public string Name;
        public List<string> Lore;

        [DebuggerStepThrough()]
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
