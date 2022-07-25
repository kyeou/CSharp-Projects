using System;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Net.Http.Json;
// See https://aka.ms/new-console-template for more information
namespace csun
{
    //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-7-0

    public class Root
    {
        public List<Course> courses { get; set; } = default!;
        public List<Course> classes { get; set; } = default!;
    }
        public class Class
    {
        public string class_number { get; set; }
        public string subject { get; set; }
        public string catalog_number { get; set; }
        public string section_number { get; set; }
        public string title { get; set; }
        public int course_id { get; set; }
        public string description { get; set; }
        public string units { get; set; }
        public string term { get; set; }
        public string class_type { get; set; }
        public int enrollment_cap { get; set; }
        public int enrollment_count { get; set; }
        public int waitlist_cap { get; set; }
        public int waitlist_count { get; set; }
        public List<Meeting> meetings { get; set; }
        public List<Instructor> instructors { get; set; }
    }

    public class Instructor
    {
        public string instructor { get; set; }
    }

    public class Meeting
    {
        public int meeting_number { get; set; }
        public string location { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string days { get; set; }
    }
    public class Course
    {
        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Course other)) return false;
            return catalog_number.Equals(other.catalog_number);
        }
        public string? subject { get; set; }
        public string? catalog_number { get; set; }

        public string? title { get; set; }

        public string? description { get; set; }
        public string? units { get; set; }

    }

    public class csun
    {
        public static async Task ShowCatalog()
        {
            using HttpClient client_fall = new() { BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/courses/comp") };
            using HttpClient client_spring = new() { BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Spring-2022/courses/comp") };

            Root? fall_listing = await client_fall.GetFromJsonAsync<Root?>(new string(""));
            Root? spring_listing = await client_spring.GetFromJsonAsync<Root?>(new string(""));

            List<Course> complete_listing = fall_listing?.courses.Concat(spring_listing?.courses).ToList();

            complete_listing = complete_listing?.Distinct().ToList();
            complete_listing.DistinctBy(x=>x.catalog_number).OrderBy(x => x.catalog_number).ToList().ForEach(x => Console.WriteLine($"{x.catalog_number} - {x.title} - {x.units}"));
        }
    }
}