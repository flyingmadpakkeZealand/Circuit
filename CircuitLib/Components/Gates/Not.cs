namespace CircuitLib.Components.Gates
{
    public class Not : BaseComponentBound, IComponent
    {
        internal const string NOT = nameof(NOT);

        private readonly IComponent _input;

        string IComponent.Type => NOT;
        IComponent[] IComponent.Inputs => new []{_input};

        public Not(IComponent input)
        {
            input.IncOutput();
            _input = input;
        }


        public bool Signal()
        {
            return !_input.Signal();
        }

        
    }
}
