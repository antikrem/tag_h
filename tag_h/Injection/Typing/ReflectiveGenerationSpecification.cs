using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using EphemeralEx.Extensions;
using TypeSharpGen.Builder;
using TypeSharpGen.Specification;


namespace tag_h.Injection.Typing
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
            => Declare(type)
                .ChainCall(
                    GetPublicProperties(type), 
                    (definition, method) 
                        => method.IsAttributed<IgnoredByClient>() 
                            ? definition 
                            : definition.AddProperty(method, new OverideName(char.ToLower(method.Name.First()) + method.Name.Substring(1) )) // TODO: EX
                )
                .EmitTo($"{type.Name}.ts");

        private static IEnumerable<PropertyInfo> GetPublicProperties(Type type)
            => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
    }
}
