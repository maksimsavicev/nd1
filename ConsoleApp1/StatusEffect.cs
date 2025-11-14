namespace ConsoleApp1;

public enum StatusEffectType
{
    None,
    Poison,
    Silence
}

public class StatusEffect
{
    public StatusEffectType Type { get; }
    public int Duration { get; private set; }
    public int Damage { get; }

    public StatusEffect(StatusEffectType type, int duration, int damage = 0)
    {
        Type = type;
        Duration = duration;
        Damage = damage;
    }

    public bool IsActive => Duration > 0;

    public void DecreaseDuration()
    {
        if (Duration > 0)
        {
            Duration--;
        }
    }

    public int ApplyEffect()
    {
        if (Type == StatusEffectType.Poison && IsActive)
        {
            DecreaseDuration();
            return Damage;
        }
        return 0;
    }
}


