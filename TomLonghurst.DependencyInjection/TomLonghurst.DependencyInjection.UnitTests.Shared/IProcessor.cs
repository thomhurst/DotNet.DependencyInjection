using System;

namespace TomLonghurst.DependencyInjection.UnitTests.Shared
{
    public interface IProcessor
    {
        Type Process(Decision decision);
    }
}