using System;
using System.Net;
using System.IO;
using System.Text.Json;
// See https://aka.ms/new-console-template for more information
namespace csun
{
//https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-7-0
 
public class Rootobject
{
    public string api { get; set; }
    public string status { get; set; }
    public string success { get; set; }
    public string version { get; set; }
    public string collection { get; set; }
    public Class1[] classes { get; set; }
}

public class Class1
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
    public Meeting[] meetings { get; set; }
    public Instructor[] instructors { get; set; }
}

public class Meeting
{
    public int meeting_number { get; set; }
    public string location { get; set; }
    public string start_time { get; set; }
    public string end_time { get; set; }
    public string days { get; set; }
}

public class Instructor
{
    public string instructor { get; set; }
}

    public class csun
    {
        public static async Task Main()
        {
            HttpClient client = new HttpClient(Bas);

            //string myJSON = client.DownloadString("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/classes/math");
            //var stringTask = client.GetStringAsync("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/classes/math");



            //Console.WriteLine(myClass);


            //JObject? json_blob = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(await stringTask) as JObject;
            //JObject json_blob = JObject.Parse(await stringTask);

           // Classes? classes = await JsonSerializer.DeserializeAsync<Classes>(await stringTask);

            Rootobject? rtob = await JsonSerializer.DeserializeAsync<Rootobject>(await stringTask);
           // Console.WriteLine(await stringTask);
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