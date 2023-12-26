using ReadDataConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataConsoleApp
{
    internal class Program
    {
        private static ImagesDBEntities _context = new ImagesDBEntities();
        static void Main(string[] args)
        {
            string imagesPath = @"C:\Users\admin\Downloads\09_1_2-2022_2_\09_1.2-2022_2\Вариант 2\Вариант 2\Сессия 1\Сотрудники_import";

            var employees = _context.Employees.ToList();
            var files = Directory.GetFiles(imagesPath);

            for (int i = 0; i < employees.Count(); i++)
            {
                foreach (var file in files)
                {
                    if (file.Contains(employees[i].SecondName.ToString()))
                    {
                        employees[i].ImageFile = File.ReadAllBytes(file);
                        break;
                    }
                }
            }

            _context.SaveChanges();

            Console.ReadLine();
        }
    }
}
