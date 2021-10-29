using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyProject
{
    class Drug
    {
        public string Name { get; }
        public Models.DrugType Type { get; }
        public int Price { get; }
        public int Count { get; set; }
        public string Info { get; }
        public int Id { get; }
        private static int _counter;
        public Drug()
        {
            _counter++;
            Id = _counter;
        }
        public Drug(string name ,Models.DrugType type, int price , int count,string info):this()
        {
            Name = name;
            Type = type;
            Price = price;
            Count = count;
            Info = info;
        }
        public override string ToString()
        {
            return Id + "." + Name +"("+"count:"+ Count +") :"+ Price;
        }
    }
}
