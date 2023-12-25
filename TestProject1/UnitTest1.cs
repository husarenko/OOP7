using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Magaz.Logic.Tests
{
    [TestClass]
    public class AutocenterTests
    {
        [TestClass]
        public class AutocenterConstructorTests
        {
            [TestMethod]
            public void DefaultConstructor_ShouldSetDefaultValues()
            {
                // Arrange
                Autocenter autocenter = new Autocenter();

                // Act

                // Assert
                Assert.AreEqual("DefaultAutovalue", autocenter.Autovalue);
                Assert.AreEqual(0, autocenter.Cash);
                Assert.AreEqual(0, autocenter.Items.Count);
            }

            [TestMethod]
            public void ConstructorWithInitialCash_ShouldSetCash()
            {
                // Arrange
                Autocenter autocenter = new Autocenter(1000);

                // Act

                // Assert
                Assert.AreEqual(1000, autocenter.Cash);
            }

            [TestMethod]
            public void ConstructorWithInitialItems_ShouldSetItems()
            {
                // Arrange
                List<string> initialItems = new List<string> { "Car1", "Car2" };
                Autocenter autocenter = new Autocenter(1000, initialItems);

                // Act

                // Assert
                CollectionAssert.AreEqual(initialItems, autocenter.Items);
            }

            [TestMethod]
            public void ConstructorWithMoneyAndCar_ShouldSetCashAndItems()
            {
                // Arrange

                // Act
                Autocenter autocenter = new Autocenter(20000, "Coupe");

                // Debug
                Console.WriteLine($"Debug: Cash={autocenter.Cash}, Items={string.Join(", ", autocenter.Items)}");

                // Assert
                Assert.AreEqual(20000, autocenter.Cash);
                Assert.AreEqual(1, autocenter.Items.Count);
                Assert.AreEqual("Coupe - 12000$", autocenter.Items[0]);
                Assert.AreEqual(1, autocenter.Id);
            }
        }

        [TestClass]
        public class AutocenterParseTests
        {
            [TestMethod]
            public void Parse_ValidString_ShouldReturnAutocenter()
            {
                // Arrange
                string input = "20000,Smart_Fortwo";

                // Act
                Autocenter autocenter = Autocenter.Parse(input);

                // Assert
                Assert.AreEqual(20000, autocenter.Cash);
                Assert.AreEqual(1, autocenter.Items.Count);
                Assert.AreEqual("Smart_Fortwo - 12000$", autocenter.Items[0]);
            }

            [TestMethod]
            public void Parse_InvalidString_ShouldThrowFormatException()
            {
                // Arrange
                string input = "invalid";

                // Act + Assert
                Assert.ThrowsException<FormatException>(() => Autocenter.Parse(input));
            }

            [TestMethod]
            public void TryParse_ValidString_ShouldReturnTrueAndAutocenter()
            {
                // Arrange
                string input = "20000,Smart_Fortwo";
                Autocenter autocenter;

                // Act
                bool result = Autocenter.TryParse(input, out autocenter);

                // Assert
                Assert.IsTrue(result);
                Assert.IsNotNull(autocenter);
            }

            [TestMethod]
            public void TryParse_InvalidString_ShouldReturnFalseAndNull()
            {
                // Arrange
                string input = "invalid";
                Autocenter autocenter;

                // Act
                bool result = Autocenter.TryParse(input, out autocenter);

                // Assert
                Assert.IsFalse(result);
                Assert.IsNull(autocenter);
            }
        }
        [TestClass]
        public class AutocenterPriceDynamicsTests
        {
            [TestMethod]
            public void GetPriceDynamics_ValidCarType_ShouldReturnDynamicsString()
            {
                // Arrange

                // Act
                string dynamics = Autocenter.GetPriceDynamics(1);

                // Assert
                StringAssert.Contains(dynamics, "Динаміка цін для Купе:");
            }


            [TestMethod]
            public void GetPriceDynamics_InvalidCarType_ShouldThrowArgumentException()
            {
                // Arrange

                // Act + Assert
                Assert.ThrowsException<ArgumentException>(() => Autocenter.GetPriceDynamics(0));
            }
        }
        [TestClass]
        public class AutocenterListTests
        {
            [TestMethod]
            public void AddCar_AddsCarToCarsList()
            {
                // Arrange
                Autocenter shop = new Autocenter();
                Car car = new Car
                {
                    Number = 1,
                    Name = "TestCar",
                    Price = 10000
                };

                // Act
                shop.AddCar(car);

                // Assert
                Assert.AreEqual(1, shop.cars.Count);
                Assert.AreEqual(car, shop.cars[0]);
            }

            [TestMethod]
            public void RemoveCar_RemovesCarFromCarsList()
            {
                // Arrange
                Autocenter shop = new Autocenter();
                Car car = new Car
                {
                    Number = 1,
                    Name = "TestCar",
                    Price = 10000
                };
                shop.AddCar(car);

                // Act
                shop.RemoveCar(car);

                // Assert
                Assert.AreEqual(0, shop.cars.Count);
            }

            [TestMethod]
            public void FindCar_ReturnsCorrectCar()
            {
                // Arrange
                Autocenter shop = new Autocenter();
                Car car1 = new Car
                {
                    Number = 1,
                    Name = "TestCar1",
                    Price = 10000
                };
                Car car2 = new Car
                {
                    Number = 2,
                    Name = "TestCar2",
                    Price = 12000
                };
                shop.AddCar(car1);
                shop.AddCar(car2);

                // Act
                Car foundCar = shop.FindCar(2);

                // Assert
                Assert.IsNotNull(foundCar);
                Assert.AreEqual(car2, foundCar);
            }
            private const string CsvFilePath = "testCsvFile.csv";
            private const string JsonFilePath = "testJsonFile.json";

            [TestMethod]
            public void SaveToCsv_ShouldSaveCarsToCsvFile()
            {
                // Arrange
                Autocenter autocenter = new Autocenter();
                autocenter.AddCar(new Car { Number = 1, Name = "TestCar1", Price = 10000 });
                autocenter.AddCar(new Car { Number = 2, Name = "TestCar2", Price = 15000 });

                // Act
                autocenter.SaveToCsv(CsvFilePath);

                // Assert
                Assert.IsTrue(File.Exists(CsvFilePath));

                // Clean up
                File.Delete(CsvFilePath);
            }

            [TestMethod]
            public void SaveToJson_ShouldSaveCarsToJsonFile()
            {
                // Arrange
                Autocenter autocenter = new Autocenter();
                autocenter.AddCar(new Car { Number = 1, Name = "TestCar1", Price = 10000 });
                autocenter.AddCar(new Car { Number = 2, Name = "TestCar2", Price = 15000 });

                // Act
                autocenter.SaveToJson(JsonFilePath);

                // Assert
                Assert.IsTrue(File.Exists(JsonFilePath));

                // Clean up
                File.Delete(JsonFilePath);
            }

            [TestMethod]
            public void LoadFromCsv_ShouldLoadCarsFromCsvFile()
            {
                // Arrange
                Autocenter autocenter = new Autocenter();
                autocenter.AddCar(new Car { Number = 1, Name = "TestCar1", Price = 10000 });
                autocenter.AddCar(new Car { Number = 2, Name = "TestCar2", Price = 15000 });
                autocenter.SaveToCsv(CsvFilePath);

                // Act
                autocenter.LoadFromCsv(CsvFilePath);

                // Assert
                Assert.AreEqual(2, autocenter.cars.Count);

                // Clean up
                File.Delete(CsvFilePath);
            }

            [TestMethod]
            public void LoadFromJson_ShouldLoadCarsFromJsonFile()
            {
                // Arrange
                Autocenter autocenter = new Autocenter();
                autocenter.AddCar(new Car { Number = 1, Name = "TestCar1", Price = 10000 });
                autocenter.AddCar(new Car { Number = 2, Name = "TestCar2", Price = 15000 });
                autocenter.SaveToJson(JsonFilePath);

                // Act
                autocenter.LoadFromJson(JsonFilePath);

                // Assert
                Assert.AreEqual(2, autocenter.cars.Count);

                // Clean up
                File.Delete(JsonFilePath);
            }
        }
    }
}


