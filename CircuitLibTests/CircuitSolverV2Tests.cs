using System.Collections.Generic;
using CircuitLib;
using CircuitLib.Components;
using CircuitLib.Components.Gates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitLibTests
{
    [TestClass]
    public class CircuitSolverV2Tests
    {
        [TestMethod]
        public void OneOfGatesTests()
        {
            ComponentEndPoint and = new ComponentEndPoint(new And(new Constant(), new Constant()));
            CheckSolutions(and);

            ComponentEndPoint or = new ComponentEndPoint(new Or(new Constant(), new Constant()));
            CheckSolutions(or);

            ComponentEndPoint xOr = new ComponentEndPoint(new XOr(new Constant(), new Constant()));
            CheckSolutions(xOr);

            ComponentEndPoint nAnd = new ComponentEndPoint(new NAnd(new Constant(), new Constant()));
            CheckSolutions(nAnd);

            ComponentEndPoint nOr = new ComponentEndPoint(new NOr(new Constant(), new Constant()));
            CheckSolutions(nOr);

            ComponentEndPoint xNOr = new ComponentEndPoint(new XNOr(new Constant(), new Constant()));
            CheckSolutions(xNOr);

            ComponentEndPoint not = new ComponentEndPoint(new Not(new Constant()));
            CheckSolutions(not);
        }

        [TestMethod]
        public void SplitterTests()
        {
            Splitter s1 = new Splitter(new Constant());
            ComponentEndPoint and = new ComponentEndPoint(new And(s1, s1));
            CheckSolutions(and);

            Splitter s2 = new Splitter(new Constant());
            ComponentEndPoint or = new ComponentEndPoint(new And(s2, s2));
            CheckSolutions(or);

            Splitter quadrupleS3 = new Splitter(new Constant(), 4);
            ComponentEndPoint tripleOrQuadrupleSplitter = new ComponentEndPoint(new Or(new Or(quadrupleS3, quadrupleS3), new Or(quadrupleS3, quadrupleS3)));
            CheckSolutions(tripleOrQuadrupleSplitter);

            Splitter s4 = new Splitter(new Constant());
            ComponentEndPoint orDoubleAndMiddleSplitter = new ComponentEndPoint(new Or(new And(new Constant(), s4), new And(new Constant(), s4)));
            CheckSolutions(orDoubleAndMiddleSplitter);

            Splitter tripleS5 = new Splitter(new Constant(), 3);
            ComponentEndPoint andOrNAndNOrXNOrXOrNot = new ComponentEndPoint(new And(new Or(new NOr(new Constant(), tripleS5), new XNOr(new Constant(), new Constant())), new NAnd(new XOr(new Constant(), tripleS5), new Not(tripleS5))));
            CheckSolutions(andOrNAndNOrXNOrXOrNot);
        }

        private void CheckSolutions(ComponentEndPoint endPoint)
        {
            CircuitSolverV2 solverV2 = new CircuitSolverV2();
            endPoint.RequestedSignal = true;
            foreach (CircuitSolution solution in solverV2.SolveDistinct(endPoint))
            {
                TestAllVariations(solution, endPoint, true);
            }

            endPoint.RequestedSignal = false;
            foreach (CircuitSolution solution in solverV2.SolveDistinct(endPoint))
            {
                TestAllVariations(solution, endPoint, false);
            }
        }

        private void TestAllVariations(CircuitSolution solution, ComponentEndPoint endPoint, bool testingFor)
        {
            Dictionary<int, Constant> constantsById = endPoint.ConstantsById;
            List<KeyValuePair<int, char>> bitsById = new List<KeyValuePair<int, char>>(solution.Solution);
            RunTests(0);

            void RunTests(int pointer)
            {
                if (pointer == bitsById.Count)
                {
                    if (testingFor)
                    {
                        Assert.IsTrue(endPoint.Signal());
                    }
                    else
                    {
                        Assert.IsFalse(endPoint.Signal());
                    }
                    return;
                }

                KeyValuePair<int, char> bitById = bitsById[pointer];
                Constant constant = constantsById[bitById.Key];
                char bit = bitById.Value;
                if (bit == 'a')
                {
                    constant.SetSignal = false;
                    RunTests(pointer+1);
                    constant.SetSignal = true;
                    RunTests(pointer+1);
                }
                else
                {
                    constant.SetSignal = bit == 't';
                    RunTests(pointer+1);
                }
            }
        }
    }
}
