namespace ConsoleApp1.Spells;

public class HealSpell : Spell
{
    private int healAmount;

    public HealSpell() : base("Heal", Constants.HealManaCost,
        $"Restores {Constants.HealAmount} health. Costs {Constants.HealManaCost} mana.", targetsSelf: true)
    {
        healAmount = Constants.HealAmount;
    }

    protected override void ExecuteEffect(Wizard caster, Wizard target, SpellResult result)
    {
        result.HealingDone = target.Heal(healAmount);
        result.Message = $"{caster.Name} heals {target.Name} for {result.HealingDone} health!";
    }
}

