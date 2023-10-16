using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZh_1
{
    public class ActiveProjectMember
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }

#error Optional
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Name: ");
            sb.AppendLine(Name);

            sb.Append("Salary: ");
            sb.AppendLine(Salary.ToString());

            sb.Append("Position: ");
            sb.AppendLine(Position);

            return sb.ToString();
        }
    }
}
