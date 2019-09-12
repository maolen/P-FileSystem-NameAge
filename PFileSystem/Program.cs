using System;
using System.IO;

namespace PFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = string.Empty;

            Console.WriteLine("Выбрать диск по номеру:");
            var drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                if (drives[i].IsReady && drives[i].DriveType == DriveType.Fixed)
                {
                    var counter = i + 1;
                    Console.WriteLine($"{counter}. {drives[i].Name} - {drives[i].AvailableFreeSpace} байт.");
                }
            }

            var driveNumberAsString = Console.ReadLine();
            if (int.TryParse(driveNumberAsString, out var driveUserPosition))
            {
                var driveIndex = driveUserPosition - 1;
                path = drives[driveIndex].Name;

                Console.WriteLine("\n\nВсе директории:");

                foreach (var directoryName in Directory.GetDirectories(path))
                {
                    Console.WriteLine(directoryName);
                }

                Console.WriteLine("Введите имя новой папки:");
                var userDirectoryName = Console.ReadLine();

                path += userDirectoryName;
                Directory.CreateDirectory(path);

                Console.WriteLine("Введите имя нового файла:");
                var userFileName = Console.ReadLine();
                path += $@"\{userFileName}";

                Console.Write("Введите ваше имя: ");
                var firstName = Console.ReadLine();
                Console.Write("Введите вашу фамилию: ");
                var lastName = Console.ReadLine();
                Console.Write("Введите ваш возраст в годах: ");
                var age = Console.ReadLine();

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                
                using (var stream = new StreamWriter(path))
                {
                    stream.WriteLine($"Имя: {user.FirstName}");
                    stream.WriteLine($"Фамилия: {user.LastName}");
                    stream.WriteLine($"Возраст: {user.Age} лет");
                }

                using (var stream = new StreamReader(path))
                {
                    var text = stream.ReadToEnd();
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода!");
            }
            Console.ReadKey();
        }
    }
}
