using System;
using UnityEngine;


public class TypeRequireAttribute : PropertyAttribute
{
    public Type Type { get; }

    public TypeRequireAttribute(Type type)
    {
        Type = type;
    }
}