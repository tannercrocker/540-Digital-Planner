/*
*   File:           Planner.cs
*   Author:         Benjamin Albrecht
*   Date:           9/30/2017
*   Description:    Contains a class for the Planner object.  Planner stores a list of
*                   Events and Categories.  It also manages the sorting and assignment of events.
*/

using System;
using System.Collections.Generic;

namespace DigitalPlanner
{
    class Planner{
        private List<PlannerEvent> assignments = new List<PlannerEvent>();

        public void AddAssignment(PlannerEvent newAssignment){
            assignments.Add(newAssignment);
        }

        public void GenerateSchedule(){
            SortAssignments();
            AssignWorkDays();
        }

        //Not very efficient - might need to be rewritten
        private void SortAssignments(){
            //create temp and clear assignments list
            List<PlannerEvent> temp = new List<PlannerEvent>();
            for (int i = 0; i < assignments.Count; i++)
                temp.Add(assignments[i]);
            assignments.Clear();

            //add assignments to list in order of score
            while (temp.Count > 0){
                int highestIndex = 0;
                for (int i = 0; i < temp.Count; i++)
                    if (temp[i].Score > temp[highestIndex].Score) highestIndex = i;
                assignments.Add(temp[highestIndex]);
                temp.RemoveAt(highestIndex);
            }
        }

        private void AssignWorkDays(){

        }

        public void DebugPrintAssignments(){
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("Sorting Test: ");

            for (int i = 0; i < assignments.Count; i++)
                System.Diagnostics.Debug.WriteLine(assignments[i].Name);

            System.Diagnostics.Debug.WriteLine("");
        }

    }
}
