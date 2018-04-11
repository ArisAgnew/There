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

            //test.GetNewValueOtherwiseThrowTest();
            //test.StringExecution();

            WriteLine(test.GetNewValueTest("Hi!"));
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
        internal object GetNewValueOtherwiseThrowTest() => There<object>.IsNullable(@INT)
                .Condition(v => v.Equals(23))
                .InCaseOfTrue(t => WriteLine($"Содержится: {t}"))
                .InCaseOfFalse(f => WriteLine($"Не Содержится: {f}"))
                .InCaseOfFalseGetValueOtherwiseThrow(() => new InvalidProgramException(nameof(InvalidProgramException)));

        internal void StringExecution() => There<string>.IsNullable(RESPONSE_SUCCESS_CREATED)
                .Condition(v => v.StartsWith("<a") && v.Contains("Su"))
                .InCaseOfTrue(t => WriteLine($"Содержится: {t}"))
                .InCaseOfFalse(f => WriteLine($"Не Содержится: {f}"))
                .InCaseOfFalseThrow(() => new InvalidCastException("YOU"));

        internal object GetNewValueTest(string str) => 
            There<object>.IsNullable(str).Condition(s => string.IsNullOrEmpty((string)s)).InCaseOfTrueGetNewValue(() => $"Yes it's true: ");

        //doesn't work properly, it has to be improved
        internal string MemKek(string str)
        {
            There<string>.IsNullable(str)
                .Condition(s => string.IsNullOrEmpty(s))
                .InCaseOfTrueGetNewValue(output => string.Format($"Empty String: {output}"));
                       
            for (int i = 0; i < str.Length - 1; i++)
            {
                There<string>.IsNullable(str)
                    .Condition(s => str[i] == str[i + 1])
                    .InCaseOfTrueGetNewValue(output => MemKek(str.Substring(0, i) + str.Substring(i + 2)));
            }
            return str;
        }
    }
}
