using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{
    /// <summary>
    /// Employee's swipe in and out data.
    /// </summary>
    public class EmployeeSwipeInAndOut
    {
        /// <summary>
        /// Employee id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Swipe in
        /// </summary>
        public DateTime? SwipeIn { get; set; }

        /// <summary>
        /// Swipe out
        /// </summary>
        public DateTime? SwipeOut { get; set; }

    }
}
