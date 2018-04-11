using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalThere
{
    public static class PredicateStuff
    {
        public static Predicate<T> And<T>(this Predicate<T> first, Predicate<T> second) => 
            (T type) => first.RequireNonNull()(type) && second.RequireNonNull()(type);

        public static Predicate<T> Or<T>(this Predicate<T> first, Predicate<T> second) => 
            (T type) => first.RequireNonNull()(type) || second.RequireNonNull()(type);

        public static Predicate<T> Negate<T>(this Predicate<T> predicate) => 
            (T type) => !predicate.RequireNonNull()(type);

        public static Predicate<T> IsEqual<T>(dynamic target) => 
            (T obj) => target.RequireNonNull().Equals(obj);
    }
}
