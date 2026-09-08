using System;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace Utility
{
    /// <summary>
    /// Class that contains functions that handle very boardly applicable code and code that is boring / is a slog to rewrite
    /// If a code snippet fulfills said criterias, a function probably exists in here
    /// </summary>
    public static class Utility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckIfTypeIsNull<T>(T obj) where T : class
        {
            return obj is null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogStandardSingletonCreationError(Object obj)
        {
            Debug.Log($"Object {obj.name} was destroyed because there can only exist one of these objects in a given scene.", obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogStandardSingletonCreationError(object obj)
        {
            Debug.Log($"object {obj} was destroyed because there can only exist one of these objects in a given scene.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogWarningStandardNullReference(Object obj)
        {
            Debug.LogWarning($"Warning! object {obj.name} is null. Please fill in this reference", obj);
        }

        public static void LogCompleteStandardNullReference(Object obj)
        {
            if (obj is null) LogWarningStandardNullReference(obj);
        }
        public static void LogWarningStandardNullReferenceWeak(Object obj)
        {
            Debug.LogWarning($"Warning! object {obj.name} is null. It is recommended that this reference is filled", obj);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogWarningStandardNullReference(object obj)
        {
            Debug.LogWarning($"Warning! object {obj} is null. Please fill in this reference");
        }
        
        
        // No CallerArgumentExpression :(
        public static void LogStandardEmptyCollection()
        {
            Debug.Log($"Collection is empty");
        }
    }
}