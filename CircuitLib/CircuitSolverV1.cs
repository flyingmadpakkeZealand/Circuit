using System;
using System.Collections.Generic;
using CircuitLib.Components;
using CircuitLib.Components.Gates;

namespace CircuitLib
{
    public class CircuitSolverV1 : ICircuitSolver
    {
        public IEnumerable<string> SolveRaw(params ComponentEndPoint[] endPoints)
        {
            return ChainEndPoints(endPoints, 0);
        }

        public IEnumerable<string> SolveRawDistinct(params ComponentEndPoint[] endPoints)
        {
            HashSet<string> solutions = new HashSet<string>();

            foreach (string solution in ChainEndPoints(endPoints, 0))
            {
                if (solutions.Add(solution))
                {
                    yield return solution;
                }
            }
        }

        public IEnumerable<CircuitSolution> SolveDistinct(params ComponentEndPoint[] endPoints)
        {
            foreach (string solution in SolveRawDistinct(endPoints))
            {
                yield return new CircuitSolution(solution);
            }
        }

        private IEnumerable<string> ChainEndPoints(ComponentEndPoint[] endPoints, int pointer)
        {
            if (pointer == endPoints.Length)
            {
                yield return "";
                yield break;
            }

            ComponentEndPoint endPoint = endPoints[pointer];
            foreach (string solutionBitsA in Solve(endPoint.RequestedSignal, endPoint.Input))
            {
                foreach (string solutionBitsB in ChainEndPoints(endPoints, pointer + 1))
                {
                    yield return solutionBitsA + solutionBitsB;
                }
            }
        }

        protected virtual IEnumerable<string> Solve(bool requestedSignal, IComponent component)
        {
            switch (component.Type)
            {
                case And.AND: return SolveAndGate(requestedSignal, component);
                case Or.OR: return SolveOrGate(requestedSignal, component);
                case NAnd.NAND: return SolveNAndGate(requestedSignal, component);
                case NOr.NOR: return SolveNOrGate(requestedSignal, component);
                case XOr.XOR: return SolveXOrGate(requestedSignal, component);
                case XNOr.XNOR: return SolveXNOrGate(requestedSignal, component);
                case Not.NOT: return SolveNotGate(requestedSignal, component);
                case Constant.CONSTANT: return SolveConstant(requestedSignal, component);
            }

            throw new ArgumentException("Unsupported component type.", component.Type);
        }

        private IEnumerable<string> SolveAndGate(bool requestedSignal, IComponent andGate)
        {
            IComponent leftInput = andGate.Inputs[0];
            IComponent rightInput = andGate.Inputs[1];

            if (requestedSignal)
            {
                return RequestLeftAndRightInputs(leftInput, true, rightInput, true);
            }

            return RequestLeftThenRightInputs(leftInput, false, rightInput, false);
        }

        private IEnumerable<string> SolveOrGate(bool requestedSignal, IComponent orGate)
        {
            IComponent leftInput = orGate.Inputs[0];
            IComponent rightInput = orGate.Inputs[1];

            if (requestedSignal)
            {
                return RequestLeftThenRightInputs(leftInput, true, rightInput, true);
            }

            return RequestLeftAndRightInputs(leftInput, false, rightInput, false);
        }

        private IEnumerable<string> SolveNAndGate(bool requestedSignal, IComponent nAndGate)
        {
            IComponent leftInput = nAndGate.Inputs[0];
            IComponent rightInput = nAndGate.Inputs[1];

            if (requestedSignal)
            {
                return RequestLeftThenRightInputs(leftInput, false, rightInput, false);
            }

            return RequestLeftAndRightInputs(leftInput, true, rightInput, true);
        }

        private IEnumerable<string> SolveNOrGate(bool requestedSignal, IComponent nOrGate)
        {
            IComponent leftInput = nOrGate.Inputs[0];
            IComponent rightInput = nOrGate.Inputs[1];

            if (requestedSignal)
            {
                return RequestLeftAndRightInputs(leftInput, false, rightInput, false);
            }

            return RequestLeftThenRightInputs(leftInput, true, rightInput, true);
        }

