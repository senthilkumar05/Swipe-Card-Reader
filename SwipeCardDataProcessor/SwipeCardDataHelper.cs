using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardDataProcessor
{
    public class SwipeCardDataHelper
    {
        public static SwipeData GetSwipeData(string swipeData)
        {
            if (swipeData.Length != 37)
                return null;

            SwipeData d = new SwipeData();
            d.EmployeeID = swipeData.Substring(5, 12);
            string swipeTime = swipeData.Substring(18, 12);
            d.Date = DateTime.ParseExact(swipeTime, "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
            //Swipe Mode
            string mode = swipeData.Substring(30, 1);
            if (mode == "I" || mode == "i")
            {
                d.SwipeMode = SwipeMode.IN;
            }
            else if (mode == "O" || mode == "o")
            {
                d.SwipeMode = SwipeMode.OUT;
            }

            d.SwipeDeviceId = swipeData.Substring(31);

            return d;
        }

        public static List<SwipeData> GetSwipeData(IEnumerable<string> swipeData)
        {
            List<SwipeData> sdata = new List<SwipeData>();
            foreach (var d in swipeData)
                sdata.Add(GetSwipeData(d));

            return sdata;
        }

        public static List<EmployeeSwipeFirstInLastOutInfo> GetSwipeFirstInLastOutInfoByEmpId(List<SwipeData> swipeData, string empId)
        {
            List<EmployeeSwipeFirstInLastOutInfo> FirstInOutList = new List<EmployeeSwipeFirstInLastOutInfo>();

            var d = swipeData.Where(p => p.EmployeeID == empId).OrderBy(p => p.Date).GroupBy(p => p.Date.Date).ToList();
            foreach (var g in d)
            {

                EmployeeSwipeFirstInLastOutInfo info = new EmployeeSwipeFirstInLastOutInfo();
                info.EmployeeId = empId;

                var first = g.First(p => p.SwipeMode == SwipeMode.IN);
                if (first != null)
                    info.SwipeIn = first.Date;

                var last = g.Last(p => p.SwipeMode == SwipeMode.OUT);
                if (last != null)
                    info.SwipeOut = last.Date;

                FirstInOutList.Add(info);

            }
            return FirstInOutList;
        }

        public static List<WorkTimeInfo> GetSwipeEveryValidInOutInfoByEmpId(List<SwipeData> swipeData, string empId, string shiftStartTime, string shiftEndTime)
        {
            List<WorkTimeInfo> workTimeList = new List<WorkTimeInfo>();

            var d = swipeData.Where(p => p.EmployeeID == empId).OrderBy(p => p.Date).GroupBy(p => p.Date.Date).ToList();
            foreach (var g in d)
            {
                DateTime shiftStart = GetShitTime(g.Key, shiftStartTime);
                DateTime shiftEnd = GetShitTime(g.Key, shiftEndTime);

                workTimeList.Add(GetWorkingTimeInfo(g.ToList(), shiftStart, shiftEnd));
            }
            return workTimeList;
        }

        private static DateTime GetShitTime(DateTime shiftDate, string shiftTime)
        {
            string format = string.Format("{0} {1}", shiftDate.ToString("yyyyMMdd"), shiftTime);
            try {
                return DateTime.ParseExact(format, "yyyyMMdd HH:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return DateTime.ParseExact(format, "yyyyMMdd hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        public static WorkTimeInfo GetWorkingTimeInfo(List<SwipeData> swipeData, DateTime shiftStartTime, DateTime shiftEndTime)
        {
            int flag = 0;
            int before = 0;
            int late = 0;
            int totworkingMinutes = 0;
            int extra = 0;
            DateTime? lastIn = null;
            foreach (var d in swipeData)
            {
                if (flag == 0 && d.SwipeMode == SwipeMode.IN)
                {
                    flag++;
                    if (d.Date < shiftStartTime)
                    {
                        before = shiftStartTime.Subtract(d.Date).Minutes;
                        lastIn = shiftStartTime;
                    }
                    else if (d.Date > shiftStartTime)
                    {
                        if (late == 0)
                        {
                            late = d.Date.Subtract(shiftStartTime).Minutes;
                        }
                        lastIn = d.Date;
                    }
                    else
                    {
                        lastIn = shiftStartTime;
                    }
                }
                else if (flag == 1 && d.SwipeMode == SwipeMode.OUT)
                {
                    flag--;
                    if (lastIn != null)
                    {
                        if (d.Date > shiftEndTime)
                        {
                            if (extra == 0)
                            {
                                totworkingMinutes += shiftEndTime.Subtract(lastIn.Value).Minutes;
                            }

                            extra += d.Date.Subtract(shiftEndTime).Minutes;
                        }
                        else if (d.Date <= shiftEndTime)
                        {
                            totworkingMinutes += d.Date.Subtract(lastIn.Value).Minutes;
                        }
                        lastIn = null;
                    }
                }

            }

            return new WorkTimeInfo
            {
                ExtraWorkInMinutes = extra + before,
                LateInMinutes = late,
                TotalWorkInMinutes = totworkingMinutes
            };
        }
    }

    public class WorkTimeInfo
    {
        public int TotalWorkInMinutes { get; set; }
        public int LateInMinutes { get; set; }
        public int ExtraWorkInMinutes { get; set; }
    }

    public class SwipeData
    {
        public string EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public SwipeMode SwipeMode { get; set; }

        public string SwipeDeviceId { get; set; }

    }

    public class EmployeeSwipeFirstInLastOutInfo
    {
        public string EmployeeId { get; set; }
        public DateTime SwipeIn { get; set; }
        public DateTime SwipeOut { get; set; }

    }

    public enum SwipeMode
    {
        IN,
        OUT
    }


}
