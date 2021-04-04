namespace CircuitLib.Components.Gates
{
    public class Or : BaseComponentBound, IComponent
    {
        internal const string OR = nameof(OR);

        private readonly IComponent _inputLeft;
        private readonly IComponent _inputRight;

        string IComponent.Type => OR;
        IComponent[] IComponent.Inputs => new []{_inputLeft, _inputRight};

        public Or(IComponent inputLeft, IComponent inputRight)
        {
            inputLeft.IncOutput();
            inputRight.IncOutput();
            _inputLeft = inputLeft;
            _inputRight = inputRight;
        }

        public bool Signal()
        {
            return _inputLeft.Signal() || _inputRight.Signal();
        }
    }
}
