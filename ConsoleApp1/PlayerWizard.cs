namespace ConsoleApp1;

public class PlayerWizard : Wizard
{
    public PlayerWizard(string name) : base(name, Constants.DefaultHealth, Constants.DefaultMana)
    {
        InitializeSpells();
    }

    private void InitializeSpells()
    {
        Spells = new List<ISpell>
        {
            new Spells.FireballSpell(),
            new Spells.IceBoltSpell(),
            new Spells.LightningSpell(),
            new Spells.HealSpell(),
            new Spells.PoisonSpell(),
            new Spells.SilenceSpell()
        };
    }

    public ISpell ChooseSpell(Wizard opponent)
    {
        DisplaySpellMenu();
        return GetSpellChoice();
    }

    private void DisplaySpellMenu()
    {
        Console.WriteLine();
        UIHelper.PrintColored($"{Name}'s Turn\n", ConsoleColor.Cyan);
        
        if (IsSilenced)
        {
            UIHelper.PrintWarning("You are SILENCED and cannot cast spells!");
        }
        
        Console.WriteLine();
        Console.WriteLine("Available Spells:");
        Console.WriteLine();
        
        for (int i = 0; i < Spells.Count; i++)
        {
            ISpell spell = Spells[i];
            bool canCast = CurrentMana >= spell.ManaCost && !IsSilenced;
            
            Console.Write($"{i + 1}. ");
            if (canCast)
            {
                UIHelper.PrintColored("[OK] ", ConsoleColor.Green);
            }
            else
            {
                UIHelper.PrintColored("[NO] ", ConsoleColor.Red);
            }
            
            if (canCast)
            {
                Console.Write($"{spell.Name} ({spell.ManaCost} mana) - ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{spell.Name} ({spell.ManaCost} mana) - ");
                Console.ResetColor();
            }
            Console.WriteLine(spell.Description);
        }
        
        Console.WriteLine();
        UIHelper.PrintInfo("Skip turn (restore 5 mana)");
        Console.WriteLine();
    }

    private ISpell GetSpellChoice()
    {
        while (true)
        {
            Console.Write($"Choose a spell (1-{Spells.Count}), 0 to skip, or Q to quit: ");
            string input = Console.ReadLine();
            
            if (input?.ToLower() == "q" || input?.ToLower() == "й")
            {
                Console.Clear();
                Console.WriteLine("============================================================");
                Console.WriteLine("GAME EXIT");
                Console.WriteLine("============================================================");
                Console.WriteLine();
                Console.WriteLine("You have exited the game.");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            
            if (int.TryParse(input, out int choice))
            {
                if (choice == 0)
                {
                    RestoreMana(5);
                    UIHelper.PrintSuccess("You skip your turn and restore 5 mana.");
                    System.Threading.Thread.Sleep(1000);
                    return null;
                }
                
                if (choice >= 1 && choice <= Spells.Count)
                {
                    ISpell spell = Spells[choice - 1];
                    
                    if (IsSilenced)
                    {
                        UIHelper.PrintError("You are silenced! Cannot cast spells.");
                        System.Threading.Thread.Sleep(1200);
                        continue;
                    }
                    
                    if (CurrentMana < spell.ManaCost)
                    {
                        UIHelper.PrintError($"Not enough mana! You need {spell.ManaCost} mana, but you have {CurrentMana}.");
                        System.Threading.Thread.Sleep(1200);
                        continue;
                    }
                    
                    return spell;
                }
            }
            
            UIHelper.PrintError("Invalid choice. Please try again.");
            System.Threading.Thread.Sleep(800);
            Console.WriteLine();
        }
    }
}

