namespace CircuitLib.Components.Gates
{
    public class XNOr : BaseComponentBound, IComponent
    {
        internal const string XNOR = nameof(XNOR);

        private readonly IComponent _inputLeft;
        private readonly IComponent _inputRight;

        string IComponent.Type => XNOR;
        IComponent[] IComponent.Inputs => new []{_inputLeft, _inputRight};

        public XNOr(IComponent inputLeft, IComponent inputRight)
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

            return !((outLeft || outRight) && !(outLeft && outRight));
        }
    }
}
