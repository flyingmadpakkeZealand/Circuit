using System.Collections.Generic;
using CircuitLib.Components;

namespace CircuitLib
{
    public interface ICircuitSolver
    { 
        IEnumerable<string> SolveRaw(params ComponentEndPoint[] endPoints);

        IEnumerable<string> SolveRawDistinct(params ComponentEndPoint[] endPoints);

        IEnumerable<CircuitSolution> SolveDistinct(params ComponentEndPoint[] endPoints);
    }
}
