namespace TomLonghurst.DependencyInjection.Windsor.UnitTests
{
    public class AllowProcessor : IProcessor
    {
        public static bool WasProcessed { get; private set; }

        public AllowProcessor()
        {
            WasProcessed = false;
        }
        
        public void Process(Decision decision)
        {
            WasProcessed = true;
        }
    }
}