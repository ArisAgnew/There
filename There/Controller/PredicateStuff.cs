using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalThere
{
    public static class PredicateStuff
    {
        public static Predicate<T> And<T>(this Predicate<T> first, Predicate<T> second)
        {
            return (T type) => first.RequireNonNull().Invoke(type) && second.RequireNonNull().Invoke(type);
        }

        public static Predicate<T> Or<T>(this Predicate<T> first, Predicate<T> second)
        {
            return (T type) => first.RequireNonNull().Invoke(type) || second.RequireNonNull().Invoke(type);
        }

        public static Predicate<T> Negate<T>(this Predicate<T> predicate)
        {            
            return (T type) => !predicate.RequireNonNull().Invoke(type);
        }
                
        public static Predicate<T> IsEqual<T>(object target)
        {
            return (T @object) => target.RequireNonNull().Equals(@object);
        }
    }
}
