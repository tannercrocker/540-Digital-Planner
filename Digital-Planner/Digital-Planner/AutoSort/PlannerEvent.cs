/*
*   File:           Event.cs
*   Author:         Benjamin Albrecht
*   Date:           11/12/2017
*   Description:    Stores information for Event objects
*/


using System;

namespace Sorting_Prototype{

    class PlannerEvent {
        private String name;

        private int priority;
        private float completionHours;
        private DateTime dueDate;
        private float score;
        private bool autoAssign = true;
        private DateTime assignedDay;


        public PlannerEvent(String name, int priority, float completionHours, DateTime dueDate) {
            //  Constructor for auto events

            this.name = name;
            this.priority = priority;
            this.completionHours = completionHours;
            this.dueDate = dueDate;

            GenerateScore();
        }

        
        public PlannerEvent(String name, int priority, float completionHours, DateTime dueDate, DateTime assignedDay){
            //  Constructor for manual events

            this.name = name;
            this.priority = priority;
            this.completionHours = completionHours;
            this.dueDate = dueDate;
            this.assignedDay = assignedDay;
            autoAssign = false;

            GenerateScore();
        }


        private void GenerateScore() {
            //  Generates the event's score

            float daysUntilDue = (float)(dueDate - DateTime.Now).TotalDays;

            score = 0;
            score += (int)priority * PlannerSettings.PRIORITY_WEIGHT;
            score += completionHours * PlannerSettings.HOURS_WEIGHT;
            score -= daysUntilDue * PlannerSettings.DUE_DATE_WEIGHT;
        }


        public String Name {
            get {
                return name;
            }
            set {
                name = value;
                GenerateScore();
            }
        }


        public int PriorityLevel {
            get {
                return priority;
            }
            set {
                priority = value;
                GenerateScore();
            }
        }


        public float Hours {
            get {
                return completionHours;
            }
            set {
                completionHours = value;
                GenerateScore();
            }
        }


        public DateTime DueDate {
            get {
                return dueDate;
            }
            set {
                dueDate = value;
                GenerateScore();
            }
        }


        public float Score {
            get {
                return score;
            }
        }


        public DateTime AssignedDay {
            get {
                return assignedDay;
            }
            set {
                assignedDay = value;
            }
        }


        public bool AutoAssign {
            get {
                return autoAssign;
            }
        }


        public void DebugPrint(){
            System.Diagnostics.Debug.WriteLine(name + ":    hours: " + completionHours + "   Score: " + score + "   priority: " + priority + "   Due Date: " + dueDate);
        }

    }
}
