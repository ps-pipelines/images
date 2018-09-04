using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework.Api;
using NUnit.Framework.Internal;

namespace PetStore.Images.Tests.Runner
{

    /// <summary>
    /// this allows the Unit tests to be run as an exe.
    ///
    /// the idea is to run this with Rapid 7
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new NUnitTestAssemblyRunner(new DefaultTestAssemblyBuilder());
            runner.Load(typeof(SimpleApiTest).Assembly, new Dictionary<string, object>()
                {
                    { "context" , "remote" }
                }
            );
            runner.Run(TestListener.NULL, TestFilter.Empty);

            Console.WriteLine("done");
            Thread.Sleep(1000);

        }
    }
}
