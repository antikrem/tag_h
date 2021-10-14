using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

using tag_h.Core.Helper.Extensions;

namespace tag_h.Injection.Controllers
{
    static public class ApiControllerBinder
    {

        public static IEnumerable<ControllerDefinition> GetControllerDefinitions()
        {
            return GetApiControllers()
                .Select(controller => new ControllerDefinition(controller));
        }

        private static IEnumerable<Type> GetApiControllers()
        {
            return ReflectionHelper.AllTypes
                .Where(type => type.IsDefined(typeof(ApiControllerAttribute), false));
        }

        public record ControllerDefinition(string Name, ControllerMethod[] Methods)
        {
            internal ControllerDefinition(Type controller)
                : this(
                        controller.Name.MatchFirst(@"(.*)Controller$"),
                        controller.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            .Where(method => method.GetCustomAttributes(typeof(HttpMethodAttribute), true).Any())
                            .Select(method => new ControllerMethod(method))
                            .ToArray()
                    )
            { }
        }

        public record ControllerMethod(string Name, string Method, string ReturnType, ControllerMethodParameter[] Parameters)
        {
            internal ControllerMethod(MethodInfo method) 
                : this(
                        method.Name,
                        GetHttpMethod(method),
                        method.ReturnType.Name,
                        method.GetParameters()
                            .Select(parameter => new ControllerMethodParameter(parameter))
                            .ToArray()
                    )
            { }

            private static string GetHttpMethod(MethodInfo method)
            {
                return method
                    .GetCustomAttribute(typeof(HttpMethodAttribute), true)
                    .GetType()
                    .Name
                    .MatchFirst(@"Http(.*)Attribute")
                    .ToUpperInvariant();
            }
        }

        public record ControllerMethodParameter(string Name, string Type, bool IsSimple)
        {
            internal ControllerMethodParameter(ParameterInfo parameter)
                : this(
                        parameter.Name, 
                        parameter.ParameterType.Name,
                        parameter.ParameterType.IsSimple()
                    ) 
            { }
        }

    }
}
