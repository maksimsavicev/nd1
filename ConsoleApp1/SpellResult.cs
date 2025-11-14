namespace ConsoleApp1;

public class SpellResult
{
    public int DamageDealt { get; set; }
    public int HealingDone { get; set; }
    public StatusEffect? StatusEffectApplied { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool WasSilenced { get; set; }
}

