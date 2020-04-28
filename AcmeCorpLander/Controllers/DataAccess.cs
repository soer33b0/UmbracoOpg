//using ClassLibrary;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using ClassLibrary;
//using AcmeCorpLander.Data;

//namespace AcmeCorpLander.Controllers
//{
//    public class DataAccess
//    {
//        private readonly AcmeDbContext _db;

//        public DataAccess(AcmeDbContext db)
//        {
//            _db = db;
//        }

//        static List<int> validSerialList = new List<int>();

//        readonly string path = @"serialNumbers.txt";

//        public void ValidateSubmission(Submission submission)
//        {
//            bool over18 = AgeCheck();
//            bool repeat = 
//        }

//        public List<int> GetSerialNumbers()
//        {
//            validSerialList.Clear();
//            FileInfo fileInfo = new FileInfo(@"serialNumbers.txt");
//            if (fileInfo.Exists)
//            {
//                FileStream fs = fileInfo.OpenRead();
//                StreamReader sr = new StreamReader(fs);
//                while (sr.Peek() != -1)
//                {
                    
//                    validSerialList.Add(Convert.ToInt32(sr.ReadLine()));
//                }

//                sr.Close();
//                fs.Close();
//            }

//            return validSerialList;
//        }

//        public bool ValidateSubmission(Submission submission)
//        {
//            if (File.ReadAllText(path).Contains(submission.SerialNum.ToString()))
//            {
//                return true;
//            }

//            RegisterCustomer(submission.FirstName, submission.LastName, submission.Email, submission.Age);

//            return false;
//        }


//        public bool AgeCheck(Submission submission)
//        {
//            if (submission.Age > 18)
//            {
//                return true;
//            }
//            return false;
//        }

//        public bool RepeatCheck(Submission submission)
//        {
//            foreach (i int in dbSerials)
//            {

//            }
//            if (submission.SerialNum == _db.Submission.)
//            {
//                return true;
//            }
//        }
       
//    }
//}
