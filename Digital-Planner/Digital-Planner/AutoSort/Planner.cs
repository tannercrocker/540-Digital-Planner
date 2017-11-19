/*
*   File:           Planner.cs
*   Author:         Benjamin Albrecht 
*   Date:           11/16/2017
*   Description:    Planner stores a list of days, auto events, and manual events.
*                   Automatically sorts and assigns events when GenerateSchedule() is called.
*/


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Digital_Planner
{
    static class Planner
    {
        private static Digital_Planner.Models.calendarEntities db = new Digital_Planner.Models.calendarEntities();

        //TODO:  Pass in user id
        public static void GenerateSchedule()
        {
            List<PlannerEvent> autoEvents = new List<PlannerEvent>();
            List<PlannerEvent> manualEvents = new List<PlannerEvent>();
            List<PlannerDay> days = new List<PlannerDay>();

            GetDataFromDatabase(autoEvents, manualEvents, days);
            SortEvents(autoEvents, manualEvents, days);
            AssignWorkDays(autoEvents, manualEvents, days);
            DebugPrint(autoEvents, manualEvents, days);
            //SaveDataToDatabase();
        }

        private static void DebugPrint(List<PlannerEvent> autoEvents, List<PlannerEvent> manualEvents, List<PlannerDay> days)
        {
            System.Diagnostics.Debug.Print("");
            System.Diagnostics.Debug.Print("Generate Schedule");
            System.Diagnostics.Debug.Print("Days List: " + days.Count);
            System.Diagnostics.Debug.Print("Auto List: " + autoEvents.Count);
            System.Diagnostics.Debug.Print("Manual List: " + manualEvents.Count);

            for (int i = 0; i < days.Count; i++)
                days[i].DebugPrintEvents();

            System.Diagnostics.Debug.Print("");
        }

             
        private static void GetDataFromDatabase(List<PlannerEvent> autoEvents, List<PlannerEvent> manualEvents, List<PlannerDay> days)
        {
            //  Gets the information from the database and populates the lists

            //Get database records
            List<Models.Event> plannerEvents = db.Events.ToList();
            List<Models.Day> plannerDays = db.Days.ToList();

            //sort events by auto/manual assign
            for (int i = 0; i < plannerEvents.Count; i++)
            {
                if (plannerEvents[i].AutoAssign)
                    autoEvents.Add(new PlannerEvent(plannerEvents[i].Title, plannerEvents[i].Priority, plannerEvents[i].Duration, plannerEvents[i].CompleteBy));
                else
                    manualEvents.Add(new PlannerEvent(plannerEvents[i].Title, plannerEvents[i].Priority, plannerEvents[i].Duration, plannerEvents[i].CompleteBy, plannerEvents[i].OccursAt));
            }

            for (int i = 0; i < plannerDays.Count; i++)
                days.Add(new PlannerDay(plannerDays[i].Date, plannerDays[i].HoursAvailable));
        }


        private static void SaveDataToDatabase() {


            db.SaveChanges();
        }


        private static void SortEvents(List<PlannerEvent> autoEvents, List<PlannerEvent> manualEvents, List<PlannerDay> days)
        {
            //  Sorts automatic events based on event score

            List<PlannerEvent> temp = new List<PlannerEvent>();

            //set temp equal to autoEvents
            for (int i = 0; i < autoEvents.Count; i++)
                temp.Add(autoEvents[i]);

            autoEvents.Clear();

            //add events to autoEvents in order of score
            while (temp.Count > 0)
            {
                int highestIndex = 0;
                for (int i = 0; i < temp.Count; i++)
                    if (temp[i].Score > temp[highestIndex].Score) highestIndex = i;
                autoEvents.Add(temp[highestIndex]);
                temp.RemoveAt(highestIndex);
            }
        }


        private static void AssignWorkDays(List<PlannerEvent> autoEvents, List<PlannerEvent> manualEvents, List<PlannerDay> days)
        {

            //assign manual events
            for (int i = 0; i < manualEvents.Count; i++)
                for (int j = 0; j < days.Count; j++)
                    if (days[j].Date == manualEvents[i].OccursAt)
                        days[j].AddManualEvent(manualEvents[i]);

            int dayIndex = 0;
            int eventIndex = 0;

            //start at the first day and keep adding events until no events fit
            //then move to next day.  Repeat until all events have been assigned
            while (autoEvents.Count > 0)
            {
                if (days.Count > 0 && days[dayIndex].RemainingWorkHours >= autoEvents[eventIndex].Duration.Minutes)
                {
                    days[dayIndex].AddAutoEvent(autoEvents[eventIndex]);
                    autoEvents.RemoveAt(eventIndex);
                }
                else
                {
                    eventIndex++;
                    if (eventIndex >= autoEvents.Count)
                    {
                        dayIndex++;
                        eventIndex = 0;
                    }

                }
            }

            //Set the assignment day for all events
            for (int i = 0; i < days.Count; i++)
            {
                days[i].AssignDays();
            }
        }

    }
}