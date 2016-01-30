using System;

namespace AppBlocks.Extensions
{
    public static class TypeExtensions
    {
        public static T ChangeType<T>(this object @this)
        {
            if (@this == null)
            {
                return default(T);
            }

            if (@this is T)
            {
                return (T) @this;
            }

            try
            {
                return (T) Convert.ChangeType(@this, typeof (T), System.Globalization.CultureInfo.InstalledUICulture.NumberFormat);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
