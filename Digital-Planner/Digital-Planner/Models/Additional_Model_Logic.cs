using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Digital_Planner.Models
{

    /*  These class are more for inheritence logic stuff,
     *    add non-inherit stuff at the bottom of the file
     *  The = * is what sets the default value 
     */
    namespace MetaData
    {
        public partial class User_Metadata
        {
            public string FirstName { get; set; } = "First";
            public string LastName { get; set; } = "Last";
            public string Email { get; set; } = "example@example.com";
            public string Password { get; set; } = "MyPa55word1";
        }

        public partial class Event_Metadata
        {
            public string Title { get; set; } = "Title";
            //[UIHint("DateTimeSelector")]
            public DateTime OccursAt { get; set; } = DateTime.Today;
            //[UIHint("TimeSelector")]
            public TimeSpan Duration { get; set; } = TimeSpan.MinValue;
            //[UIHint("DateTimeSelector")]
            public DateTime CompleteBy { get; set; } = DateTime.Today;
            [UIHint("PriorityButtons")]
            public int Priority { get; set; } = 1;
            [UIHint("CompletionCheck")]
            public bool IsComplete { get; set; } = false;
            [UIHint("AutomaticAssign")]
            public bool AutoAssign { get; set; } = false;

            /* //I'm not sure if this is needed -tc, the values are defaulted above, this is more of just in case things don't work
            public Event_Metadata()
            {
                this.Title = "Title";
                this.OccursAt = DateTime.Now;
                this.Duration = TimeSpan.MinValue;
                this.CompleteBy = DateTime.Now.AddDays(1);
                this.Priority = 1;
                this.IsComplete = false;
                this.AutoAssign = false;
            }
            */
        }

        public partial class Day_Metadata
        {
            //[UIHint("TimeSelector")]
            public TimeSpan HoursAvailable { get; set; } = TimeSpan.MinValue;
            //[UIHint("DateTimeSelector")]
            public DateTime Date { get; set; } = DateTime.Now;

            /* //I'm not sure if this is needed -tc, the values are defaulted above, this is more of just in case things don't work
            public Day_Metadata()
            {
                this.Date = DateTime.Now;
                this.HoursAvailable = TimeSpan.MinValue;
            }
            */
        }

        public partial class Category_Metadata
        {
            public string Description { get; set; } = "Description";
        }
    }


   
    /*  Non-inheritence stuff here  */

            [MetadataType(typeof(MetaData.User_Metadata))]
    public partial class User
    {
    }

    [MetadataType(typeof(MetaData.Event_Metadata))]
    public partial class Event
    {
        /* //I'm not sure if this is needed -tc, the values are defaulted above, this is more of just in case things don't work
        // :base() is like calling super() in Java
        public Event() : base()
        { }
        */
    }

    [MetadataType(typeof(MetaData.Day_Metadata))]
    public partial class Day
    {
        /* //I'm not sure if this is needed -tc, the values are defaulted above, this is more of just in case things don't work
        // :base() is like calling super() in Java
        public Day() : base()
        { }
        */
    }

    [MetadataType(typeof(MetaData.Category_Metadata))]
    public partial class Category
    {
    }
}