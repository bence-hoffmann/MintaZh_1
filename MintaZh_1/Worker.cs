using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MintaZh_1
{
    [Keyless]
    public class Worker
    {
        /// <summary>
        /// Name of the worker.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current position of the worker.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Curent salary of the worker.
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Active status of the worker.
        /// True if active otherwise false.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Number of unused holidays.
        /// </summary>
        public int NumberOfHolidays { get; set; }

        /// <summary>
        /// Number of born children.
        /// </summary>
        [MinimumChild(2)]
        public int Children { get; set; }

        /// <summary>
        /// List of availible stacks.
        /// </summary>
        public List<string> Stacks { get; set; }

        /// <summary>
        /// Imports <see cref="Worker"/>s from xml.
        /// </summary>
        /// <param name="path">Optional param, default value is the example file.</param>
        /// <returns>A list of workers.</returns>
        /// <exception cref="InvalidXmlException">If the parameters in the xml file were not given correctly.</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<Worker> Import(string path = "workers.xml")
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            var doc = XDocument.Load(path);

            var workersRaw = doc.Descendants("worker");

            var workers = new List<Worker>();

            foreach ( var workerRaw in workersRaw)
            {
                workers.Add(ToWorker(workerRaw));
            }

            return workers;
        }

        /// <summary>
        /// Converts an <see cref="XElement"/> to <see cref="Worker"/>.
        /// </summary>
        /// <param name="workerRaw">Worker as an <see cref="XElement"/>.</param>
        /// <returns>A <see cref="Worker"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidXmlException"></exception>
        private static Worker ToWorker(XElement workerRaw)
        {
            if(workerRaw is null) throw new ArgumentNullException(nameof(workerRaw));

            Worker worker;
            try
            {
                worker = new Worker
                {
                    Name = workerRaw.Descendants("name").First().Value,
                    Position = workerRaw.Descendants("position").First().Value,
                    Salary = int.Parse(workerRaw.Descendants("salary").First().Value),
                    Active = workerRaw.Descendants("active").First().Value == "true",
                    NumberOfHolidays = int.Parse(workerRaw.Descendants("numberofholidays").First().Value),
                    Children = int.Parse(workerRaw.Descendants("children").First().Value),
                    Stacks = GetStacks(workerRaw.Descendants("stack"))
                };
            }
            catch (Exception ex)
            {
                throw new FormatException(workerRaw.ToString(), ex);
            }

            return worker;
        }

        /// <summary>
        /// Gets the avaible stacks.
        /// </summary>
        /// <param name="stacks">The stacks as <see cref="XElement"/></param>
        /// <returns>A <see cref="string"/> <see cref="List{T}"/> of stacks.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static List<string> GetStacks(IEnumerable<XElement> stacks)
        {
            if(stacks is null) throw new ArgumentNullException(nameof(stacks));

            return stacks.Select(x => x.Value).ToList();
        }

        public override bool Equals(object obj)
        {
            return obj is Worker worker &&
                   Name == worker.Name &&
                   Position == worker.Position &&
                   Salary == worker.Salary &&
                   Active == worker.Active &&
                   NumberOfHolidays == worker.NumberOfHolidays &&
                   Children == worker.Children &&
                   EqualityComparer<List<string>>.Default.Equals(Stacks, worker.Stacks);
        }
    }
}
