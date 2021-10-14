using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;


using TypeGen.Core.Extensions;
using TypeGen.Core.SpecGeneration;

using tag_h.Helper.Extensions;


namespace tag_h.Injection.Typing
{
    public class ReflectiveGenerationSpec : GenerationSpec
    {

        //TODO: Use ReflectionHelper
        public override void OnBeforeGeneration(OnBeforeGenerationArgs args)
            => GetType()
                .Assembly
                .GetLoadableTypes()
                .Where(type => type.IsAttributed<UsedByClient>())
                .ForEach(RegisterType);

        public override void OnBeforeBarrelGeneration(OnBeforeBarrelGenerationArgs args)
            => AddBarrel(TypesDirectory);

        private void RegisterType(Type type)
            => AddInterface(type, TypesDirectory)
                .ChainCall(
                        ExcludedMembers(type),
                        (builder, excludedMember) => builder.Member(excludedMember)
                    )
                .Ignore();

        private static IEnumerable<string> ExcludedMembers(Type type)
            => type
                .GetProperties()
                .Where(IsExcludedProperty)
                .Select(property => property.Name);

        private static bool IsExcludedProperty(MemberInfo type)
            => type.IsAttributed<ExcludeFromClientType>();

        private static string TypesDirectory => "types";
    }
}