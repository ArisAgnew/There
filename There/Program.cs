using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace FunctionalThere
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();

            test.GetNewValueTest();
            test.StringExecution();
            
            WriteLine();
            ReadKey();
        }
    }

    internal class Test
    {
        private const int @INT = 23;
        private const string RESPONSE_SUCCESS_CREATED = "<a:Code>Success</a:Code>";

        /// <summary>
        /// It happened if you need to use any digit type you have to make it through Object class/object keyword
        /// </summary>
        /// <returns></returns>
        internal object GetNewValueTest() => There<object>.IsNullable(@INT)
                .Condition(v => v.Equals(23))
                .InCaseOfTrue(t => WriteLine($"Содержится: {t}"))
                .InCaseOfFalse(f => WriteLine($"Не Содержится: {f}"))
                .InCaseOfTrueGetNewValue(() => 89);

        internal void StringExecution()
        {
            There<string>.IsNullable(RESPONSE_SUCCESS_CREATED)
                .Condition(v => v.StartsWith("<a") && v.Contains("Su"))
                .InCaseOfTrue(t => WriteLine($"Содержится: {t}"))
                .InCaseOfFalse(f => WriteLine($"Не Содержится: {f}"))
                .InCaseOfFalseThrow(() => new InvalidCastException("YOU"));
        }
    }
}
