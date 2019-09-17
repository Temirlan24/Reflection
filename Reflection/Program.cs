using System;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;


namespace Reflection
{

    class Program
    {
        static bool isFileNameValid(string fileName)
        {
            if ((fileName == null) || (fileName.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                return false;
            try
            {
                var tempFileInfo = new FileInfo(fileName);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }
        static void Main(string[] args)
        {
            //C:\Users\ЕрзатулыТ\source\repos\DelegateLesson\DelegateLesson\bin\Debug\netcoreapp2.1\DelegateLesson.dll
            Console.WriteLine("Введите путь до файла: ");

            string path = Console.ReadLine();
            if (isFileNameValid(path)) { 
            var assembly = Assembly.LoadFile(path);

                Console.WriteLine(assembly.FullName);
                var types = assembly.GetTypes();
                using (var stream = new StreamWriter($@"C:\Новая папка\1.txt"))
                {
                    foreach (var type in types)
                    {
                        Console.WriteLine("***************************");
                        stream.WriteLine("***************************");
                        var typeIn = type.IsClass ? "класс" : "другой тип";
                        string text = $"{type.Name} - {typeIn}";
                        Console.WriteLine($"{type.Name} - {typeIn}");
                        stream.WriteLine($"{type.Name} - {typeIn}");
                        foreach (var memberInfo in type.GetMembers())
                        {
                            if (memberInfo is MethodInfo)
                            {
                                var methodInfo = memberInfo as MethodInfo;
                                Console.WriteLine($"Метод - {methodInfo.Name}, {methodInfo.ReflectedType}");
                                stream.WriteLine($"Метод - {methodInfo.Name}, {methodInfo.ReflectedType}");
                                foreach (var parameter in methodInfo.GetParameters())
                                {
                                    Console.WriteLine($"Параметр - {parameter.ParameterType}, {parameter.Name}");
                                    stream.WriteLine($"Параметр - {parameter.ParameterType}, {parameter.Name}");
                                }
                            }
                            else if (memberInfo is ConstructorInfo)
                            {
                                var constructorInfo = memberInfo as ConstructorInfo;
                                Console.WriteLine($"Конструктор - {constructorInfo.Name}, {constructorInfo.ReflectedType}");
                                stream.WriteLine($"Конструктор - {constructorInfo.Name}, {constructorInfo.ReflectedType}");
                                foreach (var parameter in constructorInfo.GetParameters())
                                {
                                    Console.WriteLine($"Параметр- {parameter.ParameterType}, {parameter.Name}");
                                    stream.WriteLine($"Параметр- {parameter.ParameterType}, {parameter.Name}");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Incorrect text");
            }
            Console.ReadLine();
        }
    }
}

