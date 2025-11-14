namespace ConsoleApp1.Factories;

public static class SpellFactory
{
    public static ISpell CreateSpell(string spellName)
    {
        return spellName.ToLower() switch
        {
            "fireball" => new Spells.FireballSpell(),
            "lightning" => new Spells.LightningSpell(),
            "icebolt" or "ice bolt" => new Spells.IceBoltSpell(),
            "heal" => new Spells.HealSpell(),
            "poison" => new Spells.PoisonSpell(),
            "silence" => new Spells.SilenceSpell(),
            _ => throw new ArgumentException($"Unknown spell: {spellName}")
        };
    }

    public static List<ISpell> CreateDefaultSpellSet()
    {
        return new List<ISpell>
        {
            new Spells.FireballSpell(),
            new Spells.LightningSpell(),
            new Spells.IceBoltSpell(),
            new Spells.HealSpell(),
            new Spells.PoisonSpell(),
            new Spells.SilenceSpell()
        };
    }
}

