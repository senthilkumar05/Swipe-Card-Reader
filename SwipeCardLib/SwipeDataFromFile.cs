using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{
    /// <summary>
    /// Read swipe data from text file
    /// </summary>
    public class SwipeDataFromFile : ISwipeCardData
    {
        string swipeDataFile = string.Empty;

        /// <summary>
        /// Constructor of SwipeDataFromFile
        /// </summary>
        /// <param name="swipeDataFile">Pass swipe data file</param>
        public SwipeDataFromFile(string swipeDataFile)
        {
            this.swipeDataFile = swipeDataFile;
        }

        /// <summary>
        /// Get swipe data from file
        /// </summary>
        /// <returns>return collection of swipe data</returns>
        public IEnumerable<SwipeData> GetSwipeData()
        {
            List<SwipeData> sdata = new List<SwipeData>();

            if (String.IsNullOrEmpty(swipeDataFile) ||
                !File.Exists(swipeDataFile))
                return sdata;

            string[] data = File.ReadAllLines(swipeDataFile);
            foreach (var swipe in data)
                sdata.Add(GetSwipeData(swipe));

            return sdata;
        }

        /// <summary>
        /// Get swipe data from string
        /// </summary>
        /// <param name="swipeData"></param>
        /// <returns></returns>
        protected virtual SwipeData GetSwipeData(string swipeData)
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

    }
}
