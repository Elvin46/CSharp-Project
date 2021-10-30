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
                            Help.Print("1.Painkiller\n2.Antibiotics\n3.Antibacterials\n4.Vitamins");
                            string type = Console.ReadLine();
                            if (type.ToLower().Trim()!= "painkiller" && type.ToLower().Trim() != "antibiotics" && type.ToLower().Trim() != "antibacterials" && type.ToLower().Trim() != "vitamins")
                            {
                                Help.Print("Enter The Correct Type", ConsoleColor.DarkRed);
                            }
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
                            if (existPharmacy.ShowDrugItems()==null)
                            {
                                Help.Print("There isn't Drug in Pharmacy", ConsoleColor.DarkRed);
                                break;
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
                            Help.Print(drug.ToString(), ConsoleColor.Green);

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
                                if (item.ShowDrugItems() == null)
                                {
                                    Help.Print("There isn't Drug in Pharmacy", ConsoleColor.DarkRed);
                                    continue;
                                }
                                foreach (var item1 in item.ShowDrugItems())
                                {
                                    Help.Typing($"{item1.Id}.{item1.Name}", ConsoleColor.Cyan);
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
                            int counter = 0;
                            foreach (var item in pharmacies)
                            {
                                Help.Print(item.ToString(), ConsoleColor.Green);
                                if (item.ShowDrugItems() == null)
                                {
                                    counter++;
                                }
                            }
                            if (counter == 0)
                            {
                                Help.Print("There isn't drug in any pharmacy", ConsoleColor.DarkRed);
                                goto case Functions.AddDrug;
                            }
                            Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                        inputPharmacyName3:
                            pharmacyName = Console.ReadLine();
                            Help.Blink();
                            existPharmacy = pharmacies.Find(x => x.Name.ToLower() == pharmacyName.ToLower());
                            if (existPharmacy == null)
                            {
                                Help.Print("Choose Correct Pharmacy Name", ConsoleColor.DarkRed);
                                goto inputPharmacyName3;
                            }
                            if (existPharmacy.ShowDrugItems() == null)
                            {
                                Help.Print("There isn't Drug in Pharmacy", ConsoleColor.DarkRed);
                                Help.Print("Choose another Pharmacy", ConsoleColor.DarkRed);
                                foreach (var item in pharmacies)
                                {
                                    Help.Print(item.ToString(), ConsoleColor.Green);
                                }
                                goto inputPharmacyName3;
                            }
                            existPharmacy.ShowDrugItems();
                            Help.Print("Enter Your Cash", ConsoleColor.Yellow);
                            int cash = Help.Parse();
                        inputDrugName2:
                            Help.Print("Enter the Name of Drug", ConsoleColor.Yellow);
                            name = Console.ReadLine();
                            inputDrugAmount:
                            Help.Print("Enter the Amount of Drug", ConsoleColor.Yellow);
                            count = Help.Parse();
                            Help.Blink();
                            Drug findDrug = existPharmacy.SaleDrug(name, count, cash);
                            if (findDrug == null || findDrug.Count == 0)
                            {
                                Help.Print("This drug doesn't exist", ConsoleColor.DarkRed);
                                Help.Print("Do you want to buy another drug?yes/no", ConsoleColor.Yellow);
                                string ans = Console.ReadLine();
                                if (ans.ToLower() == "yes")
                                {
                                    existPharmacy.ShowDrugItems();
                                    goto inputDrugName2;
                                }
                                if (ans.ToLower() == "no")
                                {
                                    break;
                                }
                                Help.Print("Enter The correct answer!!!", ConsoleColor.DarkRed);
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
                                    break;
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
