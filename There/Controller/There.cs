using System;
using System.Threading;
using There;

namespace FunctionalThere
{
    /// <summary>
    /// Simple enough if/else for C# functional meta represented by There.class
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public sealed partial class There<S> : IThere<S> where S : class
    {
        private readonly S value;
        private Predicate<S> BasePredicate;

        private There() {}

        private There(S _value)
        {
            value = _value.RequireNonNull("value is null");
        }

        private There(Supplier<S> supplier_value)
        {
            value = supplier_value.RequireNonNull("value is null").Invoke();
        }

        private static There<S> Empty() => new There<S>();

        public static There<S> Is(S input)
        {
            return new There<S>(input);
        }

        public static There<S> Is(Supplier<S> supplier_input)
        {
            return new There<S>(supplier_input);
        }

        public static There<S> IsNullable(S input)
        {
            return input != null ? Is(input) : Empty();
        }

        public static There<S> IsNullable(Supplier<S> supplier_input)
        {
            return supplier_input != null ? Is(supplier_input) : Empty();
        }
    }

    /// <summary>
    /// This part of There class describes basis logic
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public sealed partial class There<S>
    {
        public bool IsPresent() => value != null;

        public void IfPresent(Action<S> consumer) 
        {
            consumer.RequireNonNull();
            if (IsPresent())
                 consumer.Invoke(value);            
        }

        public IThere<S> Filter(Predicate<S> predicate)
        {
            return !IsPresent()
                ? this
                : predicate.RequireNonNull().Invoke(value)
                    ? this
                    : Empty();
        }

        public IThere<S> Condition(Predicate<S> predicate)
        {
            BasePredicate = predicate;
            BasePredicate.RequireNonNullAsWellAs(predicate, $"{nameof(predicate)} is null as well");            
            return this;
        }
        
        public S GetValue()
        {
            if (!IsPresent())
            {
                throw new Exception("There is no value");
            }
            return value;
        }
    }

    /// <summary>
    /// This part of There class describes main condition manipulations, 
    /// i.e. simple True or False ways
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public sealed partial class There<S>
    {
        public IThere<S> InCaseOfTrue(Action<S> consumer)
        {
            Filter(BasePredicate).IfPresent(consumer);
            return this;
        }
        
        public IThere<S> InCaseOfTrue(Supplier<S> supplier)
        {
            Filter(BasePredicate).IfPresent(c => supplier.Invoke());
            return this;
        }
        
        public IThere<S> InCaseOfTrue(ThreadStart runnable)
        {
            Filter(BasePredicate).IfPresent(c => new Thread(runnable).Start());            
            return this;
        }

        public IThere<S> InCaseOfFalse(Action<S> consumer)
        {
            Filter(BasePredicate.Negate()).IfPresent(consumer);
            return this;
        }
        
        public IThere<S> InCaseOfFalse(Supplier<S> supplier)
        {
            Filter(BasePredicate.Negate()).IfPresent(c => supplier.Invoke());
            return this;
        }
        
        public IThere<S> InCaseOfFalse(ThreadStart runnable)
        {
            Filter(BasePredicate.Negate()).IfPresent(c => new Thread(runnable).Start());
            return this;
        }
    }
    
    /// <summary>
    /// This part of There class describes getting the value in case of true/false
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public sealed partial class There<S>
    {
        public bool IsBasePredicateTrue => BasePredicate.RequireNonNull().Invoke(value);

        public void InCaseOfTrueGetNewValue(ThreadStart runnable)
        {
            runnable.RequireNonNull();
            if (IsBasePredicateTrue)
            {
                new Thread(runnable).Start();
            }
        }

        public void InCaseOfTrueGetNewValue(Action<S> consumer)
        {
            consumer.RequireNonNull();
            if (IsBasePredicateTrue)
            {
                consumer.Invoke(value);
            }
        }

        public V InCaseOfTrueGetNewValue<V>(Supplier<V> supplier)
        {
            supplier.RequireNonNull();
            if (IsBasePredicateTrue)
            {
                return supplier.Invoke();
            }
            else return default(V);
        }
        
        public V InCaseOfTrueGetNewValue<V>(Func<S, V> function)
        {
            function.RequireNonNull();
            if (IsBasePredicateTrue)
            {
                return function.Invoke(value);
            }
            else return default(V);
        }

        public void InCaseOfFalseGetNewValue(ThreadStart runnable)
        {
            runnable.RequireNonNull();
            if (!IsBasePredicateTrue)
            {
                new Thread(runnable).Start();
            }
        }

        public void InCaseOfFalseGetNewValue(Action<S> consumer)
        {
            consumer.RequireNonNull();
            if (!IsBasePredicateTrue)
            {
                consumer.Invoke(value);
            }
        }

        public V InCaseOfFalseGetNewValue<V>(Supplier<V> supplier)
        {
            supplier.RequireNonNull();
            if (!IsBasePredicateTrue)
            {
                return supplier.Invoke();
            }
            else return default(V);
        }

        public V InCaseOfFalseGetNewValue<V>(Func<S, V> function)
        {
            function.RequireNonNull();
            if (!IsBasePredicateTrue)
            {
                return function.Invoke(value);
            }
            else return default(V);
        }
    }

    /// <summary>
    /// This part of There class describes getting the value in case of true/false otherwise throw exception
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public sealed partial class There<S>
    {
        public void InCaseOfTrueThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception
        {
            ExceptionSupplier.RequireNonNull();
            if (Filter(BasePredicate).IsPresent())
            {
                throw ExceptionSupplier.Invoke();
            }
        }

        public S InCaseOfTrueGetValueOtherwiseThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception
        {
            ExceptionSupplier.RequireNonNull();
            if (Filter(BasePredicate.Negate()).IsPresent())
            {
                return value;
            }
            else throw ExceptionSupplier.Invoke();
        }

        public void InCaseOfFalseThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception
        {
            ExceptionSupplier.RequireNonNull();
            if(Filter(BasePredicate.Negate()).IsPresent())
            {
                throw ExceptionSupplier.Invoke();
            }
        }

        public S InCaseOfFalseGetValueOtherwiseThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception
        {
            ExceptionSupplier.RequireNonNull();
            if (Filter(BasePredicate).IsPresent())
            {
                return value;
            }
            else throw ExceptionSupplier.Invoke();
        }

        public void ThenThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception
        {
            ExceptionSupplier.RequireNonNull();
            throw ExceptionSupplier.Invoke();
        }
    }
}
