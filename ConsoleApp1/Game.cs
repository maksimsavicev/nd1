namespace ConsoleApp1;

public class Game
{
    private PlayerWizard player;
    private AIWizard ai;
    private int turnNumber;
    private List<string> actionLog;
    private List<List<string>> turnSummaries;

    public Game(PlayerWizard playerWizard, AIWizard aiWizard)
    {
        player = playerWizard;
        ai = aiWizard;
        turnNumber = 1;
        actionLog = new List<string>();
        turnSummaries = new List<List<string>>();
    }

    public void Start()
    {
        Console.Clear();

        while (player.CurrentHealth > 0 && ai.CurrentHealth > 0)
        {
            PlayTurn();
            turnNumber++;
        }

        DisplayGameOver();
    }

    private void PlayTurn()
    {
        List<string> currentTurnActions = new List<string>();
        
        if (actionLog.Count > 10)
        {
            actionLog.RemoveRange(0, actionLog.Count - 10);
        }

        DisplayTurnHeader();
        ProcessStatusEffectsWithMessage(player, currentTurnActions);
        ProcessStatusEffectsWithMessage(ai, currentTurnActions);

        DisplayBattleStatus();

        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------");
        DisplayActionLog();
        Console.WriteLine();
        ExecuteTurn(player, ai, true, currentTurnActions);

        if (ai.CurrentHealth <= 0)
        {
            turnSummaries.Add(currentTurnActions);
            return;
        }

        System.Threading.Thread.Sleep(1500);
        DisplayTurnHeader();
        DisplayBattleStatus();
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------");
        DisplayActionLog();
        Console.WriteLine();
        ExecuteTurn(ai, player, false, currentTurnActions);

        turnSummaries.Add(currentTurnActions);
        System.Threading.Thread.Sleep(1500);
    }

    private void ProcessStatusEffectsWithMessage(Wizard wizard, List<string> turnActions)
    {
        int healthBefore = wizard.CurrentHealth;
        wizard.ProcessStatusEffects();
        int healthAfter = wizard.CurrentHealth;
        int damage = healthBefore - healthAfter;

        if (damage > 0)
        {
            UIHelper.PrintWarning($"{wizard.Name} takes {damage} damage from poison!");
            string action = $"{wizard.Name} lost {damage} HP from poison";
            actionLog.Add(action);
            turnActions.Add(action);
        }
    }

    private void ExecuteTurn(Wizard caster, Wizard target, bool isPlayer, List<string> turnActions)
    {
        ISpell spell = isPlayer ? player.ChooseSpell(target) : ai.ChooseSpell(target);
        
        if (spell == null)
        {
            if (isPlayer)
            {
                UIHelper.PrintInfo("You skipped your turn.");
            }
            else
            {
                caster.RestoreMana(5);
            }
            string action = $"{caster.Name} skipped turn (+5 MP)";
            actionLog.Add(action);
            turnActions.Add(action);
            System.Threading.Thread.Sleep(1000);
            return;
        }

        Console.WriteLine();
        string spellAction = $"{caster.Name} used {spell.Name}";
        string message;
        ConsoleColor color;
        if (isPlayer)
        {
            message = $"{caster.Name} casts {spell.Name}!\n";
            color = ConsoleColor.Cyan;
        }
        else
        {
            message = $"{caster.Name} is casting {spell.Name}...\n";
            color = ConsoleColor.Magenta;
        }
        UIHelper.PrintColored(message, color);
        actionLog.Add(spellAction);
        turnActions.Add(spellAction);

        System.Threading.Thread.Sleep(800);

        Wizard actualTarget = spell.TargetsSelf ? caster : target;
        SpellResult result = spell.Cast(caster, actualTarget);
        ProcessSpellResult(result, target, actualTarget, turnActions);

        System.Threading.Thread.Sleep(1000);
        DisplayTurnHeader();
        DisplayBattleStatus();
    }

