using PharmacyProject.Helper;
using PharmacyProject.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PharmacyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            while (true)
            {
                Help.Typing("1-Create Pharmacy \n2-Add Drug \n3-Information about Drug\n4-Show List of Drugs\n5-Sale Drug\n6-Exit\n");
                int check = Help.Parse();
                if (check >= 1 && check <= 6)
                {
                    Functions func = (Functions)check;
                    if (func == Functions.Exit)
                    {
                        break;
                    }
                    switch (func)
                    {
                        #region CreatePharmacy

                        case Functions.CreatePharmacy:
                            Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                            string pharmacyName = Console.ReadLine();
                            Help.Blink();
                            if (pharmacies.Exists(x => x.Name.ToLower() == pharmacyName.ToLower()))
                            {
                                Help.Print("This Pharmacy is Exist", ConsoleColor.Red);
                                goto case Functions.CreatePharmacy;
                            }
                            Pharmacy pharmacy = new Pharmacy(pharmacyName);
                            pharmacies.Add(pharmacy);
                            Help.Print("Creating is succesfull", ConsoleColor.Green);
                            Thread.Sleep(500);
                            break;
                        #endregion

                        #region AddDrug

                        case Functions.AddDrug:
                            if (pharmacies.Count == 0)
                            {
                                Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                Help.Print("Create Pharmacy!", ConsoleColor.Yellow);
                                goto case Functions.CreatePharmacy;
                            }

                            Help.Print("Enter The Name of Drug", ConsoleColor.Yellow);
                            string name = Console.ReadLine();
                            Help.Print("Enter The Type of Drug", ConsoleColor.Yellow);
                            string type = Console.ReadLine();
                            DrugType drugType = new DrugType(type);
                            Help.Print("Enter The Price of Drug", ConsoleColor.Yellow);
                            int price = Help.Parse();
                            Help.Print("Enter The Count of Drug", ConsoleColor.Yellow);
                            int count = Help.Parse();
                            Console.Clear();
                        inputPharmacyName:
                            Help.Typing("List of Pharmacies:\n", ConsoleColor.Yellow);
                            foreach (var item in pharmacies)
                            {
                                Help.Print(item.ToString(), ConsoleColor.Green);
                            }

                            Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                            pharmacyName = Console.ReadLine();
                            Console.Clear();
                            Help.Blink();
                            Pharmacy existPharmacy = pharmacies.Find(x => x.Name.ToLower() == pharmacyName.ToLower());
                            if (existPharmacy == null)
                            {
                                Help.Print("Choose Correct Pharmacy Name", ConsoleColor.Red);
                                goto inputPharmacyName;
                            }

                            Drug drug = new Drug(name, drugType,price,count);
                            if (!existPharmacy.AddDrug(drug))
                            {
                                Help.Print("This Drug is exist",ConsoleColor.DarkRed);
                                Help.Print($"The Count of {drug.Name} has increased {drug.Count} count", ConsoleColor.Green);
                                break;
                            }
                            Help.Print($"{drug.Name} added to {existPharmacy.Name} Pharmacy", ConsoleColor.Green);
                            break;
                        #endregion

                        #region InfoDrug

                        case Functions.InfoDrug:
                            if (pharmacies.Count == 0)
                            {
                                Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                goto case Functions.CreatePharmacy;
                            }
                            Help.Print("List of Pharmacies", ConsoleColor.Yellow);
                            foreach (var item in pharmacies)
                            {
                                Help.Print(item.ToString(), ConsoleColor.Green);
                            }

                            Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                            inputPharmacyName2:
                            pharmacyName = Console.ReadLine();
                            existPharmacy = pharmacies.Find(x => x.Name.ToLower() == pharmacyName.ToLower());
                            if (existPharmacy == null)
                            {
                                Help.Print("Choose Correct Pharmacy Name", ConsoleColor.Red);
                                goto inputPharmacyName2;
                            }
                            Help.Print("The List of Drugs");
                            foreach (var item in existPharmacy.ShowDrugItems())
                            {
                                Help.Print(item.Name, ConsoleColor.Cyan);
                            }
                            Help.Print("Enter the Name of Drug", ConsoleColor.Yellow);
                            inputDrugName:
                            name = Console.ReadLine();
                            if (existPharmacy.InfoDrug(name)==null)
                            {
                            Help.Print("This drug doesn't exist", ConsoleColor.DarkRed);
                            goto inputDrugName;
                            }
                            drug = existPharmacy.InfoDrug(name);
                            Help.Print($"{drug.Id}.{drug.ToString()}", ConsoleColor.Green);

                            break;
                        #endregion

                        #region ShowDrugItems

                        case Functions.ShowDrugItems:
                            Console.Clear();
                            if (pharmacies.Count==0)
                            {
                                Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                goto case Functions.CreatePharmacy;
                            }
                            foreach (var item in pharmacies)
                            {
                                Help.Typing(item.ToString(), ConsoleColor.Yellow);
                                foreach (var item1 in item.ShowDrugItems())
                                {
                                    Help.Typing(item1.Name, ConsoleColor.Cyan);
                                }
                            }
                            break;
                        #endregion

                        #region SaleDrug

                        case Functions.SaleDrug:
                            if (pharmacies.Count == 0)
                            {
                                Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                goto case Functions.CreatePharmacy;
                            }
                            Help.Typing("List of Pharmacies", ConsoleColor.Yellow);
                            foreach (var item in pharmacies)
                            {
                                Help.Print(item.ToString(), ConsoleColor.Green);
                            }

                            Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                        inputPharmacyName3:
                            pharmacyName = Console.ReadLine();
                            existPharmacy = pharmacies.Find(x => x.Name.ToLower() == pharmacyName.ToLower());
                            if (existPharmacy == null)
                            {
                                Help.Print("Choose Correct Pharmacy Name", ConsoleColor.Red);
                                goto inputPharmacyName3;
                            }
                        inputDrugName2:
                            Help.Print("Enter the Name of Drug", ConsoleColor.Yellow);
                            name = Console.ReadLine();
                            inputDrugAmount:
                            Help.Print("Enter the Amount of Drug", ConsoleColor.Yellow);
                            count = Help.Parse();
                            Help.Print("Enter Your Cash", ConsoleColor.Yellow);
                            int cash = Help.Parse();
                            Help.Blink();
                            Drug findDrug = existPharmacy.SaleDrug(name, count, cash);
                            if (findDrug == null)
                            {
                                Help.Print("This drug doesn't exist", ConsoleColor.DarkRed);
                                goto inputDrugName2;
                            }
                            if (findDrug.Count < count)
                            {
                                Help.Print($"We have just {findDrug.Count} {findDrug.Name}", ConsoleColor.DarkRed);
                                goto inputDrugAmount;
                            }
                            if (cash < findDrug.Price * count)
                            {
                                Help.Print($"Total Amount: {findDrug.Price * count}\nYour Cash isn't Enough",ConsoleColor.DarkRed);
                                if (cash < findDrug.Price)
                                {
                                    Help.Print("You can't buy this drug:(", ConsoleColor.DarkRed);
                                }
                                goto inputDrugAmount;
                            }
                            findDrug.Count -= count;
                            Help.Print("Buying is Succesfull!", ConsoleColor.Green);
                            break;
                        #endregion

                        default:
                            break;
                    }
                }
            }
        }
    }
    enum Functions
    {
        CreatePharmacy = 1,
        AddDrug,
        InfoDrug,
        ShowDrugItems,
        SaleDrug,
        Exit
    }
}
