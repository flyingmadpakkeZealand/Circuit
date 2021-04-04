namespace CircuitLib.Components
{
    public interface IComponent : IComponentBound
    {
        bool Signal();

        internal string Type { get; }

        internal IComponent[] Inputs { get; }
    }
}
