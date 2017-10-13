/*
*   File:           Event.cs
*   Author:         Benjamin Albrecht
*   Date:           9/30/2017
*   Description:    Contains a class for PlannerEvent objects.
*/


using System;

namespace DigitalPlanner
{

    class PlannerEvent {

        private String name;
        private PlannerPriority priority;
        private float completionHours;
        private DateTime dueDate;
        private float score;

        public PlannerEvent(String name, PlannerPriority priority, float completionHours, DateTime dueDate) {
            this.name = name;
            this.priority = priority;
            this.completionHours = completionHours;
            this.dueDate = dueDate;

            GenerateScore();
        }

        private void GenerateScore() {
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

        public PlannerPriority PriorityLevel {
            get {
                return priority;
            }
            set {
                priority = value;
                GenerateScore();
            }
        }

        public int Hours {
            get {
                return Hours;
            }
            set {
                Hours = value;
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

    }
}
