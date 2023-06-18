using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlatotursachi
{
    internal class Program
    {

            static int maxX, maxY;
            static int m, n;
            static string[,] field;
            static int totalDiamonds = 0;
            static int ourGuyX, ourGuyY;

            static void Main()
            {
                Console.Clear();

                Console.WriteLine("Въведете размери на полето (m и n):");
                m = ReadNumber("m");
                n = ReadNumber("n");

                field = CreateField(m, n);

                DrawField();

                MoveOurGuy();

                Console.ReadLine();
            }

            static int ReadNumber(string variableName)
            {
                int number;
                while (true)
                {
                    Console.Write($"{variableName} = ");
                    if (int.TryParse(Console.ReadLine(), out number) && number > 10 && number <= Console.WindowWidth)
                    {
                        return number;
                    }
                    else
                    {
                        Console.WriteLine($"Невалидно число! {variableName} трябва да е цяло число по-голямо от 10 и не по-голямо от {Console.WindowWidth}.");
                    }
                }
            }

            static string[,] CreateField(int m, int n)
            {
                string[,] field = new string[m, n];
                maxX = m - 1;
                maxY = n - 1;

                Random random = new Random();

                int totalCells = m * n;
                totalDiamonds = totalCells / 10;

                for (int i = 0; i < totalDiamonds; i++)
                {
                    int x = random.Next(m);
                    int y = random.Next(n);
                    field[x, y] = "Diamond";
                }

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (field[i, j] == null)
                        {
                            int randomValue = random.Next(100);
                            if (randomValue < 40)
                                field[i, j] = "Ground";
                            else if (randomValue < 70)
                                field[i, j] = "Grass";
                            else if (randomValue < 90)
                                field[i, j] = "Tree";
                            else
                                field[i, j] = "Stone";
                        }
                    }
                }

                int ourGuyPosX = random.Next(m);
                int ourGuyPosY = random.Next(n);
                field[ourGuyPosX, ourGuyPosY] = "OurGuy";
                ourGuyX = ourGuyPosX;
                ourGuyY = ourGuyPosY;

                return field;
            }

            static void DrawField()
            {
                Console.Clear();

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        string cellValue = field[i, j];
                        if (cellValue == "Ground")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write('\u2592');
                        }
                        else if (cellValue == "Grass")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('\u2593');
                        }
                        else if (cellValue == "Tree")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write('\u2663');
                        }
                        else if (cellValue == "Stone")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write('\u0665');
                        }
                        else if (cellValue == "OurGuy")
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write('\u263A');
                        }
                        else if (cellValue == "Diamond")
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write('\u2666');
                        }
                        else
                        {
                            Console.Write(' '); // Добавена проверка за празна клетка
                        }
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();
            }
            static void MoveOurGuy()
            {
                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    Console.Clear();

                    int newOurGuyX = ourGuyX;
                    int newOurGuyY = ourGuyY;

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            newOurGuyX--;
                            break;
                        case ConsoleKey.DownArrow:
                            newOurGuyX++;
                            break;
                        case ConsoleKey.LeftArrow:
                            newOurGuyY--;
                            break;
                        case ConsoleKey.RightArrow:
                            newOurGuyY++;
                            break;
                        default:
                            continue;
                    }

                    if (newOurGuyX >= 0 && newOurGuyX <= maxX && newOurGuyY >= 0 && newOurGuyY <= maxY)
                    {
                        if (field[newOurGuyX, newOurGuyY] == "Diamond")
                        {
                            totalDiamonds--;
                        }

                        field[ourGuyX, ourGuyY] = null;
                        field[newOurGuyX, newOurGuyY] = "OurGuy";
                        ourGuyX = newOurGuyX;
                        ourGuyY = newOurGuyY;
                    }

                    DrawField();

                    if (totalDiamonds == 0)
                    {
                        Console.WriteLine("Поздравления! Вие събрахте всички диаманти!");
                        break;
                    }
                }
            }

            static void MoveOurGuyTo(int x, int y)
            {
                if (x >= 0 && x <= maxX && y >= 0 && y <= maxY)
                {
                    if (field[x, y] == "Diamond")
                    {
                        totalDiamonds--;
                    }

                    field[ourGuyX, ourGuyY] = null;
                    field[x, y] = "OurGuy";
                    ourGuyX = x;
                    ourGuyY = y;
                }
            }
        }
    }

