using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{
    /// <summary>
    /// Swipe In and out flags
    /// </summary>
    public enum SwipeMode
    {
        /// <summary>
        /// Swipe In flag
        /// </summary>
        IN,

        /// <summary>
        /// Swipe out flag
        /// </summary>
        OUT
    }


    /// <summary>
    /// This class represent swipe data which is from swipe device 
    /// </summary>
    public class SwipeData
    {
        /// <summary>
        /// Employee id
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// Swipe date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Swipe In and Out flag
        /// </summary>
        public SwipeMode SwipeMode { get; set; }

        /// <summary>
        /// Swipe device id
        /// </summary>
        public string SwipeDeviceId { get; set; }

    }
}
