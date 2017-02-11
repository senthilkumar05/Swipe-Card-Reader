# Swipe-Card-Reader
This is simple C# library to read and manipulate employee's swipe card data

<h2>Sample Swipe card data</h2>

00001000000000040 201604011150I000001<br/>
00001000000000040 201604011230O000001<br/>
00001000000000040 201604011240I000001<br/>
00001000000000040 201604011850O000001<br/>

<h2>Usage<h2>

            string empId = "000000000040";
            string swipeDataFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "swipedata.txt");

      
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
      
