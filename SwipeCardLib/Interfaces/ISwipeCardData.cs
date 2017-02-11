using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeCardLib
{
    /// <summary>
    /// Swipe data to handle swipe card data.
    /// </summary>
    public interface ISwipeCardData
    {
        /// <summary>
        /// Get swipe data from various sources
        /// </summary>
        /// <returns>return swipe data</returns>
        IEnumerable<SwipeData> GetSwipeData();
    }
}
