using System;

namespace CircuitLib.Components
{
    public abstract class BaseComponentBound : IComponentBound
    {
        private int _outPuts;

        public int MaxOutputs { get; }

        protected BaseComponentBound()
        {
            MaxOutputs = 1;
        }

        protected BaseComponentBound(int maxOutputs)
        {
            MaxOutputs = maxOutputs;
        }

        void IComponentBound.IncOutput()
        {
            _outPuts++;
            if (_outPuts > MaxOutputs)
            {
                throw new ArgumentOutOfRangeException(GetType().ToString(), $"This component has more outputs than what is supported. {nameof(MaxOutputs)}: {MaxOutputs}");
            }
        }
    }
}
