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
            Console.WriteLine("Running HalfAdder example: ");
            HalfAdder halfAdder = new HalfAdder();
            halfAdder.RunExample();

            Console.WriteLine("Running FullAdder4Bit example: ");
            FullAdder4Bit fullAdder4Bit = new FullAdder4Bit();
            fullAdder4Bit.RunExample();
        }
    }
}
