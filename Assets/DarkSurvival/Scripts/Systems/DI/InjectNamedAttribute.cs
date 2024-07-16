using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class InjectNamedAttribute : Attribute
{
    public string Name { get; }

    public InjectNamedAttribute(string name)
    {
        Name = name;
    }
}
