namespace CircuitLib.Components.Gates
{
    public class And : BaseComponentBound, IComponent
    {
        internal const string AND = nameof(AND);

        private readonly IComponent _inputLeft;
        private readonly IComponent _inputRight;

        string IComponent.Type => AND;
        IComponent[] IComponent.Inputs => new []{_inputLeft, _inputRight};

        public And(IComponent inputLeft, IComponent inputRight)
        {
            inputLeft.IncOutput();
            inputRight.IncOutput();
            _inputLeft = inputLeft;
            _inputRight = inputRight;
        }

        public bool Signal()
        {
            return _inputLeft.Signal() && _inputRight.Signal();
        }
    }
}
