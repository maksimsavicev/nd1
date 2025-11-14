namespace ConsoleApp1;

public class Wizard
{
    private int _health;
    private int _mana;
    private StatusEffect? _statusEffect;

    public string Name { get; protected set; }
    public int MaxHealth { get; protected set; }
    public int MaxMana { get; protected set; }
    
    public int CurrentHealth 
    { 
        get { return _health; }
        private set 
        { 
            if (value < 0) value = 0;
            if (value > MaxHealth) value = MaxHealth;
            _health = value;
        }
    }
    
    public int CurrentMana 
    { 
        get { return _mana; }
        private set 
        { 
            if (value < 0) value = 0;
            if (value > MaxMana) value = MaxMana;
            _mana = value;
        }
    }
    
    public bool IsSilenced
    {
        get
        {
            if (_statusEffect == null) return false;
            if (_statusEffect.Type != StatusEffectType.Silence) return false;
            return _statusEffect.IsActive;
        }
    }
    public List<ISpell> Spells { get; protected set; } = new();

    protected Wizard(string name, int health, int mana)
    {
        Name = name;
        MaxHealth = health;
        MaxMana = mana;
        CurrentHealth = health;
        CurrentMana = mana;
    }

    public int TakeDamage(int damage)
    {
        if (damage < 0) damage = 0;
        CurrentHealth = CurrentHealth - damage;
        return damage;
    }

    public int Heal(int amount)
    {
        int healAmount = amount;
        if (CurrentHealth + healAmount > MaxHealth)
        {
            healAmount = MaxHealth - CurrentHealth;
        }
        CurrentHealth = CurrentHealth + healAmount;
        return healAmount;
    }

    public void UseMana(int amount)
    {
        CurrentMana = CurrentMana - amount;
    }

    public void RestoreMana(int amount)
    {
        int newMana = CurrentMana + amount;
        if (newMana > MaxMana) newMana = MaxMana;
        CurrentMana = newMana;
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        _statusEffect = effect;
    }

    public void ProcessStatusEffects()
    {
        if (_statusEffect == null)
        {
            return;
        }

        if (!_statusEffect.IsActive)
        {
            _statusEffect = null;
            return;
        }

        if (_statusEffect.Type == StatusEffectType.Poison)
        {
            int poisonDamage = _statusEffect.ApplyEffect();
            if (poisonDamage > 0)
            {
                TakeDamage(poisonDamage);
            }
        }

        if (_statusEffect.Type == StatusEffectType.Silence)
        {
            _statusEffect.DecreaseDuration();
        }

        if (!_statusEffect.IsActive)
        {
            _statusEffect = null;
        }
    }

}

