using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using EphemeralEx.Extensions;
using TypeSharpGen.Builder;
using TypeSharpGen.Specification;

using tag_h.Injection.Typing;

namespace TestApplication.Specifications
{
    class ReflectiveGenerationSpecification : GenerationSpecification
    {
        public override string OutputRoot => "ClientApp/src/Typings";

        public override IEnumerable<ITypeDefinition> TypeDeclaractions()
            => GetType()
                .Assembly
                .GetTypes()
                .Where(type => type.IsAttributed<UsedByClient>())
                .Select(CreateTypeDefinition);

        private ITypeDefinition CreateTypeDefinition(Type type)
            => DeclareInterface(type)
                .ChainCall(GetPublicProperties(type), (definition, method) => definition.AddProperty(method))
                .EmitTo($"{type.Name}.d.ts");

        private static IEnumerable<PropertyInfo> GetPublicProperties(Type type)
            => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
    }
}
