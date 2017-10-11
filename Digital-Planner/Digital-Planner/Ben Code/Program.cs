using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace DigitalPlanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TestSorting();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }

        //This method performs a simple test of the current sorting system.
        //Prints to the Debug Window (you will have to scroll through to find it - it should be near the top)
        private static void TestSorting()
        {
            Planner planner = new Planner();

            planner.AddAssignment(new PlannerEvent("Test 1", PlannerPriority.HIGH, 1, new DateTime(2017, 9, 2)));
            planner.AddAssignment(new PlannerEvent("Test 2", PlannerPriority.LOW, 1, new DateTime(2017, 9, 20)));
            planner.AddAssignment(new PlannerEvent("Test 3", PlannerPriority.HIGH, 3, new DateTime(2017, 9, 20)));
            planner.AddAssignment(new PlannerEvent("Test 4", PlannerPriority.MEDIUM, 1, new DateTime(2017, 9, 20)));
            planner.AddAssignment(new PlannerEvent("Test 5", PlannerPriority.HIGH, 2, new DateTime(2017, 9, 20)));
            planner.AddAssignment(new PlannerEvent("Test 6", PlannerPriority.MEDIUM, 1, new DateTime(2017, 10, 30)));

            planner.GenerateSchedule();
            planner.DebugPrintAssignments();
        }
    }
}
