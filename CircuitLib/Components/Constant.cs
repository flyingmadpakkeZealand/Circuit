namespace CircuitLib.Components
{
    public class Constant : BaseComponentBound, IComponent
    {
        internal const string CONSTANT = nameof(CONSTANT);
        private static int _idCount;
        private static readonly object IdCountLock = new object();

        private bool _constSignal;

        string IComponent.Type => CONSTANT;
        IComponent[] IComponent.Inputs => null;

        public int Id { get; }

        public bool SetSignal
        {
            set => _constSignal = value;
        }

        public Constant() : this(false) { }

        public Constant(bool constSignal)
        {
            _constSignal = constSignal;
            lock (IdCountLock) Id = _idCount++;
        }

        public bool Signal()
        {
            return _constSignal;
        }
    }
}
