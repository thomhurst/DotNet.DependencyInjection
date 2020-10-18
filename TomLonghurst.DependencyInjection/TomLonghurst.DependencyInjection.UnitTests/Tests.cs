using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TomLonghurst.DependencyInjection.UnitTests.Shared;

namespace TomLonghurst.DependencyInjection.UnitTests
{
    public class Tests
    {
         private IServiceCollection _serviceCollection;

        [SetUp]
        public void Setup()
        {
            _serviceCollection = new ServiceCollection();
        }

        [Repeat(3)]
        [Test]
        public void When_DecisionBlock_Then_DoNotDelegateToNextProcessors()
        {
            _serviceCollection.Chain<IProcessor>()
                .Add<BlockProcessor>()
                .Add<ChallengeProcessor>()
                .Add<AllowProcessor>()
                .Configure();
            
            var processor = _serviceCollection.BuildServiceProvider().GetService<IProcessor>();
            var processedByType = processor.Process(Decision.Block);
            
            Assert.That(BlockProcessor.WasProcessed, Is.True);
            Assert.That(ChallengeProcessor.WasProcessed, Is.False);
            Assert.That(AllowProcessor.WasProcessed, Is.False);
            
            Assert.That(processedByType, Is.EqualTo(typeof(BlockProcessor)));
        }
        
        [Repeat(3)]
        [Test]
        public void When_DecisionChallenge_Then_DoNotDelegateToAllowProcessor()
        {
            _serviceCollection.Chain<IProcessor>()
                .Add<BlockProcessor>()
                .Add<ChallengeProcessor>()
                .Add<AllowProcessor>()
                .Configure();
            
            var processor = _serviceCollection.BuildServiceProvider().GetService<IProcessor>();
            var processedByType = processor.Process(Decision.Challenge);
            
            Assert.That(BlockProcessor.WasProcessed, Is.True);
            Assert.That(ChallengeProcessor.WasProcessed, Is.True);
            Assert.That(AllowProcessor.WasProcessed, Is.False);
            
            Assert.That(processedByType, Is.EqualTo(typeof(ChallengeProcessor)));
        }
        
        [Repeat(3)]
        [Test]
        public void When_DecisionAllow_Then_AllProcessorsRun()
        {
            _serviceCollection.Chain<IProcessor>()
                .Add<BlockProcessor>()
                .Add<ChallengeProcessor>()
                .Add<AllowProcessor>()
                .Configure();
            
            var processor = _serviceCollection.BuildServiceProvider().GetService<IProcessor>();
            var processedByType = processor.Process(Decision.Allow);
            
            Assert.That(BlockProcessor.WasProcessed, Is.True);
            Assert.That(ChallengeProcessor.WasProcessed, Is.True);
            Assert.That(AllowProcessor.WasProcessed, Is.True);
            
            Assert.That(processedByType, Is.EqualTo(typeof(AllowProcessor)));
        }

        [Test]
        public void When_NoImplementationsProvided_Then_ThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _serviceCollection.Chain<IProcessor>()
                    .Configure();
            });
        }
    }
}