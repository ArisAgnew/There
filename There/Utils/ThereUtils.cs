using System;

namespace FunctionalThere
{
    public static class ThereUtils
    {
        internal static T RequireNonNull<T>(this T input, string message = default)
        {
            if (input.Equals(null))
                throw new NullReferenceException(message);
            return input;
        }

        internal static T RequireNonNullAsWellAs<T>(this T input, T obj, string message = default)
        {
            input.RequireNonNull("First operand is null");
            obj.RequireNonNull(message);            
            return input;
        }
    }
}
