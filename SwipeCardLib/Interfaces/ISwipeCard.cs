using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{ 
    /// <summary>
    /// Swipe card functionalities
    /// </summary>
   public interface ISwipeCard
    {
        /// <summary>
        /// Get swipe data
        /// </summary>
        IEnumerable<SwipeData> SwipeData { get; }

        /// <summary>
        /// Get First in and last out of swipe data for an employee
        /// </summary>
        /// <param name="empId">Employee id to get swipe data </param>
        /// <returns>Employee's swipe first in and out data</returns>
        IEnumerable<EmployeeSwipeInAndOut> GetFirstInLastOutSwipeByEmpId(string empId);

        /// <summary>
        /// Get Employee's work, late and extract work hours in minutes
        /// </summary>
        /// <param name="empId">Employee id</param>
        /// <param name="shiftStartTime">Shift start time. Eg."10:00:00 AM"</param>
        /// <param name="shiftEndTime">Shift end time Eg."06:00:00 PM"</param>
        /// <returns></returns>
        IEnumerable<WorkTimeInfo> GetWorkingTimeInfo(string empId, string shiftStartTime, string shiftEndTime);
    }
}
