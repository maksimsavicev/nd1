namespace ConsoleApp1.Spells;

public class PoisonSpell : Spell
{
    public PoisonSpell() : base("Poison", Constants.PoisonManaCost,
        $"Poisons target for {Constants.PoisonDuration} turns ({Constants.PoisonDamage} damage per turn). Costs {Constants.PoisonManaCost} mana.")
    {
    }

    protected override void ExecuteEffect(Wizard caster, Wizard target, SpellResult result)
    {
        var poisonEffect = new StatusEffect(StatusEffectType.Poison, Constants.PoisonDuration, Constants.PoisonDamage);
        target.ApplyStatusEffect(poisonEffect);
        result.StatusEffectApplied = poisonEffect;
        result.Message = $"{caster.Name} poisons {target.Name} for {Constants.PoisonDuration} turns!";
    }
}

public class SilenceSpell : Spell
{
    public SilenceSpell() : base("Silence", Constants.SilenceManaCost,
        $"Silences target for {Constants.SilenceDuration} turns (cannot cast spells). Costs {Constants.SilenceManaCost} mana.")
    {
    }

    protected override void ExecuteEffect(Wizard caster, Wizard target, SpellResult result)
    {
        var silenceEffect = new StatusEffect(StatusEffectType.Silence, Constants.SilenceDuration);
        target.ApplyStatusEffect(silenceEffect);
        result.StatusEffectApplied = silenceEffect;
        result.WasSilenced = true;
        result.Message = $"{caster.Name} silences {target.Name} for {Constants.SilenceDuration} turns!";
    }
}

