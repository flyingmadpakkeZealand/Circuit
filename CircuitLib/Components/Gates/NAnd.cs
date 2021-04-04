namespace CircuitLib.Components.Gates
{
    public class NAnd : BaseComponentBound, IComponent
    {
        internal const string NAND = nameof(NAND);

        private readonly IComponent _inputLeft;
        private readonly IComponent _inputRight;

        string IComponent.Type => NAND;
        IComponent[] IComponent.Inputs => new []{_inputLeft, _inputRight};

        public NAnd(IComponent inputLeft, IComponent inputRight)
        {
            inputLeft.IncOutput();
            inputRight.IncOutput();
            _inputLeft = inputLeft;
            _inputRight = inputRight;
        }

        public bool Signal()
        {
            return !(_inputLeft.Signal() && _inputRight.Signal());
        }

        
    }
}
