using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Digital_Planner.Models
{
    /*
    public class Additional_Model_Logic
    {
    }
    */
    /*
    <MetadataType(GetType(MyEntity_Metadata))>
Partial Public Class MyEntity
End Class

Class MyEntity_Metadata
    <DisplayName("Key of the entity")>
    Public Property Id As Integer
    <UIHint("MyTextBox")>
    Public Property Name As String
End Class
        */

    /*  These class are more for inheritence logic stuff, add non-inherit stuff at the bottom of the file   */
    public partial class User_Metadata
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public partial class Event_Metadata
    {
        public string Title { get; set; }
        [UIHint("DateTimeSelector")]
        public DateTime OccursAt { get; set; }
        [UIHint("TimeSelector")]
        public TimeSpan Duration { get; set; }
        [UIHint("DateTimeSelector")]
        public DateTime CompleteBy { get; set; }
        [UIHint("PriorityButtons")]
        public int Priority { get; set; }
        [UIHint("CompletionCheck")]
        public bool IsComplete { get; set; }
        [UIHint("AutomaticAssign")]
        public bool AutoAssign { get; set; }
    }

    public partial class Day_Metadata
    {
        [UIHint("TimeSelector")]
        public TimeSpan HoursAvailable { get; set; }
        [UIHint("TimeSelector")]
        public TimeSpan WorkStarts { get; set; }
        [UIHint("DateSelector")]
        public DateTime Date { get; set; }
    }

    public partial class Category_Metadata
    {
        public string Description { get; set; }
    }





    /*  Non-inheritence stuff here  */

    [MetadataType(typeof(User_Metadata))]
    public partial class User
    {
    }

    [MetadataType(typeof(Event_Metadata))]
    public partial class Event
    {
    }

    [MetadataType(typeof(Day_Metadata))]
    public partial class Day
    {
    }

    [MetadataType(typeof(Category_Metadata))]
    public partial class Category
    {
    }
}