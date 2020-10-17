namespace TomLonghurst.DependencyInjection.UnitTests.Shared
{
    public class BlockProcessor : IProcessor
    {
        private readonly IProcessor _next;
        public static bool WasProcessed { get; private set; }

        public BlockProcessor(IProcessor next)
        {
            _next = next;
            WasProcessed = false;
        }
        
        public void Process(Decision decision)
        {
            WasProcessed = true;
            if (decision != Decision.Block)
            {
                _next.Process(decision);
            }
        }
    }
}