using System;
using System.Collections.Generic;
using Magaz.Logic;

namespace Magaz.UI
{
    internal class Menu
    {
        static void Main(string[] args)
        {
            
            Autocenter shop = new Autocenter();
            int option;
            string output = "";
            int a;
            while (true)
            {
                try
                {
                    Console.WriteLine(@"Вiтаємо вас в автоцентрi! *wroom* *wroom*

Оберiть операцiю:
1) Додати грошей
2) Вивести на екран придбанну автiвку
3) Змiнити заказ
4) Видалити авто
5) Демонстрація поведінки об’єктів
6) Демонстрація роботи static методів
7) Зберегти колекцію
8) Зчитати колекцію
9) Очистити колекцію
0) Вихiд");

                    option = shop.checking(Console.ReadLine());

                    if (option == 0) break;

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine($"На вашому рахунку: {shop.InputCash(0)}$");
                            Console.WriteLine("Введiть сумму поповнення: ");
                            option = shop.checking(Console.ReadLine());
                            Console.WriteLine($"На вашому рахунку: {shop.InputCash(option)}$");
                            Console.WriteLine("Введiть рядок з характеристиками для додавання об'єкта:");
                            string inputString = Console.ReadLine();
                            if (Autocenter.TryParse(inputString, out Autocenter newObj))
                            {
                                Console.WriteLine("Машину придбано!");
                            }
                            else
                            {
                                Console.WriteLine("Невiрний тип даних.");
                            }
                            break;

                        case 2:
                            shop.output(ref output);
                            Console.WriteLine(output);
                            Console.WriteLine($"Кiлькiсть створених об'єктiв: {Autocenter.NumberOfObjects}");
                            break;

                        case 3:
                            shop.checklist();
                            Console.WriteLine(@"Оберiть операцiю:
1 - Змiнити заказ
0 - Вийти в головне меню");
                            option = shop.checking(Console.ReadLine());

                            if (option == 0) break;

                            if (option == 1)
                            {
                                Console.WriteLine("Оберiть тип кузова: \n1 - Купе \n2 - Хетчбек \n3 - Седан");
                                option = shop.checking(Console.ReadLine());
                                Console.WriteLine("Оберiть автiвку:");
                                output = shop.cars_output(option);
                                Console.WriteLine(output);
                                option = shop.checking(Console.ReadLine());
                                shop.car_purchased(option);
                                shop.output(ref output);
                                Console.WriteLine(output);
                                break;
                            }
                            break;

                        case 4:
                            shop.remove();
                            Console.WriteLine("Список очищенний");
                            break;

                        case 5:
                            shop.remove();
                            Console.WriteLine("Вiтаємо в нашому автосалонi!");
                            Console.WriteLine($"Ваш рахунок {shop.InputCash(0)}$");
                            Console.WriteLine(@"Бажаєте поповнити?
1 - Так
Будь-яке число - Нi");
                            option = shop.checking(Console.ReadLine());

                            if (option == 1)
                            {
                                Console.WriteLine("Ваша сумма:");
                                option = shop.checking(Console.ReadLine());
                                Console.WriteLine($"Ваш рахунок {shop.InputCash(option)}$");
                            }

                            Console.WriteLine("Оберiть тип кузова: \n1 - Купе \n2 - Хетчбек \n3 - Седан");
                            option = shop.checking(Console.ReadLine());
                            Console.WriteLine("Оберiть автiвку:");
                            output = shop.cars_output(option);
                            Console.WriteLine(output);
                            option = shop.checking(Console.ReadLine());
                            shop.car_purchased(option);
                            shop.output(ref output);
                            Console.WriteLine(output);
                            break;

                        case 6:
                            Console.WriteLine("Демонстрацiя роботи static методiв:");

                            string coupeDynamics = Autocenter.GetCoupesDynamics();
                            Console.WriteLine(coupeDynamics);

                            string hatchbackDynamics = Autocenter.GetHatchbacksDynamics();
                            Console.WriteLine(hatchbackDynamics);

                            string sedanDynamics = Autocenter.GetSedansDynamics();
                            Console.WriteLine(sedanDynamics);
                            Console.WriteLine($"Кiлькiсть створених об'єктiв: {Autocenter.NumberOfObjects}");
                            break;

                        case 7:
                            Console.WriteLine("Виберiть:\n1 - Зберегти у (*.Csv)\n2 - Зберегти у (*.Json)");
                            a = Convert.ToInt32(Console.ReadLine());
                            string? path;
                            switch (a)
                            {
                                case 1:
                                    Console.WriteLine("Введiть назву файла (*.csv):");
                                    path = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(path))
                                    {
                                        path += ".csv";
                                        shop.SaveToCsv(path);
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("Введiть назву файла (*.json):");
                                    path = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(path))
                                    {
                                        path += ".json";
                                        shop.SaveToJson(path);
                                    }
                                    break;
                                default: Console.WriteLine("Помилка! Немає такого значення!"); break;
                            }
                            break;

                        case 8:
                            Console.WriteLine("Виберiть:\n1 - Вiдкрити (*.Csv)\n2 - Вiдкрити (*.Json)");
                            a = Convert.ToInt32(Console.ReadLine());
                            switch (a)
                            {
                                case 1:
                                    Console.WriteLine("Введiть назву файла (*.csv):");
                                    path = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(path))
                                    {
                                        path += ".csv";
                                        shop.LoadFromCsv(path);
                                        Console.WriteLine("Об'єкти класу:");
                                        Console.WriteLine(shop.String());
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("Введiть назву файла (*.json):");
                                    path = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(path))
                                    {
                                        path += ".json";
                                        shop.LoadFromJson(path);
                                        Console.WriteLine("Об'єкти класу:");
                                        Console.WriteLine(shop.String());
                                    }
                                    break;
                                default: Console.WriteLine("Помилка! Немає такого значення!"); break;
                            }
                            break;

                        case 9:
                            shop.cars.Clear();
                            Console.WriteLine("Очищено");
                            break;

                        default: Console.WriteLine("Введiть цифри тiльки вiд 0 до 9"); break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Тiлькi цифри...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}


