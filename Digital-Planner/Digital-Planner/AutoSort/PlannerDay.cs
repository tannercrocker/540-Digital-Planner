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

namespace Sorting_Prototype{
    class PlannerDay{

        //name should be changed to a reference to the Day in the Database

        private DateTime date;
        private float totalWorkHours;
        private float remainingWorkHours;

        private List<PlannerEvent> events = new List<PlannerEvent>();

        public PlannerDay(DateTime date, float totalWorkHours){
            this.totalWorkHours = totalWorkHours;
            this.date = date;
            remainingWorkHours = totalWorkHours;
        }


        public bool AddAutoEvent(PlannerEvent newEvent){
            if (remainingWorkHours >= newEvent.Hours){
                events.Add(newEvent);
                remainingWorkHours -= newEvent.Hours;
                return true;
            }

            return false;
        }


        public void AssignDays() {
            //  Loops through all events in list and sets their assigned day to this day

            for (int i = 0; i < events.Count; i++)
                events[i].AssignedDay = date;
        }


        public void AddManualEvent(PlannerEvent newEvent){
            events.Add(newEvent);
            remainingWorkHours -= newEvent.Hours;
        }


        public float RemainingWorkHours{
            get {
                return remainingWorkHours;
            }
        }


        public DateTime Date{
            get {
                return date;
            }
        }


        public void DebugPrintEvents(){
            System.Diagnostics.Debug.WriteLine(date + ": " + totalWorkHours + " hours");
            System.Diagnostics.Debug.WriteLine("------------");
            for (int i = 0; i < events.Count; i++){
                events[i].DebugPrint();
            }
            System.Diagnostics.Debug.WriteLine("");
        }


    }
}
