using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Magaz.Logic
{
    public class Car
    {
        public int Number { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public override string ToString()
        {
            return $"{Number},{Name},{Price}";
        }
    }

    public class Autocenter
    {
        private int cash;
        private int id;
        private List<string> items = new List<string>();
        public List<Car> cars = new List<Car>();

        private static int numberOfObjects = 0;

        public static int NumberOfObjects
        {
            get { return numberOfObjects; }
        }

        public Autocenter()
        {
            Autovalue = "DefaultAutovalue";
            Cash = 0;
            items = new List<string>();
            cars = new List<Car>();
        }

        public Autocenter(int initialCash) : this()
        {
            Cash = initialCash;
        }

        public Autocenter(int initialCash, List<string> initialItems) : this(initialCash)
        {
            Items = initialItems;
        }

        public Autocenter(int money, string car)
        {
            Cash = money;
            id = 1;
            if (items.Count != 0)
                items[0] = $"{car} - {GetCarPrice(id, 1)}$";
            else
                items.Add($"{car} - {GetCarPrice(id, 1)}$");
        }

        public static Autocenter Parse(string s)
        {
            try
            {
                Autocenter autocenter = new Autocenter();
                string[] parts = s.Split(',');

                if (parts.Length != 2)
                {
                    throw new FormatException("Invalid format for Autocenter string");
                }

                if (!int.TryParse(parts[0], out int money))
                {
                    throw new FormatException("Invalid format for Autocenter string");
                }

                if (!Enum.TryParse(parts[1], out Hatchback hatch) &&
                    !Enum.TryParse(parts[1], out Coupe coupe) &&
                    !Enum.TryParse(parts[1], out Sedan sedan))
                {
                    throw new FormatException("Invalid format for Autocenter string");
                }

                autocenter = new Autocenter(money, parts[1]);
                return autocenter;
            }
            catch (Exception ex)
            {
                throw new FormatException("Error parsing string to Autocenter", ex);
            }
        }

        public static bool TryParse(string s, out Autocenter obj)
        {
            obj = null;
            try
            {
                obj = Parse(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Cash
        {
            get { return cash; }
            private set
            {
                if (value < 0 || value > 50000)
                    throw new Exception("Сума повина бути бiльша за 100$ та не перевищувати 50000$");
                else
                    cash = value;
            }
        }

        public int Id => id;

        public List<string> Items
        {
            get { return items; }
            private set { items = value; }
        }

        private static List<int> coupelist { get; } = new List<int>() { 12000, 19500, 7300 };
        private static List<int> sedanlist { get; } = new List<int>() { 44500, 36500, 11500 };
        private static List<int> hatchbacklist { get; } = new List<int>() { 16300, 8100, 2300 };

        public string Autovalue { get; set; }

        public int InputCash(int amount)
        {
            Cash += amount;
            return Cash;
        }

        public int checking(string check)
        {
            int a = Convert.ToInt32(check);
            return a;
        }

        public void checklist()
        {
            if (Items.Count == 0) throw new Exception("Список пустий. Перевiрте ще раз");
        }

        public void checklist(int check)
        {
            if (check == 0) throw new Exception("Введіть коректне значення!");
        }

        public void remove()
        {
            Items.Clear();
        }

        public string cars_output()
        {
            return cars_output(1);
        }

        public string cars_output(int a)
        {
            string res = "";
            switch (a)
            {
                case 1: Coupe(ref res); break;
                case 2: Hatchback(ref res); break;
                case 3: Sedan(ref res); break;
                default: throw new Exception("Введiть коректне значення!");
            }
            return res;
        }

        private void Coupe(ref string res)
        {
            id = 1;
            for (int i = 1; i < 4; i++) { res += ($"{i} - {(Coupe)i} {coupelist[i - 1]}$\n"); }
        }

        private void Hatchback(ref string res)
        {
            id = 2;
            for (int i = 1; i < 4; i++) { res += ($"{i} - {(Hatchback)i} {hatchbacklist[i - 1]}$\n"); }
        }

        private void Sedan(ref string res)
        {
            id = 3;
            for (int i = 1; i < 4; i++) { res += ($"{i} - {(Sedan)i} {sedanlist[i - 1]}$\n"); }
        }

        public void car_purchased(int a)
        {
            int selectedCarPrice = GetCarPrice(Id, a);

            if ((Cash - selectedCarPrice) >= 0)
            {
                Car selectedCar = new Car
                {
                    Number = a,
                    Name = GetCarName(Id, a),
                    Price = selectedCarPrice
                };

                if (cars.Count > 0)
                {
                    cars[0] = selectedCar;
                }
                else
                {
                    cars.Add(selectedCar);
                }

                Cash -= selectedCar.Price;
                numberOfObjects++;
            }
            else
            {
                throw new Exception("Не вистачає грошей :(");
            }
        }

        private string GetCarName(int carType, int carNumber)
        {
            switch (carType)
            {
                case 1: return ((Coupe)carNumber).ToString();
                case 2: return ((Hatchback)carNumber).ToString();
                case 3: return ((Sedan)carNumber).ToString();
                default: throw new Exception("Невірний тип автівки");
            }
        }

        private int GetCarPrice(int carType, int carNumber)
        {
            switch (carType)
            {
                case 1: return coupelist[carNumber - 1];
                case 2: return hatchbacklist[carNumber - 1];
                case 3: return sedanlist[carNumber - 1];
                default: throw new Exception("Невірний тип автівки");
            }
        }

        public static string GetPriceDynamics(int carType)
        {
            switch (carType)
            {
                case 1: return GetDynamicsString("Купе", coupelist);
                case 2: return GetDynamicsString("Хечбек", hatchbacklist);
                case 3: return GetDynamicsString("Седан", sedanlist);
                default: throw new ArgumentException("Невірний тип кузову :(");
            }
        }

        private static string GetDynamicsString(string carTypeName, List<int> prices)
        {
            string dynamics = $"Динаміка цін для {carTypeName}:\n";

            for (int i = 0; i < prices.Count - 1; i++)
            {
                int change = prices[i + 1] - prices[i];
                dynamics += $"Від {i + 1} до {i + 2}: {change}$\n";
            }

            return dynamics;
        }

        public static string GetCoupesDynamics()
        {
            return GetDynamicsString("Купе", coupelist);
        }

        public static string GetHatchbacksDynamics()
        {
            return GetDynamicsString("Хечбек", hatchbacklist);
        }

        public static string GetSedansDynamics()
        {
            return GetDynamicsString("Седан", sedanlist);
        }

        public void output(ref string output)
        {
            output = "";
            if (cars.Count != 0)
                output = $"Ваше придбання: \n{cars[0].Name} - {cars[0].Price}$\n";
            else
            {
                output = "Ваша корзина порожня :(\n";
            }
            if (Cash != 0)
                output += $"Здача: {Cash}$\n";
        }

        public string String()
        {
            string carString = string.Join(",", cars.Select(car => $"{car.Number} - {car.Name} {car.Price}$"));
            return $"{Cash};{carString}";
        }

        public void AddCar(Car car)
        {
            cars.Add(car);
        }

        public void RemoveCar(Car car)
        {
            cars.Remove(car);
        }

        public Car FindCar(int carNumber)
        {
            return cars.FirstOrDefault(car => car.Number == carNumber);
        }

        public void SaveToCsv(string path)
        {
            List<string> lines = new List<string>();
            foreach (var car in cars)
            {
                lines.Add(car.ToString());
            }

            try
            {
                File.WriteAllLines(path, lines);
                Console.WriteLine($"Збережено у файл: {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження в файл: {ex.Message}");
            }
        }
        public void SaveToJson(string path)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(cars);
                File.WriteAllText(path, jsonString);
                Console.WriteLine($"Збережено у файл: {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження в файл: {ex.Message}");
            }
        }
        public void LoadFromCsv(string path)
        {
            try
            {
                List<string> lines = File.ReadAllLines(path).ToList();
                cars.Clear();

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        cars.Add(new Car
                        {
                            Number = int.Parse(parts[0]),
                            Name = parts[1],
                            Price = int.Parse(parts[2])
                        });
                    }
                }

                Console.WriteLine($"Зчитано з файлу: {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання з файлу: {ex.Message}");
            }
        }
        public void LoadFromJson(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                cars = JsonSerializer.Deserialize<List<Car>>(jsonString);

                Console.WriteLine($"Зчитано з файлу: {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання з файлу: {ex.Message}");
            }
        }
    }
}
