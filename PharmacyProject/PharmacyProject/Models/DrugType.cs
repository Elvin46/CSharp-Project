using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyProject.Models
{
    class DrugType
    {
        public string TypeName { get; }
        public int Id { get; }
        private static int _counter;
        public DrugType(string typeName)
        {
            TypeName = typeName;
            _counter++;
            Id = _counter;
        }
        public override string ToString()
        {
            return TypeName;
        }
    }
}
