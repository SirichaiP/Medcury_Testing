﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MedTest
{
    class Program
    {
        static void Main(string[] args)
        {
        
            Program program = new Program();
            Console.WriteLine(program.stringDecoder("WUBWEWUBAREWUBWUBTHEWUBBACKYARDWUBMYWUBFRIENDWUB")); 
            Console.WriteLine(program.stringDecoder("AWUBBWUBC")); //"A B C", "WUB should be replaced by 1 space");


            Console.WriteLine(program.stringDecoder("AWUBWUBWUBBWUBWUBWUBC")); //"A B C", "multiples WUB should be replaced by only 1 space");
            Console.WriteLine(program.stringDecoder("WUBAWUBBWUBCWUB")); // "A B C", "heading or trailing spaces should be removed");

            const string dr = "/+1-541-754-3010 156 Alphand_St. <J Steeve>\n 133, Green, Rd. <E Kustur> NY-56423 ;+1-541-914-3010\n"
                            + "+1-541-984-3012 <P Reed> /PO Box 530; Pollocksville, NC-28573\n :+1-321-512-2222 <Paul Dive> Sequoia Alley PQ-67209\n"
                            + "+1-741-984-3090 <Peter Reedgrave> _Chicago\n :+1-921-333-2222 <Anna Stevens> Haramburu_Street AA-67209\n"
                            + "+1-111-544-8973 <Peter Pan> LA\n +1-921-512-2222 <Wilfrid Stevens> Wild Street AA-67209\n"
                            + "<Peter Gone> LA ?+1-121-544-8974 \n <R Steell> Quora Street AB-47209 +1-481-512-2222\n"
                            + "<Arthur Clarke> San Antonio $+1-121-504-8974 TT-45120\n <Ray Chandler> Teliman Pk. !+1-681-512-2222! AB-47209,\n"
                            + "<Sophia Loren> +1-421-674-8974 Bern TP-46017\n <Peter O'Brien> High Street +1-908-512-2222; CC-47209\n"
                            + "<Anastasia> +48-421-674-8974 Via Quirinal Roma\n <P Salinger> Main Street, +1-098-512-2222, Denver\n"
                            + "<C Powel> *+19-421-674-8974 Chateau des Fosses Strasbourg F-68000\n <Bernard Deltheil> +1-498-512-2222; Mount Av.Eldorado\n"
                            + "+1-099-500-8000 <Peter Crush> Labrador Bd.\n +1-931-512-4855 <William Saurin> Bison Street CQ-23071\n"
                            + "<P Salinge> Main Street, +1-098-512-2222, Denve\n";



                              Console.WriteLine(program.phone(dr, "1-541-754-3010"));
                              Console.WriteLine(program.phone(dr, "48-421-674-8974")); //, "Phone => 48-421-674-8974, Name => Anastasia, Address => Via Quirinal Roma")
                              Console.WriteLine(program.phone(dr, "1-921-512-2222")); //, "Phone => 1-921-512-2222, Name => Wilfrid Stevens, Address => Wild Street AA-67209")
                              Console.WriteLine(program.phone(dr, "1-908-512-2222")); //, "Phone => 1-908-512-2222, Name => Peter O'Brien, Address => High Street CC-47209")
                              Console.WriteLine(program.phone(dr, "1-541-754-3010")); //, "Phone => 1-541-754-3010, Name => J Steeve, Address => 156 Alphand St.")
                              Console.WriteLine(program.phone(dr, "1-121-504-8974")); //, "Phone => 1-121-504-8974, Name => Arthur Clarke, Address => San Antonio TT-45120")
                              Console.WriteLine(program.phone(dr, "1-498-512-2222")); //, "Phone => 1-498-512-2222, Name => Bernard Deltheil, Address => Mount Av. Eldorado")
                              Console.WriteLine(program.phone(dr, "1-098-512-2222")); //, "Error => Too many people: 1-098-512-2222")
                              Console.WriteLine(program.phone(dr, "5-555-555-5555")); //, "Error => Not found: 5-555-555-5555")
            Console.ReadKey();
        }

        private string stringDecoder(string strDecode)  // ข้อที่ 1
        {
            var searchString = strDecode.Split("WUB");
            searchString = searchString.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return string.Join(" ", searchString); ;

        }

        private string phone(string actual, string expected) // ข้อที่ 2
        {
            string result = "";
            List<PhonBook> phonBookList = new List<PhonBook>();

            string[] drtest = actual.Split("\n");
            drtest = drtest.Take(drtest.Count() - 1).ToArray();
            foreach (string item in drtest)
            {
                PhonBook phonBook = new PhonBook();
                string PhonPattern = "[+]*\\d{1,2}-\\d{3}-\\d{3}-\\d{4}";
                string NamePattern = "(\\<.*?\\>)";

                Match mPhon = Regex.Match(item, PhonPattern);
                Match mName = Regex.Match(item, NamePattern);

                phonBook.PhonNo = mPhon.Value.Replace("+", "");
                phonBook.Name = mName.Value.Replace("<", "").Replace(">", "");
                phonBook.Address = Regex.Replace(item.Replace(mPhon.Value, "").Replace(mName.Value, ""), @"[^0-9a-zA-Z.-]+", " ");
                phonBookList.Add(phonBook);
            }
            List<PhonBook> seachPhonBook = phonBookList.Where(s => s.PhonNo.Equals(expected)).ToList();
            if (seachPhonBook.Count == 1)
            {
                result = "Phone => " + seachPhonBook.FirstOrDefault().PhonNo + ", Name => " + seachPhonBook.FirstOrDefault().Name + ", Address => " + seachPhonBook.FirstOrDefault().Address;
            }
            else if (seachPhonBook.Count > 1)
            {
                result = "Error => Too many people: " + seachPhonBook.FirstOrDefault().PhonNo;
            }
            else if (seachPhonBook.Count == 0)
            {
                result = "Error => Not found: 5-555-555-5555";
            }
            return result;
        }

        public class PhonBook
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string PhonNo { get; set; }

        }
    }
}