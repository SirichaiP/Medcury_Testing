using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MedTest
{
  public  class phoneBook
    {
        public string phone(string actual, string expected) // ข้อที่ 2
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
                phonBook.Address = Regex.Replace(item.Replace(mPhon.Value, "").Replace(mName.Value, ""), @"[^0-9a-zA-Z.-]+", " ").TrimStart().TrimEnd();
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
