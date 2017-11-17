/*
*   File:           PlannerCalenderDate.cs
*   Author:         Benjamin Albrecht
*   Date:           11/07/2017
*   Description:    
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital_Planner
{
    class PlannerDay
    {

        //name should be changed to a reference to the Day in the Database

        private DateTime date;
        private System.TimeSpan minutesAvaialable;
        private float remainingWorkMinutes;

        private List<PlannerEvent> events = new List<PlannerEvent>();

        public PlannerDay(DateTime date, System.TimeSpan totalWorkHours)
        {
            this.minutesAvaialable = totalWorkHours;
            this.date = date;
            remainingWorkMinutes = totalWorkHours.Minutes;
        }


        public bool AddAutoEvent(PlannerEvent newEvent)
        {
            if (remainingWorkMinutes >= newEvent.Duration.Minutes)
            {
                events.Add(newEvent);
                remainingWorkMinutes -= newEvent.Duration.Minutes;
                return true;
            }

            return false;
        }


        public void AssignDays()
        {
            //  Loops through all events in list and sets their assigned day to this day

            for (int i = 0; i < events.Count; i++)
                events[i].OccursAt = date;
        }


        public void AddManualEvent(PlannerEvent newEvent)
        {
            events.Add(newEvent);
            remainingWorkMinutes -= newEvent.Duration.Minutes;
        }


        public float RemainingWorkHours
        {
            get
            {
                return remainingWorkMinutes;
            }
        }


        public DateTime Date
        {
            get
            {
                return date;
            }
        }


        public void DebugPrintEvents()
        {
            System.Diagnostics.Debug.WriteLine(date + ": " + minutesAvaialable + " hours");
            System.Diagnostics.Debug.WriteLine("------------");
            for (int i = 0; i < events.Count; i++)
            {
                events[i].DebugPrint();
            }
            System.Diagnostics.Debug.WriteLine("");
        }


    }
}