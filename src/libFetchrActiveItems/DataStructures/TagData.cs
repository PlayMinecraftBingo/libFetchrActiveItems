namespace libFetchrActiveItems.DataStructures
{
	public class TagData
    {
        public DisplayData Display;

        internal TagData DeepCopy() => new()
        {
            Display = Display.DeepCopy()
        };
    }
}
