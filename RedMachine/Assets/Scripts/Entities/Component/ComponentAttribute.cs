using System;

[AttributeUsage(AttributeTargets.Class)]
public class ComponentAttribute : System.Attribute
{
    public bool IsIdentity = true;
}