using System;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class SkipLoggingAttribute
        : Attribute
    {
    }
}
