namespace ValidationAttributes
{  
    using System.Linq;
    using System.Reflection;
    using ValidationAttributes.Attributes;

    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            PropertyInfo[] properties = obj
                .GetType()
                .GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(MyValidationAttribute)).Any())
                .ToArray();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);

                MyValidationAttribute attribute = property.GetCustomAttribute<MyValidationAttribute>();

                if(!attribute.IsValid(value))
                    return false;
            }

            return true;
        }
    }
}
