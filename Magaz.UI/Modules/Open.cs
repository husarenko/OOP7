using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Magaz.Logic;

namespace Magaz.UI
{
    internal class Open
    {
        public static List<Autocenter> FromCsv(string path)
        {
            List<Autocenter> info = new List<Autocenter>();
            try
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(path).ToList();
                Console.WriteLine("Вмiст Csv файлу:");
                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                foreach (var item in lines)
                {
                    Autocenter? shop;
                    bool result = Autocenter.TryParse(item, out shop);
                    if (result) { info.Add(shop); }
                }
            }
            catch (IOException ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return info;
        }
        public static List<Autocenter> FromJson(string path)
        {
            List<Autocenter> info = new List<Autocenter>();
            try
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(path).ToList();
                Console.WriteLine("Вмiст Json файлу:");
                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                foreach (var item in lines)
                {
                    Autocenter? items = JsonSerializer.Deserialize<Autocenter>(item);
                    if (items != null)
                        info.Add(items);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return info;
        }
    }
}
