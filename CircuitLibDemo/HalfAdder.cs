using System;
using System.Collections.Generic;
using System.Text;
using CircuitLib;
using CircuitLib.Components;
using CircuitLib.Components.Gates;
using static CircuitLibDemo.Utils;

namespace CircuitLibDemo
{
    public class HalfAdder
    {
        public void RunExample()
        {
            Splitter sBit1 = new Splitter(new Constant());
            Splitter sBit2 = new Splitter(new Constant());
            ComponentEndPoint halfAdderBit1 = new ComponentEndPoint(new XOr(sBit1, sBit2));
            ComponentEndPoint halfAdderBit2 = new ComponentEndPoint(new And(sBit1, sBit2));
            Circuit halfAdder = new Circuit(halfAdderBit1, halfAdderBit2);

            PrintSegment("00", halfAdder, false, false);

            PrintSegment("10", halfAdder, true, false);

            PrintSegment("01", halfAdder, false, true);

            PrintSegment("11", halfAdder, true, true);
        }
    }
}
