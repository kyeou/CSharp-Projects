using System;
using System.Net;
using System.IO;
using System.Text.Json;
// See https://aka.ms/new-console-template for more information
namespace csun
{

    public class course {
        public 
    }
HttpClient client = new HttpClient();

//string myJSON = client.DownloadString("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/classes/math");
var stringTask = client.GetStringAsync("https://api.metalab.csun.edu/curriculum/api/2.0/terms/Fall-2022/classes/math");



//Console.WriteLine(myClass);


//JObject? json_blob = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(await stringTask) as JObject;
//JObject json_blob = JObject.Parse(await stringTask);

Console.WriteLine(JsonSerializer.Deserialize<string>(await stringTask)!);

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



Console.ReadLine();}