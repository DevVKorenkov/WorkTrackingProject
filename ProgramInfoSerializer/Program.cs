using System;
using System.IO;
using Newtonsoft.Json;
using WorkTrackingLib.Models;


namespace ProgramInfoSerializer
{
    class Program
    { 
        static void Main(string[] args)
        {
            var programInfo = new ProgramInfo();

            Console.WriteLine("Введите название программы");
            programInfo.ProgramName = Console.ReadLine();
            Console.WriteLine(@"Введите номер сборки (без ""."")");
            programInfo.Version = Convert.ToInt32(Console.ReadLine());
            programInfo.VersionReleaseDate = DateTime.Now;

            var tempString = JsonConvert.SerializeObject(programInfo);

            Console.WriteLine(@"Введите путь сохранения");
            File.WriteAllText($@"{Console.ReadLine()}\version.json", tempString);

            Console.ReadKey();
        }
    }
}
