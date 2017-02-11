using System;
using System.Collections.Generic;
using SwipeCardLib;
using System.IO;

namespace SwipeCardDataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string empId = "000000000040";
            string swipeDataFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "swipedata.txt");

            // Read swipe data from txt file.
            SwipeDataFromFile swipeDate = new SwipeDataFromFile(swipeDataFile);

            // Create swipeCardManager to process swipe data
            SwipeCardManager swipeCard = new SwipeCardManager(swipeDate);

            // Get all swipe data
            var data = swipeCard.SwipeData;

            // Get employee's first in and last out swipe data
            Console.WriteLine("Emplyee First in and last out data");
            IEnumerable<EmployeeSwipeInAndOut> swipeInOuts = swipeCard.GetFirstInLastOutSwipeByEmpId(empId);
            foreach(var io in swipeInOuts)
            {
                Console.WriteLine("EmpId:{0}\tIn:{1}\tOut:{2}", io.EmployeeId, io.SwipeIn, io.SwipeOut);
            }

            Console.WriteLine("\nEmployee Work info");
            var workInfos = swipeCard.GetWorkingTimeInfo(empId, "10:00:00 AM", "06:00:00 PM");
            foreach(var info in workInfos)
            {
                Console.WriteLine("EmpId:{0}\tWork:{1}\tLate:{2}\tExtra:{3}",
                    info.EmployeeId, info.TotalWorkInMinutes, info.LateInMinutes, info.ExtraWorkInMinutes);
            }

           Console.ReadLine();


        }
    }
}
