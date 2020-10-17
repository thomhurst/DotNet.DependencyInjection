namespace TomLonghurst.DependencyInjection.Windsor.UnitTests
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
        
        public void Process(Decision decision)
        {
            WasProcessed = true;
            if (decision != Decision.Challenge)
            {
                _next.Process(decision);
            }
        }
    }
}