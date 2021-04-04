using System;
using System.Collections.Generic;
using System.Text;
using CircuitLib.Components;
using CircuitLib.Components.Gates;
using static CircuitLibDemo.Utils;

namespace CircuitLibDemo
{
    public class FullAdder4Bit
    {
        public void RunExample()
        {
            Splitter bit1A = new Splitter(new Constant());
            Splitter bit1B = new Splitter(new Constant());
            ComponentEndPoint sum1 = new ComponentEndPoint(new XOr(bit1A, bit1B));
            IComponent carry1 = new And(bit1A, bit1B);


            Splitter bit2A = new Splitter(new Constant());
            Splitter bit2B = new Splitter(new Constant());
            Splitter carry1Splitter = new Splitter(carry1);
            Splitter result2Splitter = new Splitter(new XOr(bit2A, bit2B));
            ComponentEndPoint sum2 = new ComponentEndPoint(new XOr(carry1Splitter, result2Splitter));
            IComponent carry2 = new Or(new And(carry1Splitter, result2Splitter), new And(bit2A, bit2B));


            Splitter bit3A = new Splitter(new Constant());
            Splitter bit3B = new Splitter(new Constant());
            Splitter carry2Splitter = new Splitter(carry2);
            Splitter result3Splitter = new Splitter(new XOr(bit3A, bit3B));
            ComponentEndPoint sum3 = new ComponentEndPoint(new XOr(carry2Splitter, result3Splitter));
            IComponent carry3 = new Or(new And(carry2Splitter, result3Splitter), new And(bit3A, bit3B));


            Splitter bit4A = new Splitter(new Constant());
            Splitter bit4B = new Splitter(new Constant());
            Splitter carry3Splitter = new Splitter(carry3);
            Splitter result4Splitter = new Splitter(new XOr(bit4A, bit4B));
            ComponentEndPoint sum4 = new ComponentEndPoint(new XOr(carry3Splitter, result4Splitter));
            IComponent carry4 = new Or(new And(carry3Splitter, result4Splitter), new And(bit4A, bit4B));


            ComponentEndPoint carry4Remainder = new ComponentEndPoint(carry4);
            Circuit fullAdder4Bit = new Circuit(sum1, sum2, sum3, sum4, carry4Remainder);

            PrintSegment("10000", fullAdder4Bit, true, false, false, false, false);

            PrintSegment("01000", fullAdder4Bit, false, true, false, false, false);

            PrintSegment("00001", fullAdder4Bit, false, false, false, false, true);

            PrintSegment("00110", fullAdder4Bit, false, false, true, true, false);

            PrintSegment("11111", fullAdder4Bit, true, true, true, true, true);
        }
    }
}
