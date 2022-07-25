﻿using System;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Net.Http.Json;
// See https://aka.ms/new-console-template for more information
namespace csun
{
    //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-7-0

    public class Rootobject
    {
        public List<Course> courses { get; set; } = default!;
    }

    public class Course
    {
        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Course other)) return false;
            return .Equals(other.units);
        }
        public string? subject { get; set; }
        public string? catalog_number { get; set; }

        public string? title { get; set; }

        public string? description { get; set; }
        public string? units { get; set; }

    }

    public class csun
    {
        public static async Task Main()
        {
            using HttpClient client_fall = new() { BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/courses/comp") };
            using HttpClient client_spring = new() { BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Spring-2022/courses/comp") };

            Rootobject? fall_listing = await client_fall.GetFromJsonAsync<Rootobject?>(new string(""));
            Rootobject? spring_listing = await client_spring.GetFromJsonAsync<Rootobject?>(new string(""));

            List<Course> complete_listing = fall_listing?.courses.Concat(spring_listing?.courses).Distinct().ToList();

            complete_listing = complete_listing?.Distinct().ToList();
            complete_listing.ToList().OrderBy(x => x.catalog_number).ToList().ForEach(x => Console.WriteLine($"{x.catalog_number} - {x.title} - {x.units}"));
        }
    }
}