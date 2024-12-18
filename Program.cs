using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laba_5
{
    internal class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("Главное меню");
            Console.WriteLine("1. Сформировать динамический двумерный массив.");
            Console.WriteLine("2. Добавить К строк в начало матрицы.");
            Console.WriteLine("3. Сформировать динамический рваный массив.");
            Console.WriteLine("4. Удалить строку с заданным номером в рваном массиве.");
            Console.WriteLine("5. В строке, поданной на вход, перевернуть каждое слово, номер которого в предложении, совпадает с его длиной.");
            Console.WriteLine("0. Выход.");
            Console.Write("Выберите опцию: ");
        }


        static bool isConvert;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            string choice;
            int[,] origMatr = null;
            int[][] tornMatr = null;
            do
            {
                PrintMenu();
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        origMatr = DynamicArrayCreate();
                        break;
                    case "2":
                        int amount = ReadNumNotNegative("Введите количество строк для добавления в матрицу");
                        origMatr = AddStrings(origMatr, amount);
                        PrintMatr(origMatr);
                        break;
                    case "3":
                        CreateTornMatr();
                        break;
                    case "4":
                        int str = ReadNumNotNegative("Введите номер строки для удаления");
                        tornMatr = DeleteString(tornMatr, str);
                        PrintTornMatr(tornMatr, "Ваш массив:");
                        break;
                    case "5":
                        Console.WriteLine();
                        Console.WriteLine("Введите строку:");
                        ReverseWordsInString(Console.ReadLine());
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Пожалуйста, попробуйте снова.");
                        break;
                }
            } while (choice != "0");
        }

        static int[,] RandomCreateMatr(int str, int col)
        {
            int[,] matr = new int[str, col];

            Random rnd = new Random();

            for (int i = 0; i < str; i++)
                for (int j = 0; j < col; j++)
                {
                    matr[i, j] = rnd.Next(-10, 10);
                }

            PrintMatr(matr);

            return matr;
        }

        static int ReadNum(string message)
        {
            int num;
            do
            {
                Console.WriteLine(message);
                isConvert = int.TryParse(Console.ReadLine(), out num);

                if (!isConvert)
                {
                    Console.WriteLine("Введен неверный формат данных. Повторите ввод");
                }

            } while (!isConvert);
            return num;
        }
        static int ReadNumNotNegative(string message)
        {
            int num;
            do
            {
                Console.WriteLine(message);
                isConvert = int.TryParse(Console.ReadLine(), out num);

                if (num < 0)
                {
                    isConvert = false;
                }

                if (!isConvert)
                {
                    Console.WriteLine("Введен неверный формат данных. Повторите ввод");
                }

            } while (!isConvert);
            return num;
        }

        static int[,] DynamicArrayCreate()
        {
            Console.Write("Выберите метод (1 - случайные числа, 2 - ввод с клавиатуры): ");
            string ans = Console.ReadLine();
            int[,] array = null;

            switch (ans)
            {
                case "1":
                    int str = ReadNumNotNegative("Введите количество строк");
                    int col = ReadNumNotNegative("Введите количество столбцов");

                    array = new int[str, col];
                    array = RandomCreateMatr(str, col);
                    break;
                case "2":
                    str = ReadNum("Введите количество строк");
                    col = ReadNum("Введите количество столбцов");

                    array = new int[str, col];

                    for (int i = 0; i < str; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            array[i, j] = ReadNum($"{i + 1} строка, {j + 1} столбец: ");
                        }
                    }
                    PrintMatr(array);
                    break;
                default:
                    Console.WriteLine("Неверный ввод. Повторите снова.");
                    break;
            }
            return array;
        }

        static bool IsEmpty(int[,] matr)
        {
            return (matr == null || matr.Length == 0);
        }
        
        static bool IsEmptyTorn(int[][] matr)
        {
            return (matr == null || matr.Length == 0);
        }

        static void PrintMatr(int[,] matr)
        {
            Console.WriteLine("Элементы массива:");

            if (IsEmpty(matr))
                Console.WriteLine("Матрица пустая");
            else
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        Console.Write($"{matr[i, j],4}");
                    }
                    Console.WriteLine();
                }
        }

        static int[,] AddStrings(int[,] matr, int amount)
        {
            int realStr = matr.GetLength(0);
            int realCol = matr.GetLength(1);

            int[,] newMatr = new int[realStr + amount, realCol];

            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    newMatr[i + amount, j] = matr[i, j];
                }
            }
            Console.Write("Выберите метод заполнения добавленных строк (1 - случайные числа, 2 - ввод с клавиатуры): ");
            string ans = Console.ReadLine();

            switch (ans)
            {
                case "1":
                    for (int i = 0; i < amount; i++)
                        for (int j = 0; j < matr.GetLength(1); j++)
                        {
                            newMatr[i, j] = rnd.Next(-10, 10);
                        }
                    break;
                case "2":
                    Console.WriteLine("Введите элементы: ");

                    for (int i = 0; i < amount; i++)
                    {
                        for (int j = 0; j < matr.GetLength(1); j++)
                        {
                            newMatr[i, j] = ReadNum($"{i + 1} строка, {j + 1} столбец: ");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Неверный ввод. Повторите снова.");
                    break;
            }
            return newMatr;
        }

        static void CreateTornMatr()
        {

            Console.Write("Выберите метод заполнения (1 - случайные числа, 2 - ввод с клавиатуры): ");
            string ans = Console.ReadLine();

            switch (ans)
            {
                case "1":
                    int stroka = rnd.Next(1, 20);
                    int[][] matr = new int[stroka][];
                    for (int i = 0; i < stroka; i++)
                    {
                        int colomn = rnd.Next(1, 10);
                        matr[i] = new int[colomn];

                        for (int j = 0; j < colomn; j++)
                        {
                            matr[i][j] = rnd.Next(-10, 10);
                        }
                    }
                    PrintTornMatr(matr, "Ваш массив: ");
                    break;
                case "2":
                    int str = ReadNum("Введите количество строк для создания рваной матрицы");
                    int[][] tMatr = new int[str][];
                    for (int i = 0; i < str; i++)
                    {
                        int col = ReadNum($"Введите длину {i + 1} строки");
                        tMatr[i] = new int[col];
                        for (int j = 0; j < col; j++)
                        {
                            tMatr[i][j] = ReadNum($"{i + 1} строка, {j + 1} столбец: ");
                        }
                    }
                    PrintTornMatr(tMatr, "Ваш массив: ");
                    break;
                default:
                    Console.WriteLine("Неверный ввод. Повторите снова.");
                    break;
            }
        }

        static void PrintTornMatr(int[][] matr, string msg)
        {
            Console.WriteLine(msg);
            if (IsEmptyTorn(matr))
            {
                Console.WriteLine("Массив пустой");
            }
            else
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr[i].GetLength(0); j++)
                    {
                        Console.Write($"{matr[i][j], 4}");
                    }
                    Console.WriteLine();
                }
        }

        static int[][] DeleteString(int[][] matr, int str)
        {
            int row = str - 1;
            if (row >= matr.Length)
            {
                Console.WriteLine("Неверный номер строки для удаления");
                return matr;
            }
            int[][] newMatr = new int[matr.Length - 1][];

            for (int i = 0, j = 0; i < matr.Length; i++)
            {
                if (i != row)
                {
                    newMatr[j] = matr[i];
                    j++;
                }
            }


            return newMatr;
        }

        static void ReverseWordsInString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Строка пуста.");
                return;
            }

            string[] sentences = Regex.Split(input, @"(?<=[.!?])\s+");
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < sentences.Length; i++)
            {
                string sentence = sentences[i].Trim();
                string[] words = sentence.Split(new char[] { ' ', ',', ';', ':', '.', '!', '?' },
                                                 StringSplitOptions.RemoveEmptyEntries);
                StringBuilder modifiedSentence = new StringBuilder(sentence);
                for (int j = 0; j < words.Length; j++)
                {
                    string word = words[j];

                    if (j + 1 == word.Length)
                    {
                        char[] charArray = word.ToCharArray();

                        Array.Reverse(charArray);

                        string reversedWord = new string(charArray);
                        modifiedSentence.Replace(word, reversedWord);
                    }
                }
                result.Append(modifiedSentence + " ");
            }

            Console.WriteLine("Ваша строка: ");
            Console.WriteLine(result.ToString().Trim());
        }

    }
}
