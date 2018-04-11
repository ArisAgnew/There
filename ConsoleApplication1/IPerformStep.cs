using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    internal interface IPerformStep<T> where T : IPerformStep<T>
    {
        T perform(string description, Action<T> stepsAction);
    }

    internal class PerformStep : IPerformStep<PerformStep>
    {
        public PerformStep perform(string description, Action<PerformStep> stepsAction)
        {
            string.IsNullOrEmpty(description);
            stepsAction?.Invoke(this);
            return this;            
        }
    }

    internal static class Utilites
    {
        internal static T RequireNonNull<T>(this T input)
        {
            if (input.Equals(null))
                throw new NullReferenceException();
            return input;
        }        
    }
}
