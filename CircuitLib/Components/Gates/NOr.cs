namespace CircuitLib.Components.Gates
{
    public class NOr : BaseComponentBound, IComponent
    {
        internal const string NOR = nameof(NOR);

        private readonly IComponent _inputLeft;
        private readonly IComponent _inputRight;

        string IComponent.Type => NOR;
        IComponent[] IComponent.Inputs => new []{_inputLeft, _inputRight};

        public NOr(IComponent inputLeft, IComponent inputRight)
        {
            inputLeft.IncOutput();
            inputRight.IncOutput();
            _inputLeft = inputLeft;
            _inputRight = inputRight;
        }

        public bool Signal()
        {
            return !(_inputLeft.Signal() || _inputRight.Signal());
        }
    }
}
