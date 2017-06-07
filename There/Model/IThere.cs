using FunctionalThere;
using System;
using System.Threading;

namespace There
{
    public interface IThere<S>
    {
        bool IsPresent();
        void IfPresent(Action<S> consumer);
        IThere<S> Filter(Predicate<S> predicate);
        IThere<S> Condition(Predicate<S> predicate);
        S GetValue();

        IThere<S> InCaseOfTrue(Action<S> consumer);
        IThere<S> InCaseOfTrue(Supplier<S> supplier);
        IThere<S> InCaseOfTrue(ThreadStart runnable);
        IThere<S> InCaseOfFalse(Action<S> consumer);
        IThere<S> InCaseOfFalse(Supplier<S> supplier);
        IThere<S> InCaseOfFalse(ThreadStart runnable);
        
        void InCaseOfTrueGetNewValue(ThreadStart runnable);
        void InCaseOfTrueGetNewValue(Action<S> consumer);
        V InCaseOfTrueGetNewValue<V>(Supplier<V> supplier);
        V InCaseOfTrueGetNewValue<V>(Func<S, V> function);
        void InCaseOfFalseGetNewValue(ThreadStart runnable);
        void InCaseOfFalseGetNewValue(Action<S> consumer);
        V InCaseOfFalseGetNewValue<V>(Supplier<V> supplier);
        V InCaseOfFalseGetNewValue<V>(Func<S, V> function);

        void InCaseOfTrueThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception;
        S InCaseOfTrueGetValueOtherwiseThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception;
        void InCaseOfFalseThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception;
        S InCaseOfFalseGetValueOtherwiseThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception;
        void ThenThrow<E>(Supplier<E> ExceptionSupplier) where E : Exception;
    }
}
