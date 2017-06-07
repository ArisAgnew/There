using System;

namespace FunctionalThere
{
    public static class ThereUtils
    {
        internal static T RequireNonNull<T>(this T input)
        {
            if (input.Equals(null))
                throw new NullReferenceException();
            return input;
        }

        internal static T RequireNonNull<T>(this T input, string message)
        {
            if (input.Equals(null))
                throw new NullReferenceException(message);
            return input;
        }

        internal static T RequireNonNullAsWellAs<T>(this T input, T obj)
        {
            obj.RequireNonNull("Second operand is null");
            if (input.Equals(null))
                throw new NullReferenceException();
            return input;
        }

        internal static T RequireNonNullAsWellAs<T>(this T input, T obj, string message)
        {
            obj.RequireNonNull("Second operand is null");
            if (input.Equals(null))
                throw new NullReferenceException(message);
            return input;
        }        
    }
}
