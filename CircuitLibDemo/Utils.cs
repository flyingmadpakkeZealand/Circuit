using System;
using System.Collections.Generic;
using System.Text;
using CircuitLib;
using CircuitLib.Components;

namespace CircuitLibDemo
{
    internal static class Utils
    {
        internal static void PrintSegment(string segmentName, Circuit circuit, params bool[] requestedSignals)
        {
            Console.WriteLine($"Solving for {segmentName}: ");
            foreach (CircuitSolution solution in circuit.Solve(requestedSignals))
            {
                PrintSolution(circuit, solution);
                Console.WriteLine("---Raw solution: " + solution.Raw);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        internal static void PrintSolution(Circuit circuit, CircuitSolution solution)
        {
            circuit.AutoConfigureConstants(solution);
            string constantsSignal = "Constants signal: ";
            foreach (Constant constant in circuit.Constants)
            {
                constantsSignal += $"{constant.Signal()} | ";
            }
            Console.WriteLine(constantsSignal);

            string endPointsSignal = "EndPoints signal: ";
            foreach (ComponentEndPoint endPoint in circuit.EndPoints)
            {
                endPointsSignal += $"{endPoint.Signal()} | ";
            }
            Console.WriteLine(endPointsSignal);
        }
    }
}
