using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using EphemeralEx.Extensions;
using Microsoft.AspNetCore.Mvc;
using TypeSharpGen.Builder;
using TypeSharpGen.Specification;

using tag_h.Controllers;
using System.Threading.Tasks;

namespace tag_h.Injection.Typing
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
            => Declare(type)
                .ChainCall(
                    GetControllerEndPoints(type), 
                    (definition, method) => definition.AddMethod(method.Name, GetOverideForMethodReturn(method))
                )
                .EmitTo($"{type.Name}.ts");

        private static IEnumerable<MethodInfo> GetControllerEndPoints(Type type)
            => type
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(method => !method.IsAttributed<IgnoredByClient>());

        private static IMethodModifier GetOverideForMethodReturn(MethodInfo method)
            => new OverideReturnType(
                method.ReturnType.Collect(
                    type => type == typeof(void) ? typeof(Task) : null,
                    type => IsTaskType(type) ? type : null,
                    type => typeof(Task<>).MakeGenericType(type)
                )
                .NotNull()
                .First());

        private static bool IsTaskType(Type type) //TODO: move to ex 
            => (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
                || type == typeof(Task);

    }
}
