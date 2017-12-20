using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K9CleanSweep.Models
{
    public class Schedule
    {
        /*==========     Monday     ==========*/
        public bool Booked00 { get; set; }    /* 8am */
        public bool Booked01 { get; set; }    /* 9am */
        public bool Booked02 { get; set; }    /* 10am */
        public bool Booked03 { get; set; }    /* 11am */
        public bool Booked04 { get; set; }    /* noon */
        public bool Booked05 { get; set; }    /* 1pm */
        public bool Booked06 { get; set; }    /* 2pm */
        public bool Booked07 { get; set; }    /* 3pm */
        public bool Booked08 { get; set; }    /* 4pm */
        public bool Booked09 { get; set; }    /* 5pm */
        public bool Booked010 { get; set; }   /* 6pm */
        public bool Booked011 { get; set; }   /* 7pm */
        public bool Booked012 { get; set; }   /* 8pm */

        /*==========     Teusday     ==========*/
        public bool Booked10 { get; set; }    /* 8am */
        public bool Booked11 { get; set; }    /* 9am */
        public bool Booked12 { get; set; }    /* 10am */
        public bool Booked13 { get; set; }    /* 11am */
        public bool Booked14 { get; set; }    /* noon */
        public bool Booked15 { get; set; }    /* 1pm */
        public bool Booked16 { get; set; }    /* 2pm */
        public bool Booked17 { get; set; }    /* 3pm */
        public bool Booked18 { get; set; }    /* 4pm */
        public bool Booked19 { get; set; }    /* 5pm */
        public bool Booked110 { get; set; }   /* 6pm */
        public bool Booked111 { get; set; }   /* 7pm */
        public bool Booked112 { get; set; }   /* 8pm */

        /*==========     Wednesday     ==========*/
        public bool Booked20 { get; set; }    /* 8am */
        public bool Booked21 { get; set; }    /* 9am */
        public bool Booked22 { get; set; }    /* 10am */
        public bool Booked23 { get; set; }    /* 11am */
        public bool Booked24 { get; set; }    /* noon */
        public bool Booked25 { get; set; }    /* 1pm */
        public bool Booked26 { get; set; }    /* 2pm */
        public bool Booked27 { get; set; }    /* 3pm */
        public bool Booked28 { get; set; }    /* 4pm */
        public bool Booked29 { get; set; }    /* 5pm */
        public bool Booked210 { get; set; }   /* 6pm */
        public bool Booked211 { get; set; }   /* 7pm */
        public bool Booked212 { get; set; }   /* 8pm */

        /*==========     Thursday     ==========*/
        public bool Booked30 { get; set; }    /* 8am */
        public bool Booked31 { get; set; }    /* 9am */
        public bool Booked32 { get; set; }    /* 10am */
        public bool Booked33 { get; set; }    /* 11am */
        public bool Booked34 { get; set; }    /* noon */
        public bool Booked35 { get; set; }    /* 1pm */
        public bool Booked36 { get; set; }    /* 2pm */
        public bool Booked37 { get; set; }    /* 3pm */
        public bool Booked38 { get; set; }    /* 4pm */
        public bool Booked39 { get; set; }    /* 5pm */
        public bool Booked310 { get; set; }   /* 6pm */
        public bool Booked311 { get; set; }   /* 7pm */
        public bool Booked312 { get; set; }   /* 8pm */

        /*==========     Friday     ==========*/
        public bool Booked40 { get; set; }    /* 8am */
        public bool Booked41 { get; set; }    /* 9am */
        public bool Booked42 { get; set; }    /* 10am */
        public bool Booked43 { get; set; }    /* 11am */
        public bool Booked44 { get; set; }    /* noon */
        public bool Booked45 { get; set; }    /* 1pm */
        public bool Booked46 { get; set; }    /* 2pm */
        public bool Booked47 { get; set; }    /* 3pm */
        public bool Booked48 { get; set; }    /* 4pm */
        public bool Booked49 { get; set; }    /* 5pm */
        public bool Booked410 { get; set; }   /* 6pm */
        public bool Booked411 { get; set; }   /* 7pm */
        public bool Booked412 { get; set; }   /* 8pm */

        /*==========     Saturday     ==========*/
        public bool Booked50 { get; set; }    /* 8am */
        public bool Booked51 { get; set; }    /* 9am */
        public bool Booked52 { get; set; }    /* 10am */
        public bool Booked53 { get; set; }    /* 11am */
        public bool Booked54 { get; set; }    /* noon */
        public bool Booked55 { get; set; }    /* 1pm */
        public bool Booked56 { get; set; }    /* 2pm */
        public bool Booked57 { get; set; }    /* 3pm */
        public bool Booked58 { get; set; }    /* 4pm */
        public bool Booked59 { get; set; }    /* 5pm */
        public bool Booked510 { get; set; }   /* 6pm */
        public bool Booked511 { get; set; }   /* 7pm */
        public bool Booked512 { get; set; }   /* 8pm */

        /*==========     Sunday     ==========*/
        public bool Booked60 { get; set; }    /* 8am */
        public bool Booked61 { get; set; }    /* 9am */
        public bool Booked62 { get; set; }    /* 10am */
        public bool Booked63 { get; set; }    /* 11am */
        public bool Booked64 { get; set; }    /* noon */
        public bool Booked65 { get; set; }    /* 1pm */
        public bool Booked66 { get; set; }    /* 2pm */
        public bool Booked67 { get; set; }    /* 3pm */
        public bool Booked68 { get; set; }    /* 4pm */
        public bool Booked69 { get; set; }    /* 5pm */
        public bool Booked610 { get; set; }   /* 6pm */
        public bool Booked611 { get; set; }   /* 7pm */
        public bool Booked612 { get; set; }   /* 8pm */
    }
}
