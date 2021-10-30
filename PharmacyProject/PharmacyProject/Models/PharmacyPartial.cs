using PharmacyProject.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyProject.Models
{
    partial class Pharmacy
    {
        public override string ToString()
        {
            return Id + " - " + Name;
        }
        public bool AddDrug(Drug drug)
        {
            Drug findDrug = _drugs.Find(x => x.Name.ToLower() == drug.Name.ToLower());
            if (findDrug!=null)
            {
                findDrug.Count+=drug.Count;
                return false;
            }
            _drugs.Add(drug);
            return true;
        }
        public Drug InfoDrug(string name)
        {
            Drug findDrug = _drugs.Find(x => x.Name.ToLower() == name.ToLower());
            if (findDrug==null)
            {
                return null;
            }
            return findDrug;
        }
        public List<Drug> ShowDrugItems()
        {
            if (_drugs.Count == 0)
            {
                return null;
            }
            return _drugs;
            
        }
        public Drug SaleDrug(string name,int count,int cash)
        {
            Drug findDrug = _drugs.Find(x => x.Name.ToLower() == name.Trim().ToLower());
            return findDrug;
        }

    }
    enum Errors
    {
        DoesntExist = 1,
        NotEnoughCount,
        NotEnoughCash,
        True
    }
}
