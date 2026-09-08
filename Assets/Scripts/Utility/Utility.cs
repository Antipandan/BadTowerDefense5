using System;
using System.Runtime.CompilerServices;

namespace Utility
{
    public static class Utility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckIfTypeIsNull<T>(T obj) where T : class
        {
            return obj is null;
        }
    }
}