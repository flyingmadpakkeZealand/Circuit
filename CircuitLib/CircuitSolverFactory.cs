using System;

namespace CircuitLib
{
    public static class CircuitSolverFactory
    {
        public static ICircuitSolver CreateCircuitSolver(string solverName)
        {
            switch (solverName)
            {
                case nameof(CircuitSolverV2): return new CircuitSolverV2();
                case nameof(CircuitSolverV1): return new CircuitSolverV1();
            }

            throw new ArgumentException( $"No such {solverName} solver.", nameof(solverName));
        }
    }
}
