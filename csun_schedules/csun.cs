using System;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Net.Http.Json;

/* 
How to run: Download the whole csun_schedules folder
Open CL in folder and run: dotnet run Term Year SubjectCode
Example: dotnet run Fall 2022 comp
*/


namespace csun
{
    //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-7-0

    public class Root
    {
        public List<Course> courses { get; set; } = default!;
        public List<Class> classes { get; set; } = default!;
        public void filter(String[] args)
        {
            if (args.Length >= 5)
            {
                for (int i = 0; i < (args.Length - 5) / 2; i++)
                {
                    Console.WriteLine("filter function called");
                }
            }

        }
    }
    public class Class
    {
        public string class_number { get; set; } = default!;
        public string subject { get; set; } = default!;
        public string catalog_number { get; set; } = default!;
        public string section_number { get; set; } = default!;
        public string title { get; set; } = default!;
        public int course_id { get; set; } = default!;
        public string description { get; set; } = default!;
        public string units { get; set; } = default!;

        public int enrollment_cap { get; set; } = default!;
        public int enrollment_count { get; set; } = default!;
        public int waitlist_cap { get; set; } = default!;
        public int waitlist_count { get; set; } = default!;
        public List<Meeting> meetings { get; set; } = default!;
        public List<Instructor> instructors { get; set; } = default!;
    }

    public class Instructor
    {
        public string? instructor { get; set; } = default!;
    }

    public class Meeting
    {
        public int meeting_number { get; set; } = default!;
        public string location { get; set; } = default!;
        public string start_time { get; set; } = default!;
        public string end_time { get; set; } = default!;
        public string days { get; set; } = default!;
    }
    public class Course
    {
        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Course other)) return false;
            return catalog_number.Equals(other.catalog_number);
        }
        public string subject { get; set; } = default!;
        public string catalog_number { get; set; } = default!;
        public string title { get; set; } = default!;
        public string description { get; set; } = default!;
        public string units { get; set; } = default!;

    }

    public class csun
    {
        public static async Task ShowCatalog()
        {
            using HttpClient client_fall = new() { BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/courses/comp") } ;
            using HttpClient client_spring = new() { BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Spring-2022/courses/comp") };

            Root? fall_listing = await client_fall.GetFromJsonAsync<Root>(new string(""));
            Root? spring_listing = await client_spring.GetFromJsonAsync<Root>(new string(""));

            List<Course>? complete_listing = fall_listing.courses.Concat(spring_listing.courses).ToList();

            complete_listing = complete_listing.Distinct().ToList();
            complete_listing.DistinctBy(x => x.catalog_number).OrderBy(x => x.catalog_number).ToList().ForEach(x => Console.WriteLine($"{x.catalog_number} - {x.title} - {x.units}"));
        }


        public static async Task ShowSchedule(String[] args)
        {
            using HttpClient client = new() { BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/" + args[0] + "-" + args[1] + "/classes/" + args[2]) };
            Root? listing = await client.GetFromJsonAsync<Root>(new string(""));

            listing?.filter(args);

            //listing.classes.OrderBy(x => x.catalog_number).ToList().ForEach(x => Console.WriteLine($"{x.class_number} - {x.catalog_number} - {x.title} - {x.units}"));

            string current_class = "";

            foreach (Class c in listing.classes.Where(x => x.meetings.Any()))
            {
                if (!(current_class.Equals(c.catalog_number)))
                {
                    current_class = c.catalog_number;
                    Console.WriteLine("\n\n--------------\n\n" + c.subject + " " + c.catalog_number + " " + c.title);
                    Console.WriteLine("\n\tSection\t\tLocation\tDays\t\tSeats\t\t\tTime\t\t\t\tFaculty");
                    Console.WriteLine("\t-------\t\t--------\t----\t\t-----\t\t\t----\t\t\t\t-------");
                }


                List<string> section_string = new List<string>();

                section_string.Add($"\t {c.class_number,-13} {c.meetings[0].location,8} {c.meetings[0].days,11} {c.enrollment_cap - c.enrollment_count, 15} ");
                section_string.Add("\t\t    " +
                                    (c.meetings[0].start_time).Substring(0, 2) + ":" +
                                    (c.meetings[0].start_time).Substring(2, 2)
                                    + " - " +
                                    (c.meetings[0].end_time).Substring(0, 2) + ":" +
                                    (c.meetings[0].end_time).Substring(2, 2));
                if (c.instructors.Any()) { section_string.Add("\t\t" + c.instructors[0].instructor + "\n"); }
                else                     { section_string.Add("\t\t\t" + "Staff\n"); }


                foreach (string s in section_string)  { Console.Write(s); }

            }
        }

        public static async Task Main(String[] args)
        {
            await ShowSchedule(args);
        }
    }
}