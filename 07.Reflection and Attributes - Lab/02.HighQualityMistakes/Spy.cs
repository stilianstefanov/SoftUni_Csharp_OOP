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

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type typeToAnalyze = Type.GetType(className);

            FieldInfo[] fields = typeToAnalyze.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            MethodInfo[] publicMethods = typeToAnalyze.GetMethods(BindingFlags.Public| BindingFlags.Instance);

            MethodInfo[] nonPublicMethods = typeToAnalyze.GetMethods(BindingFlags.NonPublic| BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (MethodInfo method in publicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            foreach (MethodInfo methodInfo in nonPublicMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{methodInfo.Name} have to be public!");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
