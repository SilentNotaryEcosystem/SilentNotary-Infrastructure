using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace SilentNotary.Common
{
    public class TypeFactory : ITypeFactory
    {
        private static Assembly[] Assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(asm => asm.FullName.StartsWith("SilentNotary") || asm.FullName.StartsWith("SBN"))
            .ToArray();

        private readonly ConcurrentDictionary<string, Type> _types = new ConcurrentDictionary<string, Type>();

        public Type Get(string typeName)
        {
            string genericArg = null;
            if (typeName.IndexOf("[", StringComparison.Ordinal) > 0)
            {
                var startIdx = typeName.IndexOf("[", StringComparison.Ordinal) + 1;
                var endIdx = typeName.IndexOf("]", StringComparison.Ordinal);
                genericArg = typeName.Substring(startIdx, endIdx - startIdx);
                typeName = typeName.Replace(genericArg, "T");
            }

            if (_types.TryGetValue(typeName, out Type cmdType))
                return cmdType;

            foreach (var assembly in Assemblies)
            {
                cmdType = assembly
                    .GetTypes()
                    .FirstOrDefault(type => type
                        .ToString()
                        .Equals(typeName)
                    );

                if (cmdType == null) continue;

                if (genericArg != null)
                {
                    var genericArgType = Get(genericArg);
                    cmdType = cmdType.MakeGenericType(genericArgType);
                }

                _types[typeName] = cmdType;
                return cmdType;
            }

            return null;
        }
    }
}