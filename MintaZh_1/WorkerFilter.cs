using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZh_1
{
    public static class WorkerFilter
    {
        /// <summary>
        /// Filters workers by <see cref="MinimumChildAttribute"/>
        /// </summary>
        /// <param name="workers">Workers collection.</param>
        /// <returns>The filtered collection.</returns>
        public static IEnumerable<Worker> Filter(IEnumerable<Worker> workers)
        {
            return workers.Where(worker =>Validate(worker));
        }

        /// <summary>
        /// Validates a <see cref="Worker"/> object using the <see cref="MinimumChildAttribute"/>.
        /// </summary>
        /// <param name="worker">The to be validated <see cref="Worker"/></param>
        /// <returns>True if valid, otherwise false.</returns>
        private static bool Validate(Worker worker)
        {
            var property = worker.GetType().GetProperty("Children");
            var value = (int)property.GetValue(worker, null);
            var attribute = (MinimumChildAttribute)property.GetCustomAttributes(typeof(MinimumChildAttribute), false).FirstOrDefault();
            var attributeValue = attribute.Value;
            
            return value >= attributeValue;
        }
    }
}
