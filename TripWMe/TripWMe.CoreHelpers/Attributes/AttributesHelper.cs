using System;

namespace TripWMe.CoreHelpers.Attributes
{
    public static class AttributesHelper
    {
        public static bool CheckAttributeOnClassLevel(Type type, AuditOptions options)
        {
            return true;
        }
    }

    public enum AuditOptions
    {
        IgnoreShadow
    }
}
