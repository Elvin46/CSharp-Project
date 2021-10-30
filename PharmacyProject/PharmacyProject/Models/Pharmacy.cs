using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyProject.Models
{
    partial class Pharmacy
    {
        public string Name { get; }
        private List<Drug> _drugs;
        public List<DrugType> _drugTypes;

        public int Id { get; }
        private static int _counter;
        public Pharmacy(string name)
        {
            Name = name;
            _counter++;
            Id = _counter;
            _drugs = new List<Drug>();
        }
    }
}
