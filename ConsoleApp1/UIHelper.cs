namespace ConsoleApp1;

public static class UIHelper
{
    public static void DrawHealthBar(string label, int current, int max)
    {
        int barLength = 30;
        double percentage = 0;
        if (max > 0)
        {
            percentage = (double)current / max;
        }
        
        int filled = (int)(barLength * percentage);
        int empty = barLength - filled;
        
        ConsoleColor color = ConsoleColor.Green;
        if (percentage < 0.3)
        {
            color = ConsoleColor.Red;
        }
        else if (percentage < 0.6)
        {
            color = ConsoleColor.Yellow;
        }
        
        Console.Write($"{label}: ");
        Console.ForegroundColor = color;
        for (int i = 0; i < filled; i++)
        {
            Console.Write("█");
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int i = 0; i < empty; i++)
        {
            Console.Write("░");
        }
        Console.ResetColor();
        Console.WriteLine($" {current}/{max}");
    }

    public static void DrawManaBar(string label, int current, int max)
    {
        int barLength = 30;
        double percentage = 0;
        if (max > 0)
        {
            percentage = (double)current / max;
        }
        
        int filled = (int)(barLength * percentage);
        int empty = barLength - filled;
        
        Console.Write($"{label}: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        for (int i = 0; i < filled; i++)
        {
            Console.Write("█");
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int i = 0; i < empty; i++)
        {
            Console.Write("░");
        }
        Console.ResetColor();
        Console.WriteLine($" {current}/{max}");
    }

    public static void DrawTitle(string title)
    {
        Console.WriteLine("============================================================");
        Console.WriteLine(title);
        Console.WriteLine("============================================================");
    }

    public static void PrintColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[OK] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[X] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    public static void PrintWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[!] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    public static void PrintInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[0] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }
}

