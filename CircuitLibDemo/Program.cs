using System;
using CircuitLib;
using CircuitLib.Components;
using CircuitLib.Components.Gates;

namespace CircuitLibDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Splitter sBit1 = new Splitter(new Constant());
            Splitter sBit2 = new Splitter(new Constant());
            ComponentEndPoint halfAdderBit1 = new ComponentEndPoint(new XOr(sBit1, sBit2));
            ComponentEndPoint halfAdderBit2 = new ComponentEndPoint(new And(sBit1, sBit2));
            Circuit halfAdder = new Circuit(halfAdderBit1, halfAdderBit2);

            Console.WriteLine("Solving for 00: ");
            foreach (CircuitSolution solution in halfAdder.Solve(false, false))
            {
                PrintSolution(halfAdder, solution);
                Console.WriteLine("---Raw solution: " + solution.Raw);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Solving for 10: ");
            foreach (CircuitSolution solution in halfAdder.Solve(true, false))
            {
                PrintSolution(halfAdder, solution);
                Console.WriteLine("---Raw solution: " + solution.Raw);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Solving for 01: ");
            foreach (CircuitSolution solution in halfAdder.Solve(false, true))
            {
                PrintSolution(halfAdder, solution);
                Console.WriteLine("---Raw solution: " + solution.Raw);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Solving for 11: ");
            foreach (CircuitSolution solution in halfAdder.Solve(true, true))
            {
                PrintSolution(halfAdder, solution);
                Console.WriteLine("---Raw solution: " + solution.Raw);
            }
        }

        static void PrintSolution(Circuit circuit, CircuitSolution solution)
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
