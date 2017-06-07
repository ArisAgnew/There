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
            first.RequireNonNullAsWellAs(second);
            return (T type) => first.Invoke(type) && second.Invoke(type);
        }

        public static Predicate<T> Or<T>(this Predicate<T> first, Predicate<T> second)
        {
            first.RequireNonNullAsWellAs(second);
            return (T type) => first.Invoke(type) || second.Invoke(type);
        }

        public static Predicate<T> Negate<T>(this Predicate<T> predicate)
        {
            predicate.RequireNonNull();
            return (T type) => !predicate.Invoke(type);
        }
                
        public static Predicate<T> IsEqual<T>(object target)
        {
            if (target != null)
                return (T @object) => target.Equals(@object);
            else return null;
        }
    }
}
