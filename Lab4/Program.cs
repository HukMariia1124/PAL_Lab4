using System;
using System.Net.NetworkInformation;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab4
{
    internal class Program
    {
        static void WithoutUsingMethods(int[] array)
        {
            int max = array[0];
            int min = array[0];
            int maxIndex = 0;
            int minIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] >= max)
                {
                    max = array[i];
                    maxIndex = i;
                }

                if (array[i] <= min)
                {
                    min = array[i];
                    minIndex = i;
                }
            }

            int startIndex = Math.Min(maxIndex, minIndex) + 1;
            int endIndex = Math.Max(maxIndex, minIndex);

            if (startIndex >= endIndex)
            {
                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Діапазон між останніми входженнями максимального та мінімального значень не існує.");
            }
            else
            {
                int[] array2 = array[startIndex..endIndex];
                int sum = 0;

                for (int i = 0; i < array2.Length; i++)
                {
                    sum += array2[i];
                }

                double average = (double)sum / array2.Length;

                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Елементи масиву, які розміщені між останніми входженнями максимального та мінімального чисел: [" + string.Join(", ", array2) + "]");
                Console.WriteLine("Середнє арифметичне: " + average);
            }
            Main(new string[] { });
        }
        static void UsingMethods(int[] array)
        {
            int max = array.Max();
            int min = array.Min();

            int maxIndex = Array.LastIndexOf(array, max);
            int minIndex = Array.LastIndexOf(array, min);

            int startIndex = Math.Min(maxIndex, minIndex) + 1;
            int endIndex = Math.Max(maxIndex, minIndex);

            if (startIndex >= endIndex)
            {
                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Елементів між останніми входженнями максимального та мінімального чисел не існує.");
            }
            else
            {
                int[] array2 = array[startIndex..endIndex];

                double average = array2.Average();

                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Елементи масиву, які розміщені між останніми входженнями максимального та мінімального чисел: [" + string.Join(", ", array2) + "]");
                Console.WriteLine("Середнє арифметичне: " + average);
            }
            Main(new string[] { });
        }
        static void ChosenMethod(int choice, int[] array)
        {
            if (choice == 1)
            {
                WithoutUsingMethods(array);
            }
            else
            {
                UsingMethods(array);
            }
        }
        static void RandomlyFillAnArray(int choice, int n, int[] array)
        {
            Console.WriteLine("Введіть мінімальне число в масиві");
            int min = int.Parse(Console.ReadLine());
            Console.WriteLine("Введіть максимальне число в масиві");
            int max = int.Parse(Console.ReadLine());
            Random rndGen = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rndGen.Next(min, max+1);
            }
            Console.WriteLine();
            ChosenMethod(choice, array);
        }
        static void FillAnArrayOneByOne(int choice, int n, int[] array)
        {
            Console.WriteLine("Введіть кожен елемент масива в окремому рядку");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }
            ChosenMethod(choice, array);
        }
        static void FillAnArrayInOneRow(int choice, int n, int[] array)
        {
            Console.WriteLine("Введіть всі елементи масива в одному рядку");
            string[] elements = Console.ReadLine().Trim().Split();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(elements[i]);
            }
            ChosenMethod(choice, array);
        }
        static void StartToFillAnArray(out int n, out int[] array)
        {
            Console.WriteLine("\nМетод заповнення обрано");
            Console.WriteLine("Введіть скільки елементів має бути в масиві");
            n = int.Parse(Console.ReadLine());
            array = new int[n];
        }
        static void HowToFillAnArray(int choice)
        {
            int choice2, choice3, n;
            int[] array;
            do
            {
                Console.WriteLine("\nЯк Ви бажаєте заповнити масив?");
                Console.WriteLine("Для заповнення випадково введіть 1.");
                Console.WriteLine("Для заповнення вручну введіть 2.");
                Console.WriteLine("Для переходу до минулого етапу введіть 0.");
                choice2 = int.Parse(Console.ReadLine());
                switch (choice2)
                {
                    case 1:
                        StartToFillAnArray(out n, out array);
                        RandomlyFillAnArray(choice, n, array);
                        break;
                    case 2:
                        Console.WriteLine("\nДля введення кожного елемента в окремому рядку введіть 1.");
                        Console.WriteLine("Для введення кожного елемента в одному рядку через пробіли та/або табуляції введіть 2.");
                        Console.WriteLine("Для переходу до минулого етапу введіть 0.");
                        choice3 = int.Parse(Console.ReadLine());
                        do
                        {
                            switch (choice3)
                            {
                                case 1:
                                    StartToFillAnArray(out n, out array);
                                    FillAnArrayOneByOne(choice, n, array);
                                    break;
                                case 2:
                                    StartToFillAnArray(out n, out array);
                                    FillAnArrayInOneRow(choice, n, array);
                                    break;
                                case 0:
                                    Console.WriteLine("Переходимо до минулого етапу.");
                                    break;
                                default:
                                    Console.WriteLine("Команда \"{0}\" не розпізнана. Зробіть, будь ласка, вибір із 1, 2, 0.", choice3);
                                    break;
                            }
                        }
                        while (choice3 != 0);
                        break;
                    case 0:
                        Console.WriteLine("Переходимо до минулого етапу.");
                        break;
                    default:
                        Console.WriteLine("Команда \"{0}\" не розпізнана. Зробіть, будь ласка, вибір із 1, 2, 0.", choice2);
                        break;
                }
            }
            while (choice2 != 0);
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            int choice;
            do
            {
                Console.WriteLine("\nПрограма може бути виконана двома способами.");
                Console.WriteLine("Для виконання БЕЗ використання властивостей і методів масиву введіть 1.");
                Console.WriteLine("Для виконання З використання властивостей і методів масиву введіть 2.");
                Console.WriteLine("Для виходу з програми введіть 0.");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Виконую програму БЕЗ їх використання.");
                        HowToFillAnArray(choice);
                        break;
                    case 2:
                        Console.WriteLine("Виконую програму З їх використанням.");
                        HowToFillAnArray(choice);
                        break;
                    case 0:
                        Console.WriteLine("Зараз завершимо, тільки натисніть будь ласка ще раз Enter.");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Команда \"{0}\" не розпізнана. Зробіть, будь ласка, вибір із 1, 2, 0.\n", choice);
                        break;
                }
            }
            while (choice != 0);
        }
    }
}
