using System;
using Castle.Core;
using Castle.Windsor;
using NUnit.Framework;

namespace TomLonghurst.DependencyInjection.Windsor.UnitTests
{
    public class Tests
    {
        private WindsorContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = new WindsorContainer();
        }

        [Repeat(3)]
        [Test]
        public void When_DecisionBlock_Then_DoNotDelegateToNextProcessors()
        {
            _container.AddChained<IProcessor, BlockProcessor, ChallengeProcessor, AllowProcessor>(LifestyleType.Transient);
            var processor = _container.Resolve<IProcessor>();
            processor.Process(Decision.Block);
            
            Assert.That(BlockProcessor.WasProcessed, Is.True);
            Assert.That(ChallengeProcessor.WasProcessed, Is.False);
            Assert.That(AllowProcessor.WasProcessed, Is.False);
        }
        
        [Repeat(3)]
        [Test]
        public void When_DecisionChallenge_Then_DoNotDelegateToAllowProcessor()
        {
            _container.AddChained<IProcessor, BlockProcessor, ChallengeProcessor, AllowProcessor>(LifestyleType.Transient);
            var processor = _container.Resolve<IProcessor>();
            processor.Process(Decision.Challenge);
            
            Assert.That(BlockProcessor.WasProcessed, Is.True);
            Assert.That(ChallengeProcessor.WasProcessed, Is.True);
            Assert.That(AllowProcessor.WasProcessed, Is.False);
        }
        
        [Repeat(3)]
        [Test]
        public void When_DecisionAllow_Then_AllProcessorsRun()
        {
            _container.AddChained<IProcessor, BlockProcessor, ChallengeProcessor, AllowProcessor>(LifestyleType.Transient);
            var processor = _container.Resolve<IProcessor>();
            processor.Process(Decision.Allow);
            
            Assert.That(BlockProcessor.WasProcessed, Is.True);
            Assert.That(ChallengeProcessor.WasProcessed, Is.True);
            Assert.That(AllowProcessor.WasProcessed, Is.True);
        }
        
        [Test]
        public void When_InterfaceIsNotFirstArgument_Then_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => _container.AddChained<BlockProcessor, ChallengeProcessor, AllowProcessor>(LifestyleType.Transient));
        }
        
        [Test]
        public void When_ImplementationsThatImplementWrongInterface_Then_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => _container.AddChained<IWrongInterface, BlockProcessor, ChallengeProcessor, AllowProcessor>(LifestyleType.Transient));
        }
        
        [Test]
        public void When_NoImplementationsProvided_Then_ThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => _container.AddChained<IProcessor>(LifestyleType.Transient));
        }
    }
}