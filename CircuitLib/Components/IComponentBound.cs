namespace CircuitLib.Components
{
    public interface IComponentBound
    {
        int MaxOutputs { get; }

        internal void IncOutput();
    }
}
