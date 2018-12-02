using Moq;
using System.Collections.Generic;

namespace TestUtilities.MockBuilders
{
    public class TransactionsMockBuilder
    {
        public IList<string> Build()
        {
            var transactions = Mock.Of<IList<string>>();
            Mock.Get(transactions)
                .Setup(t => t.Add(It.IsAny<string>()));

            return transactions;
        }
    }
}
