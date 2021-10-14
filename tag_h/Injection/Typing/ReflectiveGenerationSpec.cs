using System;
using System.Linq;

using tag_h.Core.Helper.Extensions;

using TypeGen.Core.Extensions;
using TypeGen.Core.SpecGeneration;


namespace tag_h.Injection.Typing
{
    public class ReflectiveGenerationSpec : GenerationSpec
    {
        public override void OnBeforeGeneration(OnBeforeGenerationArgs args)
        {
            //TODO: use ReflectionHelper
            GetType()
                .Assembly
                .GetLoadableTypes()
                .Where(type => type.IsDefined(typeof(UsedByClient), false))
                .ForEach(Register);
        }

        private void Register(Type type)
        {
            AddInterface(type);
        }
    }
}