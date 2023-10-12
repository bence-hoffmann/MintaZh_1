using MintaZh_1;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    public class WorkerTest
    {
        /// <summary>
        /// <see cref="Worker"/> lists, { given values, expected values }. 
        /// </summary>
        public static IEnumerable<TestCaseData> WorkerAndExpected
        {
            get
            {
                var cases = new List<TestCaseData>();

                var input = new List<Worker>
                {
                    new Worker { Children = 1 },
                    new Worker { Children = 1 },
                    new Worker { Children = 2 },
                    new Worker { Children = 0 },
                    new Worker(),
                    new Worker { Children = 100 },
                    new Worker { Children = -1 },
                    new Worker { Children = -100 }
                };

                var expexted = new List<Worker>
                {
                    new Worker { Children = 2 },
                    new Worker { Children = 100 },
                };

                cases.Add(new TestCaseData(new object[] { input, expexted }));

                return cases;
            }
        }

        /// <summary>
        /// Test if <see cref="WorkerFilter.Filter(IEnumerable{Worker})"/> filters correctly.
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="expected"></param>
        [TestCaseSource(nameof(WorkerAndExpected))]
        public void WorkerFilterTest(List<Worker> workers, List<Worker> expected)
        {
            //Act
            var actual = WorkerFilter.Filter(workers);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
