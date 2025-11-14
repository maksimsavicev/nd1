using ConsoleApp1;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        var player = new PlayerWizard("Player");
        var ai = new AIWizard("Dark Mage");
        
        var game = new Game(player, ai);
        game.Start();
    }
}
