using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedTest
{
    public class songDecoder
    {
        public string strDecoder(string strDecode)  // ข้อที่ 1
        {
            var searchString = strDecode.Split("WUB");
            searchString = searchString.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return string.Join(" ", searchString); ;

        }
    }
}
