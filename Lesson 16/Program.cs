using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;
using System.IO;

namespace Lesson_16
{
    //1.Необходимо разработать программу для записи информации о товаре в текстовый файл в формате json.
    //Разработать класс для моделирования объекта «Товар». Предусмотреть члены класса «Код товара» (целое число),
    //«Название товара» (строка), «Цена товара» (вещественное число).
    //Создать массив из 5-ти товаров, значения должны вводиться пользователем с клавиатуры.
    //Сериализовать массив в json-строку, сохранить ее программно в файл «Products.json».
    //2.Необходимо разработать программу для получения информации о товаре из json-файла.
    //Десериализовать файл «Products.json» из задачи 1. Определить название самого дорогого товара.
    class Program
    {
        public static JavaScriptEncoder Encoder { get; private set; }

        static void Main(string[] args)
        {
            //Задаем кол-во товаров
            Console.WriteLine("Введите количество товара :");
            int count = Convert.ToInt32(Console.ReadLine());
            //Объявляем массив товаров
            Product[] arrayProducts = new Product[count];

            //В цикле записываем с использованием метода значение в класс продукт
            for (int i = 0; i < arrayProducts.Length; i++)
            {
                arrayProducts[i] = SetProduct(); 
                
                count++;
            }
            //Записываем и сохраняем массив в файл
            StreamWriter swArrayPproduct = File.CreateText("Products.json");
            swArrayPproduct.WriteLine(JsonSerializer.Serialize(arrayProducts));
            swArrayPproduct.Close();//Закрываем поток
            JsonSerializerOptions options = new JsonSerializerOptions();
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
            };
            //Создаем массив класса Товар присваиваем ему значение десериализовацию <Класс>(Путь)
            Product[] arrayReadJson = JsonSerializer.Deserialize<Product[]>(File.ReadAllText("Products.json"),options);
            double maxPrice = 0;
            string productExpensiv = "";

            // поиск максимальной цены(не смог реализовать в цикле ФОР)
            foreach (var i in arrayProducts)
            {
                if (i.ProductPrice >= maxPrice)
                {
                    maxPrice = i.ProductPrice;
                    productExpensiv = i.ProductName;
                }
            }
            Console.WriteLine("Самый дорогой товар  {0}",productExpensiv);
            Console.ReadKey();
        }
        static Product SetProduct() //Не смог разобраться как реализовать метод непосредственно в классе с последующм вызовом в массиве.
        {
            Product product = new Product();
            Console.WriteLine("Введите данные товара :");
            Console.WriteLine("Введите код товара");
            product.ProductCode = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название товара");
            product.ProductName = Console.ReadLine();
            Console.WriteLine("Введите цену товара");
            product.ProductPrice = Convert.ToDouble(Console.ReadLine());

            return product;
        }
        class Product
        {
            public int ProductCode { get; set; }
            public string ProductName { get; set; }
            public double ProductPrice { get; set; }
        }
    }

}
//Создание класса для моделирования объекта «Товар».
[Serializable]
class Product
{
    public int ProductCode { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
}

