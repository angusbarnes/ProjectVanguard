using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Extensions
{
    public static string FlattenToString<T>(this IEnumerable<T> array)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendJoin(',', array);
        /*
        foreach (T item in array) {
            stringBuilder.Append(item.ToString());
            stringBuilder.Append(", ");
        }
        */

        return stringBuilder.ToString();
    }

    public static bool Exists(this object obj)
    {
        return !(obj == null);
    }
}
