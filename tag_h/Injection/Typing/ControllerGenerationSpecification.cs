using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using EphemeralEx.Extensions;
using Microsoft.AspNetCore.Mvc;
using TypeSharpGen.Builder;
using TypeSharpGen.Specification;

using tag_h.Controllers;


namespace TestApplication.Specifications
{
    class ControllerGenerationSpecification : GenerationSpecification
    {
        public override string OutputRoot => "ClientApp/src/Controllers";

        public override IEnumerable<ITypeDefinition> TypeDeclaractions()
            => GetType()
                .Assembly
                .GetTypes()
                .Where(type => type.Inherits<ControllerBase>())
                .Where(type => type != typeof(ApiControllerBinderController))
                .Select(CreateControllerDefinition);

        private ITypeDefinition CreateControllerDefinition(Type type)
            => DeclareInterface(type)
                .ChainCall(GetControllerEndPoints(type), (definition, method) => definition.AddMethod(method))
                .EmitTo($"{type.Name}.d.ts");

        private static IEnumerable<string> GetControllerEndPoints(Type type)
            => type
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(method => method.Name)
                .Where(name => name != "GetFile");
    }
}
