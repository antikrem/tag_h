using System;


namespace tag_h.Injection.Typing
{
    public class ClientReturns : Attribute
    {
        public Type Type { get; }

        public ClientReturns(Type type)
        {
            Type = type;
        }
    }
}