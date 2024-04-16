using System.Diagnostics;

namespace libFetchrActiveItems.DataStructures
{
	public class TagData
    {
        public DisplayData Display;

        [DebuggerStepThrough()]
        internal TagData DeepCopy() => new()
        {
            Display = Display.DeepCopy()
        };
    }
}
