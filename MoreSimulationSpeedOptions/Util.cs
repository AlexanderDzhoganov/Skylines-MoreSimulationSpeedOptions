using System;
using System.IO;
using System.Reflection;
using ColossalFramework;
using UnityEngine;

namespace MoreSimulationSpeedOptions
{

    public static class Util
    {

        public static FieldInfo FindField<T>(T o, string fieldName)
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var f in fields)
            {
                if (f.Name == fieldName)
                {
                    return f;
                }
            }

            return null;
        }

        public static T GetFieldValue<T>(FieldInfo field, object o)
        {
            return (T)field.GetValue(o);
        }

        public static void SetFieldValue<T>(FieldInfo field, object o, T value)
        {
            field.SetValue(o, value);
        }

    }

}
