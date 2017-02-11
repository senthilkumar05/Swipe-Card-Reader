using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{
    /// <summary>
    /// Read swipe data from swipe machine
    /// </summary>
    public class SwipeDataFromDevice : ISwipeCardData
    {
        public SwipeDataFromDevice(object device)
        {
            //Read swipe card data from device.
        }

        /// <summary>
        /// Get  swipe data from device
        /// </summary>
        /// <returns>returns collection of swipe data</returns>
        public IEnumerable<SwipeData> GetSwipeData()
        {
            List<SwipeData> sdata = new List<SwipeData>();

            //Implement your code to read data from device.

            return sdata;

        }
    }
}
