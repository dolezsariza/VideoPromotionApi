using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPromotionApi.Services
{
    public class FileHandler
    {
        public string[] ReadFromFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"VideoPromotionApi\..\..\..\Assets\API.txt");

            return lines;
        }
    }
}
