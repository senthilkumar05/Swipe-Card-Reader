using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace SwipeCardDataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
             
            var s = DateTime.ParseExact("10:30:00 AM","HH:mm:ss tt",System.Globalization.CultureInfo.InvariantCulture);
            string[] swipedataStrings = System.IO.File.ReadAllLines("swipedata.txt");
            List<SwipeData> swipeDataList = SwipeCardDataHelper.GetSwipeData(swipedataStrings);

            List<string> employees = swipeDataList.Select(p => p.EmployeeID).Distinct().ToList();
            foreach (var empid in employees)
            {
                //var swipeFirtstInLastOut = SwipeCardDataHelper.GetSwipeFirstInLastOutInfoByEmpId(swipeDataList, empid);
                var xx = SwipeCardDataHelper.GetSwipeEveryValidInOutInfoByEmpId(swipeDataList, empid, "10:00:00 AM", "06:00:00 PM");
               
            }


            SwipeCardDataProcessor.AttendanceSection config =
        (SwipeCardDataProcessor.AttendanceSection)System.Configuration.ConfigurationManager.GetSection("attendance");
            
            Console.ReadLine();


        }
    }
}
