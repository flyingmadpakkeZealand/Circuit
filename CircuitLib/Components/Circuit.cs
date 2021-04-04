using System.Collections.Generic;

namespace CircuitLib.Components
{
    public class Circuit : SolvableUnit
    {
        public ComponentEndPoint[] EndPoints { get; }

        public Circuit(params ComponentEndPoint[] endPoints)
        {
            EndPoints = endPoints;
        }

        public IEnumerable<CircuitSolution> Solve(params bool[] requestedSignals)
        {
            int counter = 0;
            foreach (bool requestedSignal in requestedSignals)
            {
                if (counter < EndPoints.Length)
                {
                    EndPoints[counter++].RequestedSignal = requestedSignal;
                }
                else
                {
                    break;
                }
            }

            ICircuitSolver solver = CircuitSolverFactory.CreateCircuitSolver(nameof(CircuitSolverV2));
            return solver.SolveDistinct(EndPoints);
        }

        public override void AutoConfigureConstants(CircuitSolution solution, params bool[] onAny)
        {
            base.AutoConfigureConstants(solution, onAny);
        }

        protected override Dictionary<int, Constant> GetConstants()
        {
            Dictionary<int, Constant> constantsById = new Dictionary<int, Constant>();
            foreach (ComponentEndPoint endPoint in EndPoints)
            {
                FindConstants(endPoint.Input, in constantsById);
            }

            return constantsById;
        }
    }
}

