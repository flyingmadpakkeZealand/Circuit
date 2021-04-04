using System.Collections.Generic;

namespace CircuitLib.Components
{
    public abstract class SolvableUnit
    {
        private Dictionary<int, Constant> _constantsById;
        private List<Constant> _constants;

        public Dictionary<int, Constant> ConstantsById
        {
            get
            {
                if (!(_constantsById is null)) return _constantsById;
                _constantsById = GetConstants();
                return _constantsById;
            }
        }

        public List<Constant> Constants
        {
            get
            {
                if (!(_constants is null)) return _constants;
                _constants = new List<Constant>(ConstantsById.Values);
                return _constants;
            }
        }

        public virtual void AutoConfigureConstants(CircuitSolution solution, params bool[] onAny)
        {
            if (onAny.Length == 0) onAny = new[] { false };

            int counter = 0;
            foreach (Constant constant in Constants)
            {
                char bit = solution.Solution[constant.Id];
                if (bit == 'a')
                {
                    constant.SetSignal = onAny[counter];
                    counter = (counter + 1) % onAny.Length;
                }
                else
                {
                    constant.SetSignal = bit == 't';
                }
            }
        }

        protected void FindConstants(IComponent input, in Dictionary<int, Constant> constantsById)
        {
            if (input.Type == Constant.CONSTANT)
            {
                Constant constant = (Constant)input;
                constantsById.TryAdd(constant.Id, constant);
                return;
            }

            foreach (IComponent component in input.Inputs)
            {
                FindConstants(component, in constantsById);
            }
        }

        protected abstract Dictionary<int, Constant> GetConstants();
    }
}
