using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{
    /// <summary>
    /// It represent employee's work, late and extract hours in minutes
    /// </summary>
    public class WorkTimeInfo
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Total work in minutes
        /// </summary>
        public int TotalWorkInMinutes { get; set; }

        /// <summary>
        /// Late in minutes
        /// </summary>
        public int LateInMinutes { get; set; }
        
        /// <summary>
        /// Extra work in minutes
        /// </summary>
        public int ExtraWorkInMinutes { get; set; }
    }
}
