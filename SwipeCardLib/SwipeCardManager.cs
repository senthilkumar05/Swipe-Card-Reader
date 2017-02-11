using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{
    public class SwipeCardManager : ISwipeCard
    {
        IEnumerable<SwipeData> swipeData = null;

        public SwipeCardManager(ISwipeCardData swipeSource)
        {
            swipeData = swipeSource.GetSwipeData();
        }

        public IEnumerable<SwipeData> SwipeData
        {
            get
            {
                return swipeData;
            }
        }

        public IEnumerable<EmployeeSwipeInAndOut> GetFirstInLastOutSwipeByEmpId(string empId)
        {
            List<EmployeeSwipeInAndOut> FirstInOutList = new List<EmployeeSwipeInAndOut>();

            if (swipeData == null)
                return FirstInOutList;

            var d = swipeData.Where(p => p.EmployeeID == empId).OrderBy(p => p.Date).GroupBy(p => p.Date.Date).ToList();
            foreach (var g in d)
            {
                EmployeeSwipeInAndOut info = new EmployeeSwipeInAndOut();
                info.EmployeeId = empId;

                var first = g.FirstOrDefault(p => p.SwipeMode == SwipeMode.IN);
                if (first != null)
                    info.SwipeIn = first.Date;
                
                var last = g.LastOrDefault(p => p.SwipeMode == SwipeMode.OUT);
                if (last != null)
                    info.SwipeOut = last.Date;

                FirstInOutList.Add(info);

            }
            return FirstInOutList;
        }

        public IEnumerable<WorkTimeInfo> GetWorkingTimeInfo(string empId, string shiftStartTime, string shiftEndTime)
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

        private DateTime GetShitTime(DateTime shiftDate, string shiftTime)
        {
            string format = string.Format("{0} {1}", shiftDate.ToString("yyyyMMdd"), shiftTime);
            try
            {
                return DateTime.ParseExact(format, "yyyyMMdd HH:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return DateTime.ParseExact(format, "yyyyMMdd hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        private WorkTimeInfo GetWorkingTimeInfo(IEnumerable<SwipeData> swipeData, DateTime shiftStartTime, DateTime shiftEndTime)
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
}
