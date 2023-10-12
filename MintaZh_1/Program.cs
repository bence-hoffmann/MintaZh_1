using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MintaZh_1
{
    internal static class Program
    {

        /// <summary>
        /// Pretty console writer for <see cref="IQueryable{T}"/> implementations.
        /// </summary>
        /// <typeparam name="T">Any object.</typeparam>
        /// <param name="anwsers">The answer to the quesrtion.</param>
        /// <param name="questionName">The name of the question.</param>
        private static void QuestionWriter<T>(this IQueryable<T> anwsers, string questionName)
        {
            anwsers.ToList().QuestionWriter(questionName);
        }

        /// <summary>
        /// Pretty console writer for <see cref="IEnumerable{T}"/> implementations.
        /// </summary>
        /// <typeparam name="T">Any object.</typeparam>
        /// <param name="anwsers">The answer to the quesrtion.</param>
        private static void QuestionWriter<T>(this IEnumerable<T> anwsers, string questionName)
        {
            Console.WriteLine($"----------------------------------Question: {questionName}------------------------------------------");
            Console.WriteLine("**************************");

            foreach (var item in anwsers)
            {
                Console.WriteLine(item);
                Console.WriteLine("**************************");
            }

            Console.Write("------------------------------------------------------------------------------------");
            for (int i = 0; i < (questionName.Length); i++) Console.Write('-');
            Console.WriteLine("\n\n");
        }

        static void Main(string[] args)
        {
            var workers = Worker.Import();
            workers.QuestionWriter("Xml read");

            var fileteredWorkers = WorkerFilter.Filter(workers);
            fileteredWorkers.QuestionWriter("Attribute & Filter workers.");

            LinqQueries(workers);
        }

        /// <summary>
        /// Linq queries.
        /// </summary>
        /// <param name="workers">List of workers.</param>
        private static void LinqQueries(List<Worker> workers)
        {
            //-Mennyi a Juniorok össz fizetése?
            var q1 = from worker in workers
                     group worker by worker.Position
                     into positions
                     select new
                     {
                         Position = positions.Key,
                         SumSalary = positions.Sum(x => x.Salary)
                     };

            new List<int> { q1.First(x => x.Position == "Junior").SumSalary }.QuestionWriter("Q1, Mennyi a Juniorok össz fizetése?");

            //-Hányan értenek a Javahoz? 

            var q2 = from worker in workers
                     where worker.Stacks.Count(x => x.Equals("Java", StringComparison.InvariantCultureIgnoreCase)) > 0
                     select worker;

            new List<int> { q2.Count() }.QuestionWriter("Q2, Hányan értenek a Javahoz?");

            //-Mennyi az átlag fizetése az aktív dolgozóknak?
            var q3 = from worker in workers
                     group worker by worker.Active
                     into activeWorkers
                     select new
                     {
                         Active = activeWorkers.Key,
                         AvgSalary = activeWorkers.Average(x => x.Salary)
                     };

            new List<string> { q3.First(x => x.Active == true).AvgSalary.ToString("N2") }.QuestionWriter("Q3, //-Mennyi az átlag fizetése az aktív dolgozóknak?");

            var q4 = from worker in workers
                     orderby worker.Stacks.Count descending
                     select worker;

            q4.Take(1).QuestionWriter("Q4, Mennyi az átlag fizetése az aktív dolgozóknak?");

            //-Mennyi az átlag fizu pozicióként?
            var q5 = from worker in workers
                     group worker by worker.Position
                     into positions
                     select new
                     {
                         Position = positions.Key,
                         AvgSalary = positions.Average(x => x.Salary).ToString("N2")
                     };

            q5.QuestionWriter("Q5, Mennyi az átlag fizu pozicióként?");

            //Válasszuk ki azokat a .Net fejleszőket akik vagy medior vagy senior szinten vannak.
            var q6 = from worker in workers
                     where worker.Position.Equals("medior", StringComparison.InvariantCultureIgnoreCase)
                        || worker.Position.Equals("senior", StringComparison.InvariantCultureIgnoreCase)
                     select new ActiveProjectMember
                     {
                         Name = worker.Name,
                         Salary = worker.Salary,
                         Position = worker.Position
                     };

            q6.QuestionWriter("Q6, Válasszuk ki azokat a .Net fejleszőket akik vagy medior vagy senior szinten vannak.");

            UploadToDb(q6.ToList());
        }

        private static void UploadToDb(List<ActiveProjectMember> list)
        {
            using (DbContext dbContext = new ActiveProjectMemberDbContext())
            {
                list.ForEach(x => dbContext.Add(x));

                dbContext.SaveChanges();
            }
        }
    }
}
