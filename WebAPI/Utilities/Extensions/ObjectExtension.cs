using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utilities.Extensions
{
    public static class ObjectExtension
    {
        /**
        * Assign values base on property name.
        */
        public static void AssignNotDefaultProperties (this object obj, object otherObject) {
            var type = obj.GetType();
            var otherType = otherObject.GetType();

            foreach (var otherProperty in otherType.GetProperties()) {
                var property = type.GetProperty(otherProperty.Name);
                if (property == null) continue;

                var value = otherProperty.GetValue(otherObject);
                if (value == default) continue;

                property.SetValue(obj, value);
            }
        }
    }
}