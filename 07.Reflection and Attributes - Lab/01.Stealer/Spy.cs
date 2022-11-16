namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string StealFieldInfo(string className, params string[] fields)
        {
            StringBuilder sb = new StringBuilder();

            Type typeToInvestigate = Type.GetType(className);

            object obj = Activator.CreateInstance(typeToInvestigate);

            sb.AppendLine($"Class under investigation: {typeToInvestigate.FullName}");

            FieldInfo[] fieldsInfo = typeToInvestigate.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo field in fieldsInfo.Where(f => fields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(obj)}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
