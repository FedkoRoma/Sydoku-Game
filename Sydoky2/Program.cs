using System;
using System.Text;

public class Game
{
    private static int[,] table;
    private static int size;
    private static int numeric;
    
    public static void Sydoky(int format, int figure)
    {
        numeric = figure;
        size = format;
        table = new int[size, size];
        
        int[] Numbers = GenerateShuffledNumbers();
        
        if (FillTable(table, size, Numbers, 0, 0))
        {
            Delete(table, numeric);
        }
    }
    private static int[] GenerateShuffledNumbers()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Random random = new Random();
        for (int i = 9 - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            Swap(i, j, numbers);
        }
        return numbers;
    }
    private static void Swap(int i, int j, int[]numbers)
    {
        int temp = numbers[i];
        numbers[i] = numbers[j];
        numbers[j] = temp;
    }
    private static void Delete(int[,] table,int numeric)
    {
        int j = 0, m = 0;
        Random random = new Random();
        for (int i = 0; i < numeric; i++)
        {
            do
            {
                j = random.Next(0, 9);
                m = random.Next(0, 9);
            }while(table[j, m] == 0);
            table[j, m] = 0;
        }
    }

    public static bool FillTable(int[,] table, int size, int[] Numbers, int line, int column)
    {
        if (line == size)
            return true;

        int nextLine = (column == size - 1) ? line + 1 : line;
        int nextColumn = (column == size - 1) ? 0 : column + 1;

        foreach (int number in Numbers)
        {
            if (CheckInLine(table, line, size, number) &&
                CheckInColumn(table, column, size, number) &&
                CheckInBlock(table, line, column, number))
            {
                table[line, column] = number;

                if (FillTable(table, size, Numbers, nextLine, nextColumn))
                    return true;

                table[line, column] = 0;
            }
        }
        return false;
    }

    private static bool CheckInLine(int[,] table, int line, int size, int number)
    {
        for (int i = 0; i < size; i++)
        {
            if (table[line, i] == number)
                return false;
        }
        return true;
    }

    private static bool CheckInColumn(int[,] table, int column, int size, int number)
    {
        for (int i = 0; i < size; i++)
        {
            if (table[i, column] == number)
                return false;
        }
        return true;
    }

    private static bool CheckInBlock(int[,] table, int line, int column, int number)
    {
        int blockSize = 3;
        int startLine = (line / blockSize) * blockSize;
        int startColumn = (column / blockSize) * blockSize;

        for (int i = 0; i < blockSize; i++)
        {
            for (int j = 0; j < blockSize; j++)
            {
                if (table[startLine + i, startColumn + j] == number)
                    return false;
            }
        }
        return true;
    }

    public int[,] Array()
    {
        return table;
    }
    public int Size()
    {
        return size;
    }
    public int Numeric()
    {
        return numeric;
    }
}

class User
{
    private static int[] Compare;
    public static void user()
    {
        Compare = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, };
        Game game = new Game();
        int[,] n = game.Array();
        int size = game.Size();
        int numeric = game.Numeric();
        PrintTable(n, size);
        WorkWithUser(n, size, numeric);
        Checking(n, Compare, size);
    }
    
    private static void Checking(int[,] n, int[] Compare, int size) 
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        if(CheckLine(n, Compare, size) && CheckColum(n, Compare, size))
        {
            Console.WriteLine("Ви правильно вирішили задачу");
        }
        else
        {
            Console.WriteLine("Алгоритм знайшов помилку");
        }
    }
    private static bool CheckLine(int[,] n, int[] Compare, int size)
    {
        for (int g = 0; g < size; g++)
        {
            bool[] found = new bool[size + 1];
            for (int i = 0; i < size; i++)
            {
                int number = n[g, i];
                if (found[number])
                    return false;
                found[number] = true;
            }
        }
        return true;
    }
    private static bool CheckColum(int[,] n, int[] Compare, int size)
    {
        for (int g = 0; g < size; g++)
        {
            bool[] found = new bool[size + 1];
            for (int j = 0; j < size; j++)
            {
                int number = n[j, g];
                if (found[number])
                    return false;
                found[number] = true;
            }
        }
        return true;
    }

    private static void WorkWithUser(int[,] n, int size, int numeric)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        int numerocity = 0;
        Console.WriteLine("Ваше завдання вводити координату x та y комірки у яку ви хочете вставити число. Комірка має містити цифру 0, саме замість 0 потрібно вставити потрібну цифру. Координата x позначає номер стовпця з 1 по 9 і знаходиться по горизонталі. Координата y, у свою чергу, позначає номер рядка з 1 по 9 і знаходиться по вертикалі. Координати підсвічуються жовтим кольором");
        while(numerocity != numeric)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Введіть координату x:");
            int i = int.Parse(Console.ReadLine()) - 1;
            
            if(i < 0 || i >= size)
            {
                Console.WriteLine("Координата x за межами допустимого діапазону. Спробуйте ще раз.");
                continue;
            }

            Console.WriteLine("Введіть координату y:");
            int j = int.Parse(Console.ReadLine()) - 1;
            
            while(j < 0 || j >= size)
            {
                Console.WriteLine("Координата y за межами допустимого діапазону. Спробуйте ще раз.");
                Console.WriteLine("Введіть координату y:");
                j = int.Parse(Console.ReadLine()) - 1;
            }
            if (n[j, i] != 0)
            {
                Console.WriteLine("Ця комірка вже заповнена. Спробуйте іншу.");
                continue;
            }

            Console.WriteLine("Введіть значення числа:");
            int number = int.Parse(Console.ReadLine());
            
            while(number < 1 || number > 9)
            {
                Console.WriteLine("Число повинно бути в межах від 1 до 9. Спробуйте ще раз.");
                Console.WriteLine("Введіть значення числа:");
                number = int.Parse(Console.ReadLine());
            }
            
            n[j, i] = number;
            numerocity++;
            PrintTable(n, size);
        }
    }
    private static void PrintTable(int[,] table, int size)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        Console.Write(" x:");
        for(int r = 1; r <= size; r++)
        {
            Console.Write(r + " ");
            if (r % 3 == 0 && r != 9)
            {
                Console.Write("| ");
            }
        }
        
        Console.WriteLine();
        Console.Write("y: ");
        for(int r = 1; r <= size; r++)
        {
            Console.Write("  ");
            if (r % 3 == 0 && r != 9)
            {
                Console.Write("| ");
            }
        }
        Console.WriteLine();
        
        for (int i = 0; i < size; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("--");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("-------+-------+------");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write((i + 1) + "  ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write((i + 1) + "  ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
            for (int j = 0; j < size; j++)
            {
                if (j % 3 == 0 && j != 0)
                {
                    Console.Write("| ");
                }
                Console.Write(table[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
class Program
{
    static void Main()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        int numeric = 5;
        int size = 9;
        Game.Sydoky(size, numeric);
        User.user();
    }
}