        private IEnumerable<string> SolveXOrGate(bool requestedSignal, IComponent xOrGate)
        {
            IComponent leftInput = xOrGate.Inputs[0];
            IComponent rightInput = xOrGate.Inputs[1];

            if (requestedSignal)
            {
                return RequestAllLeftAndRightInputs(leftInput, true, rightInput, false);
            }

            return RequestAllLeftAndRightInputs(leftInput, false, rightInput, false);
        }

        private IEnumerable<string> SolveXNOrGate(bool requestedSignal, IComponent xNOrgate)
        {
            IComponent leftInput = xNOrgate.Inputs[0];
            IComponent rightInput = xNOrgate.Inputs[1];

            if (requestedSignal)
            {
                return RequestAllLeftAndRightInputs(leftInput, false, rightInput, false);
            }

            return RequestAllLeftAndRightInputs(leftInput, true, rightInput, false);
        }

        private IEnumerable<string> SolveNotGate(bool requestedSignal, IComponent notGate)
        {
            IComponent input = notGate.Inputs[0];

            return Solve(!requestedSignal, input);
        }

        private IEnumerable<string> SolveConstant(bool requestedSignal, IComponent constant)
        {
            yield return ((Constant) constant).Id + (requestedSignal ? "t" : "f");
        }

        protected IEnumerable<string> RequestLeftAndRightInputs(IComponent leftInput, bool leftInputRequest,
            IComponent rightInput, bool rightInputRequest)
        {
            bool cached = false;
            List<string> cache = new List<string>();

            foreach (string leftSolution in Solve(leftInputRequest, leftInput))
            {
                if (!cached)
                {
                    foreach (string rightSolution in Solve(rightInputRequest, rightInput))
                    {
                        yield return leftSolution + rightSolution;
                        cache.Add(rightSolution);
                    }

                    cached = true;
                }
                else
                {
                    foreach (string rightSolution in cache)
                    {
                        yield return leftSolution + rightSolution;
                    }
                }
            }
        }

        protected IEnumerable<string> RequestLeftThenRightInputs(IComponent leftInput, bool leftInputRequest,
            IComponent rightInput, bool rightInputRequest)
        {
            string anyRight = null;

            foreach (string rightSolution in SolveAnySignal(rightInput))
            {
                anyRight = rightSolution;
            }

            foreach (string leftSolution in Solve(leftInputRequest, leftInput))
            {
                yield return leftSolution + anyRight;
            }


            string anyLeft = null;

            foreach (string leftSolution in SolveAnySignal(leftInput))
            {
                anyLeft = leftSolution;
            }

            foreach (string rightSolution in Solve(rightInputRequest, rightInput))
            {
                yield return anyLeft + rightSolution;
            }
        }

        protected IEnumerable<string> RequestAllLeftAndRightInputs(IComponent leftInput, bool leftInputRequest,
            IComponent rightInput, bool rightInputRequest)
        {
            foreach (string solution in RequestLeftAndRightInputs(leftInput, leftInputRequest, rightInput, rightInputRequest))
            {
                yield return solution;
            }

            foreach (string solution in RequestLeftAndRightInputs(leftInput, !leftInputRequest, rightInput, !rightInputRequest))
            {
                yield return solution;
            }
        }

        protected virtual IEnumerable<string> SolveAnySignal(IComponent component)
        {
            if (component.Type == Constant.CONSTANT)
            {
                yield return ((Constant) component).Id + "a";
                yield break;
            }

            if (component.Type == Not.NOT)
            {
                foreach (string any in SolveAnySignal(component.Inputs[0]))
                {
                    yield return any;
                }
                yield break;
            }

            //Hard coded two inputs:
            foreach (string anyLeft in SolveAnySignal(component.Inputs[0]))
            {
                foreach (string anyRight in SolveAnySignal(component.Inputs[1]))
                {
                    yield return anyLeft + anyRight;
                }
            }
        }
    }
}
