using Bank_Kata.Interfaces;
using Moq;

namespace TestUtilities.MockBuilders
{
    public class ConsoleMockBuilder
    {
        public IConsoleAdapter Build()
        {
            var console = Mock.Of<IConsoleAdapter>();
            Mock.Get(console)
                .Setup(m => m.WriteLine(It.IsAny<string>()));

            return console;
        }
    }
}