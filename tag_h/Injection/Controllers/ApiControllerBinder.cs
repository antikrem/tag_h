using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using EphemeralEx.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;


namespace tag_h.Injection.Controllers
{
    static public class ApiControllerBinder
    {

        public static IEnumerable<ControllerDefinition> GetControllerDefinitions() 
            => GetApiControllers()
                .Select(controller => new ControllerDefinition(controller));

        private static IEnumerable<Type> GetApiControllers() 
            => ReflectionHelper.AllTypes
                .Where(type => type.IsAttributed<ApiControllerAttribute>());

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
                            .Select(parameter => new ControllerMethodParameter(method, parameter))
                            .ToArray()
                    )
            { }

            private static string GetHttpMethod(MethodInfo method) 
                => method
                    .GetCustomAttribute(typeof(HttpMethodAttribute), true)
                    ?.GetType()
                    .Name
                    .MatchFirst(@"Http(.*)Attribute")
                    .ToUpperInvariant()
                    ?? throw new MethodIsNotAHttpMethodException(method);
        }

        public record ControllerMethodParameter(string Name, string Type, bool IsSimple)
        {
            internal ControllerMethodParameter(MethodInfo method, ParameterInfo parameter)
                : this(
                        parameter.Name ?? throw new MethodParameterHasNoNameException(method, parameter), 
                        parameter.ParameterType.Name,
                        parameter.ParameterType.IsSimple()
                    ) 
            { }
        }


        internal abstract class ApiControllerBindingException : Exception
        { }

        internal class MethodIsNotAHttpMethodException : Exception
        {
            public MethodInfo Method { get; }

            internal MethodIsNotAHttpMethodException(MethodInfo method)
                : base("Method is not a HTTP")
            {
                Method = method;
            }
        }

        internal class MethodParameterHasNoNameException : Exception
        {
            public MethodInfo Method { get; }
            public ParameterInfo Parameter { get; }

            internal MethodParameterHasNoNameException(MethodInfo method, ParameterInfo parameter)
                : base("Method is not a HTTP")
            {
                Method = method;
                Parameter = parameter;
            }
        }
    }
}
