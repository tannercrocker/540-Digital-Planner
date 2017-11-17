/*
*   File:           Event.cs
*   Author:         Benjamin Albrecht
*   Date:           11/12/2017
*   Description:    Stores information for Event objects
*/


using System;

namespace Digital_Planner
{
    class PlannerEvent
    {
        private String title;

        private int priority;
        private System.TimeSpan duration;
        private DateTime completeBy;
        private float score;
        private bool autoAssign = true;
        private DateTime occursAt;


        public PlannerEvent(String name, int priority, System.TimeSpan completionHours, DateTime dueDate)
        {
            //  Constructor for auto events

            this.title = name;
            this.priority = priority;
            this.duration = completionHours;
            this.completeBy = dueDate;

            GenerateScore();
        }


        public PlannerEvent(String name, int priority, System.TimeSpan completionHours, DateTime dueDate, DateTime assignedDay)
        {
            //  Constructor for manual events

            this.title = name;
            this.priority = priority;
            this.duration = completionHours;
            this.completeBy = dueDate;
            this.occursAt = assignedDay;
            autoAssign = false;

            GenerateScore();
        }


        private void GenerateScore()
        {
            //  Generates the event's score

            float daysUntilDue = (float)(completeBy - DateTime.Now).TotalDays;

            score = 0;
            score += (int)priority * PlannerSettings.PRIORITY_WEIGHT;
            score += duration.Minutes * PlannerSettings.HOURS_WEIGHT / 60;
            score -= daysUntilDue * PlannerSettings.DUE_DATE_WEIGHT;
        }


        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                GenerateScore();
            }
        }


        public int PriorityLevel
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
                GenerateScore();
            }
        }


        public System.TimeSpan Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
                GenerateScore();
            }
        }


        public DateTime CompleteBy
        {
            get
            {
                return completeBy;
            }
            set
            {
                completeBy = value;
                GenerateScore();
            }
        }


        public float Score
        {
            get
            {
                return score;
            }
        }


        public DateTime OccursAt
        {
            get
            {
                return occursAt;
            }
            set
            {
                occursAt = value;
            }
        }


        public bool AutoAssign
        {
            get
            {
                return autoAssign;
            }
        }


        public void DebugPrint()
        {
            System.Diagnostics.Debug.WriteLine(title + ":    hours: " + duration + "   Score: " + score + "   priority: " + priority + "   Due Date: " + completeBy);
        }

    }
}