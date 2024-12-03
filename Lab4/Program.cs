using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab4
{
    internal class Program
    {
        static void WithoutUsingMethods(int[] array)
        {
            int maxIndex = 0;
            int minIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] >= array[maxIndex])
                {
                    maxIndex = i;
                }
                if (array[i] <= array[minIndex])
                {
                    minIndex = i;
                }
            }

            if (maxIndex < minIndex)
            {
                (maxIndex,minIndex) = (minIndex, maxIndex);
            }
            minIndex++;

            if (minIndex >= maxIndex)
            {
                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Діапазон між останніми входженнями максимального та мінімального значень не існує.");
            }
            else
            {
                int sum = 0;
                for (int i = minIndex; i < maxIndex; i++)
                {
                    sum += array[i];
                }
                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Елементи масиву, які розміщені між останніми входженнями максимального та мінімального чисел: [" + string.Join(", ", array[minIndex..maxIndex]) + "]");
                Console.WriteLine("Середнє арифметичне: " + (double)sum / (maxIndex - minIndex));
            }
            Main();
        }
        static void UsingMethods(int[] array)
        {
            int maxIndex = Array.LastIndexOf(array, array.Max());
            int minIndex = Array.LastIndexOf(array, array.Min());

            (maxIndex, minIndex) = (Math.Max(maxIndex, minIndex), Math.Min(maxIndex, minIndex) + 1);

            if (minIndex >= maxIndex)
            {
                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Елементів між останніми входженнями максимального та мінімального чисел не існує.");
            }
            else
            {
                Console.WriteLine("Масив: [" + string.Join(", ", array) + "]");
                Console.WriteLine("Елементи масиву, які розміщені між останніми входженнями максимального та мінімального чисел: [" + string.Join(", ", array[minIndex..maxIndex]) + "]");
                Console.WriteLine("Середнє арифметичне: " + array[minIndex..maxIndex].Average());
            }
            Main();
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
        static void RandomlyFillAnArray(int choice, int[] array)
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
        static void FillAnArrayOneByOne(int choice, int[] array)
        {
            Console.WriteLine("Введіть кожен елемент масива в окремому рядку");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }
            ChosenMethod(choice, array);
        }
        static void FillAnArrayInOneRow(int choice)
        {
            Console.WriteLine("\nМетод заповнення обрано");
            Console.WriteLine("Введіть всі елементи масива в одному рядку");
            string[] elements = Console.ReadLine().Trim().Split();
            int[] array = new int[elements.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(elements[i]);
            }
            ChosenMethod(choice, array);
        }
        static void StartToFillAnArray(out int[] array)
        {
            Console.WriteLine("\nМетод заповнення обрано");
            Console.WriteLine("Введіть скільки елементів має бути в масиві");
            int n = int.Parse(Console.ReadLine());
            array = new int[n];
        }
        static void HowToFillAnArray(int choice)
        {
            int choice2, choice3;
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
                        StartToFillAnArray(out array);
                        RandomlyFillAnArray(choice, array);
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
                                    StartToFillAnArray(out array);
                                    FillAnArrayOneByOne(choice, array);
                                    break;
                                case 2:
                                    FillAnArrayInOneRow(choice);
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
        static void Main()
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
