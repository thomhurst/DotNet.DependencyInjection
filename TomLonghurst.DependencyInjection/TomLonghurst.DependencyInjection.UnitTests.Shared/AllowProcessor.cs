using System;

namespace TomLonghurst.DependencyInjection.UnitTests.Shared
{
    public class AllowProcessor : IProcessor
    {
        public static bool WasProcessed { get; private set; }

        public AllowProcessor()
        {
            WasProcessed = false;
        }
        
        public Type Process(Decision decision)
        {
            WasProcessed = true;
            return GetType();
        }
    }
}