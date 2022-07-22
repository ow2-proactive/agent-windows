using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration conf = ConfigurationParser.parseXml("example2.xml");

            System.Console.WriteLine(conf);
        }
    }
}
