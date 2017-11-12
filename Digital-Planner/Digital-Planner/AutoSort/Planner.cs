/*
*   File:           Planner.cs
*   Author:         Benjamin Albrecht 
*   Date:           11/12/2017
*   Description:    Planner stores a list of days, auto events, and manual events.
*                   Automatically sorts and assigns events when GenerateSchedule() is called.
*/

using System;
using System.Collections.Generic;

namespace Sorting_Prototype
{
    class Planner{
        private List<PlannerEvent> autoEvents = new List<PlannerEvent>();
        private List<PlannerEvent> manualEvents = new List<PlannerEvent>();
        private List<PlannerDay> days = new List<PlannerDay>();


        public void GenerateSchedule(){
            GetDataFromDatabase();
            SortEvents();
            AssignWorkDays();
            UpdateDatabase();
        }


        private void GetDataFromDatabase() {
            //  Gets the information from the database and populates the lists

        }


        private void UpdateDatabase() {
            //  Updates the database with the current values in the lists.

        }


        private void SortEvents(){
            //  Sorts automatic events based on event score

            List<PlannerEvent> temp = new List<PlannerEvent>();

            //set temp equal to autoEvents
            for (int i = 0; i < autoEvents.Count; i++)
                temp.Add(autoEvents[i]);

            autoEvents.Clear();

            //add events to autoEvents in order of score
            while (temp.Count > 0){
                int highestIndex = 0;
                for (int i = 0; i < temp.Count; i++)
                    if (temp[i].Score > temp[highestIndex].Score) highestIndex = i;
                autoEvents.Add(temp[highestIndex]);
                temp.RemoveAt(highestIndex);
            }
        }


        private void AssignWorkDays(){

            //assign manual events
            for (int i = 0; i < manualEvents.Count; i++)
                for (int j = 0; j < days.Count; j++)
                    if (days[j].Date == manualEvents[i].AssignedDay)
                        days[j].AddManualEvent(manualEvents[i]);

            int dayIndex = 0;
            int eventIndex = 0;
            
            //start at the first day and keep adding events until no events fit
            //then move to next day.  Repeat until all events have been assigned
            while (autoEvents.Count > 0){
                if (days[dayIndex].RemainingWorkHours >= autoEvents[eventIndex].Hours){
                    days[dayIndex].AddAutoEvent(autoEvents[eventIndex]);
                    autoEvents.RemoveAt(eventIndex);
                }else{
                    eventIndex++;
                    if (eventIndex >= autoEvents.Count){
                        dayIndex++;
                        eventIndex = 0;
                    }
                        
                }
            }

            //Set the assignment day for all events
            for (int i = 0; i < days.Count; i++) {
                days[i].AssignDays();
            }
        }


        public void AddAssignment(PlannerEvent newAssignment){
            //  Add a new assignment to the Planner

            if (newAssignment.AutoAssign)
                autoEvents.Add(newAssignment);
            else{
                manualEvents.Add(newAssignment);
            }

        }


        public void AddDay(PlannerDay newDay){
            //  Add a new day to the Planner

            days.Add(newDay);
        }


        public void DebugPrintAssignments(){
            //  Print output to debug Console

            for (int i = 0; i < days.Count; i++){
                days[i].DebugPrintEvents();
            }
        }

    }
}
