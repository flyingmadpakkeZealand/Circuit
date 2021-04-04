using System.Collections.Generic;

namespace CircuitLib.Components
{
    public class ComponentEndPoint : SolvableUnit
    {
        internal IComponent Input { get; }

        public bool RequestedSignal { get; set; }

        public ComponentEndPoint(IComponent input) : this(input, false) { }

        public ComponentEndPoint(IComponent input, bool requestedSignal)
        {
            input.IncOutput();
            Input = input;
            RequestedSignal = requestedSignal;
        }

        public bool Signal()
        {
            return Input.Signal();
        }

        public override void AutoConfigureConstants(CircuitSolution solution, params bool[] onAny)
        {
            base.AutoConfigureConstants(solution, onAny);
        }

        protected override Dictionary<int, Constant> GetConstants()
        {
            Dictionary<int, Constant> constantsById = new Dictionary<int, Constant>();
            FindConstants(Input, in constantsById);
            return constantsById;
        }
    }
}
