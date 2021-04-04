namespace CircuitLib.Components.Gates
{
    public class XOr : BaseComponentBound, IComponent
    {
        internal const string XOR = nameof(XOR);

        private readonly IComponent _inputLeft;
        private readonly IComponent _inputRight;

        string IComponent.Type => XOR;
        IComponent[] IComponent.Inputs => new []{_inputLeft, _inputRight};

        public XOr(IComponent inputLeft, IComponent inputRight)
        {
            inputLeft.IncOutput();
            inputRight.IncOutput();
            _inputLeft = inputLeft;
            _inputRight = inputRight;
        }

        public bool Signal()
        {
            bool outLeft = _inputLeft.Signal();
            bool outRight = _inputRight.Signal();

            return (outLeft || outRight) && !(outLeft && outRight);
        }
    }
}
