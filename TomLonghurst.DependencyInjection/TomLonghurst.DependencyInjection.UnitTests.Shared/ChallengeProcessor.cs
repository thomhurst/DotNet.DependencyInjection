using System;

namespace TomLonghurst.DependencyInjection.UnitTests.Shared
{
    public class ChallengeProcessor : IProcessor
    {
        private readonly IProcessor _next;
        public static bool WasProcessed { get; private set; }

        public ChallengeProcessor(IProcessor next)
        {
            _next = next;
            WasProcessed = false;
        }
        
        public Type Process(Decision decision)
        {
            WasProcessed = true;
            if (decision != Decision.Challenge)
            {
                return _next.Process(decision);
            }
            
            return GetType();
        }
    }
}