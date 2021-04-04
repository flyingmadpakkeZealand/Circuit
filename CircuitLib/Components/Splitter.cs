namespace CircuitLib.Components
{
    public class Splitter : BaseComponentBound, IComponent
    {
        internal const string SPLITTER = nameof(SPLITTER);

        private readonly IComponent _input;

        string IComponent.Type => SPLITTER;
        IComponent[] IComponent.Inputs => new []{_input};

        public Splitter(IComponent input, int maxOutputs = 2) : base(maxOutputs)
        {
            input.IncOutput();
            _input = input;
        }

        public bool Signal()
        {
            return _input.Signal();
        }
    }
}
