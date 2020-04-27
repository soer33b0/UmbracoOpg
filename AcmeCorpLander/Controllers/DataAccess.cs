using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorpLander.Controllers
{
    public class DataAccess
    {
        static List<int> validSerialList = new List<int>();

        public static List<int> GetSerialNumbers()
        {
            validSerialList.Clear();
            FileInfo fileInfo = new FileInfo(@"serialNumbers.txt");
            if (fileInfo.Exists)
            {
                FileStream fs = fileInfo.OpenRead();
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() != -1)
                {
                    
                    validSerialList.Add(Convert.ToInt32(sr.ReadLine()));
                }

                sr.Close();
                fs.Close();
            }

            return validSerialList;
        }

    }
}