    private void ProcessSpellResult(SpellResult result, Wizard target, Wizard actualTarget, List<string> turnActions)
    {
        if (result.DamageDealt > 0)
        {
            UIHelper.PrintError($"{result.DamageDealt} damage dealt to {target.Name}!");
            string action = $"{target.Name}: -{result.DamageDealt} HP";
            actionLog.Add($"  -> {action}");
            turnActions.Add($"  -> {action}");
        }

        if (result.HealingDone > 0)
        {
            UIHelper.PrintSuccess($"{result.HealingDone} health restored to {actualTarget.Name}!");
            string action = $"{actualTarget.Name}: +{result.HealingDone} HP";
            actionLog.Add($"  -> {action}");
            turnActions.Add($"  -> {action}");
        }

        if (result.StatusEffectApplied != null)
        {
            UIHelper.PrintWarning($"{result.StatusEffectApplied.Type} applied to {target.Name} " +
                                 $"(Duration: {result.StatusEffectApplied.Duration} turns)");
            string action = $"{target.Name}: {result.StatusEffectApplied.Type} ({result.StatusEffectApplied.Duration} turns)";
            actionLog.Add($"  -> {action}");
            turnActions.Add($"  -> {action}");
        }

        if (result.WasSilenced)
        {
            UIHelper.PrintWarning($"{target.Name} is now SILENCED!");
            string action = $"{target.Name}: SILENCED";
            actionLog.Add($"  -> {action}");
            turnActions.Add($"  -> {action}");
        }
    }

    private void DisplayTurnHeader()
    {
        Console.Clear();
        Console.WriteLine($"TURN {turnNumber}");
        Console.WriteLine("============================================================");
    }

    private void DisplayActionLog()
    {
        if (actionLog.Count == 0) return;
        
        Console.WriteLine("LAST ACTIONS:");
        Console.WriteLine();
        
        int startIndex = 0;
        if (actionLog.Count > 5)
        {
            startIndex = actionLog.Count - 5;
        }
        for (int i = startIndex; i < actionLog.Count; i++)
        {
            PrintAction(actionLog[i]);
        }
    }
    
    private void PrintAction(string action)
    {
        if (action.Contains("used"))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  > ");
            Console.ResetColor();
            Console.WriteLine(action);
        }
        else if (action.StartsWith("  ->"))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"    {action.Substring(4).TrimStart()}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("  ! ");
            Console.ResetColor();
            Console.WriteLine(action);
        }
    }

    private void DisplayBattleStatus()
    {
        Console.WriteLine();
        Console.WriteLine("BATTLE STATUS");
        Console.WriteLine("============================================================");
        Console.WriteLine();
        
        DisplayWizardStatus("YOU", player, ConsoleColor.Green);
        Console.WriteLine();
        DisplayWizardStatus("ENEMY", ai, ConsoleColor.Red);
        Console.WriteLine();
    }

    private void DisplayWizardStatus(string label, Wizard wizard, ConsoleColor color)
    {
        UIHelper.PrintColored($"{label}: {wizard.Name}\n", color);
        UIHelper.DrawHealthBar("Health", wizard.CurrentHealth, wizard.MaxHealth);
        UIHelper.DrawManaBar("Mana", wizard.CurrentMana, wizard.MaxMana);
        if (wizard.IsSilenced)
        {
            UIHelper.PrintWarning("SILENCED - Cannot cast spells!");
        }
    }

    private void DisplayGameOver()
    {
        Console.Clear();
        UIHelper.DrawTitle("GAME OVER");
        Console.WriteLine();

        if (player.CurrentHealth <= 0)
        {
            UIHelper.PrintColored("DEFEAT\n", ConsoleColor.Red);
            Console.WriteLine($"{ai.Name} WINS!");
            Console.WriteLine($"{player.Name} has been defeated!");
        }
        else
        {
            UIHelper.PrintColored("VICTORY\n", ConsoleColor.Green);
            Console.WriteLine($"{player.Name} WINS!");
            Console.WriteLine($"{ai.Name} has been defeated!");
        }

        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine($"Total turns: {turnNumber}");
        Console.WriteLine();
        Console.WriteLine("Press any key to view turn summary...");
        Console.ReadKey();
        
        DisplayTurnSummary();
    }
    
    private void DisplayTurnSummary()
    {
        Console.Clear();
        UIHelper.DrawTitle("TURN SUMMARY");
        Console.WriteLine();
        
        for (int i = 0; i < turnSummaries.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"TURN {i + 1}:");
            Console.ResetColor();
            
            if (turnSummaries[i].Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  (No actions)");
                Console.ResetColor();
            }
            else
            {
                foreach (string action in turnSummaries[i])
                {
                    PrintAction(action);
                }
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

