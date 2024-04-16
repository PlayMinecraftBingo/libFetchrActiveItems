using System.Diagnostics;

namespace libFetchrActiveItems.DataStructures
{
	public class ItemInnerData
    {
        public string Id;
        public int Count;
        public TagData Tag;

        public override string ToString() => Id;

        [DebuggerStepThrough()]
        internal ItemInnerData DeepCopy() => new()
        {
            Id = Id,
            Count = Count,
            Tag = Tag.DeepCopy()
        };
    }
}
