namespace ConsoleApp1;

public class AIWizard : Wizard
{
    public AIWizard(string name) : base(name, Constants.DefaultHealth, Constants.DefaultMana)
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
        if (IsSilenced) return null;

        List<ISpell> available = new List<ISpell>();
        foreach (ISpell spell in Spells)
        {
            if (CurrentMana >= spell.ManaCost)
            {
                available.Add(spell);
            }
        }

        if (available.Count == 0) return null;

        return available[Random.Shared.Next(available.Count)];
    }
}

