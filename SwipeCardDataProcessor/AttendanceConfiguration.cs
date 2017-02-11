using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace SwipeCardDataProcessor
{

    

    public class AttendanceSection : ConfigurationSection
    {
        // Create a "font" element.
        [ConfigurationProperty("totalHourCalculation")]
        public TotalHourCalculationElement TotalHourCalculation
        {
            get
            {
                return (TotalHourCalculationElement)this["totalHourCalculation"];
            }
            set
            { this["totalHourCalculation"] = value; }
        }

        [ConfigurationProperty("breakTime")]
        public BreakTimeElement BreakTime
        {
            get
            {
                return (BreakTimeElement)this["breakTime"];
            }
            set
            { this["breakTime"] = value; }
        }

        [ConfigurationProperty("attendanceMode")]
        public AttendanceModeElement AttendanceMode
        {
            get
            {
                return (AttendanceModeElement)this["attendanceMode"];
            }
            set
            { this["attendanceMode"] = value; }
        }

    }

    public class TotalHourCalculationElement : ConfigurationElement
    {
        [ConfigurationProperty("mode", DefaultValue = TotalHourCalculationMode.FirstCheckInAndLastCheckOut, IsRequired = true)]
        public TotalHourCalculationMode Mode
        {
            get
            {
                return (TotalHourCalculationMode)this["mode"];
            }
            set
            {
                this["mode"] = value;
            }
        }
    }


    public class BreakTimeElement : ConfigurationElement
    {
        [ConfigurationProperty("hours", DefaultValue = "00.00", IsRequired = true)]
        public string Hour
        {
            get
            {
                return (string)this["hours"];
            }
            set
            {
                this["hours"] = value;
            }
        }
    }

    public class AttendanceModeElement : ConfigurationElement
    {
        [ConfigurationProperty("use", DefaultValue = AttendanceMode.Lenient, IsRequired = true)]
        public AttendanceMode Use
        {
            get
            {
                return (AttendanceMode)this["use"];
            }
            set
            {
                this["use"] = value;
            }
        }

        [ConfigurationProperty("strict")]
        public StrictElement Strick
        {
            get
            {
                return (StrictElement)this["strict"];
            }
            set
            { this["strict"] = value; }
        }

        [ConfigurationProperty("lenient")]
        public LenientElement Lenient
        {
            get
            {
                return (LenientElement)this["lenient"];
            }
            set
            { this["lenient"] = value; }
        }
    }


    public class StrictElement : ConfigurationElement
    {
        [ConfigurationProperty("fullday", DefaultValue = "00.00", IsRequired = true)]
        public string Fullday
        {
            get
            {
                return (string)this["fullday"];
            }
            set
            {
                this["fullday"] = value;
            }
        }

        [ConfigurationProperty("halfday", DefaultValue = "00.00", IsRequired = true)]
        public string halfday
        {
            get
            {
                return (string)this["halfday"];
            }
            set
            {
                this["halfday"] = value;
            }
        }

    }

    public class LenientElement : ConfigurationElement
    {
        [ConfigurationProperty("perday", DefaultValue = "00.00", IsRequired = true)]
        public string Perday
        {
            get
            {
                return (string)this["perday"];
            }
            set
            {
                this["perday"] = value;
            }
        }
    }

    public enum TotalHourCalculationMode
    {
        //FirstCheckIn & Last CheckOut,Every Valid Check-in & Check-out
        FirstCheckInAndLastCheckOut,
        EveryValidCheckInAndCheckOut
    }
    public enum AttendanceMode
    {
        Lenient,
        Strict
        
    }

}