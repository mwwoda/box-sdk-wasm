using System.Reflection;

namespace Box.Sdk.Wasm.Utils
{
    public static class ReflectionHelper
    {
        static Assembly assembly;
        static List<Type> types;
        static List<string> excludedMethodNames = new List<string>() { "GetType", "ToString", "Equals", "GetHashCode" };

        static ReflectionHelper()
        {
            assembly = Assembly.Load("Box.V2.Core");
            types = assembly.GetExportedTypes().Where(x => x.Name.StartsWith("Box") && x.Name.EndsWith("Manager")).ToList();
        }

        public static List<Type> GetTypes()
        {
            return types;
            //assembly = Assembly.Load("Box.V2.Core");
            //return assembly.GetExportedTypes().Where(x => x.Name.StartsWith("Box") && x.Name.EndsWith("Manager")).ToList();
        }

        public static List<MethodInfo> GetMethods(string typeName)
        {
            var types = GetTypes();
            var type = types.Single(x => x.FullName == typeName);
            var methods = type.GetMethods().Where(x => !excludedMethodNames.Contains(x.Name));
            return methods.ToList();
        }

        public static List<ParameterInfo> GetParameters(string typeName, string methodName)
        {
            var methods = GetMethods(typeName);
            var method = methods.Single(x => x.Name == methodName);
            var parameters = method.GetParameters();
            return parameters.ToList();
        }
    }
}
