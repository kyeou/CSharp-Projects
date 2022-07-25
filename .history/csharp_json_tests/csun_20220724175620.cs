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
    public List<Class1> classes { get; set; } = default!;
}

public class Class1
{
    
    public string? subject { get; set; }
    public string? catalog_number { get; set; }
   
    public string? title { get; set; }
    
    //public string? description { get; set; }
    public string? units { get; set; }
  
    public string? class_type { get; set; }
    
    
}



    public class csun
    {
        public static async Task Main()
        {
            using HttpClient client_fall = new (){BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/classes/comp")};
            using HttpClient client_spring = new (){BaseAddress = new Uri("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Spring-2022/classes/comp")};
           



            //Console.WriteLine(myClass);


            //JObject? json_blob = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(await stringTask) as JObject;
            //JObject json_blob = JObject.Parse(await stringTask);

           // Classes? classes = await JsonSerializer.DeserializeAsync<Classes>(await stringTask);

            Rootobject? fall_listing = await client_fall.GetFromJsonAsync<Rootobject?>(new string(""));
            Rootobject? spring_listing = await client_spring.GetFromJsonAsync<Rootobject?>(new string(""));
            // for (int i = 0; i < (rtob?.classes?).Count; i++) {
            // Console.WriteLine(rtob?.classes[i]?.catalog_number); }


            List<Class1> complete_listing = fall_listing?.classes.Concat(spring_listing?.classes).ToList();





//              foreach(var course in complete_listing) {
//                   Console.WriteLine(course);
//              }

//         complete_listing = complete_listing.Distinct().ToList();
// Console.WriteLine("----------------------------------------------------------------");
// foreach(var course in complete_listing) {
//                   Console.WriteLine(course);
//              }

 List<>complete_listing2 = complete_listing.Distinct().ToList();
complete_listing.ToList().OrderBy(x=>x.catalog_number).ToList().ForEach(x=> Console.WriteLine($"{x.catalog_number} - {x.title} - {x.units} - {x.class_type}"));


        }
    }

    /*
    List<string> current_class = new List<string>();
    foreach (JObject classes in json_blob["classes"])
    {
        classes.Property("class_number").Remove();
        classes.Property("term").Remove();
        classes.Property("section_number").Remove();
        classes.Property("course_id").Remove();
        classes.Property("enrollment_cap").Remove();
        classes.Property("enrollment_count").Remove();
        classes.Property("waitlist_cap").Remove();
        classes.Property("waitlist_count").Remove();
        classes.Property("meetings").Remove();
        classes.Property("instructors").Remove();


        if (!current_class.Contains((string)classes["catalog_number"])) {
            current_class.Add((string)classes["catalog_number"]);
            Console.WriteLine(classes["subject"] + " " + classes["catalog_number"] + " " + classes["title"] + " " + classes["units"] + " Units\n" + classes["description"]);
        }

    }
    */


}