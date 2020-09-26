using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPromotionApi.Services
{
    public class FileHandler
    {
        public string[] ReadFromFile()
        {
            string[] lines = new string[] { "psId","accessKey" };
            try
            {
                lines = File.ReadAllLines(@"..\..\..\VideoPromotionApi\Assets\API.txt");
            } 
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"File not found. Please use Assets\API.txt to provide PSID and access key.");
            }

            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The file or directory cannot be found.");
            }
            catch (DriveNotFoundException)
            {
                Console.WriteLine("The drive specified in 'path' is invalid.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lines;
        }

    }
}
