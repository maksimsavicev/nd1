namespace ConsoleApp1.Spells;

public class DamageSpell : Spell
{
    private int damage;

    public DamageSpell(string name, int manaCost, int damageAmount) 
        : base(name, manaCost, $"Deals {damageAmount} damage. Costs {manaCost} mana.")
    {
        damage = damageAmount;
    }

    protected override void ExecuteEffect(Wizard caster, Wizard target, SpellResult result)
    {
        result.DamageDealt = target.TakeDamage(damage);
        result.Message = $"{caster.Name} casts {Name} on {target.Name} for {result.DamageDealt} damage!";
    }
}

public class FireballSpell : DamageSpell
{
    public FireballSpell() : base("Fireball", Constants.FireballManaCost, Constants.FireballDamage)
    {
    }
}

public class IceBoltSpell : DamageSpell
{
    public IceBoltSpell() : base("Ice Bolt", Constants.IceBoltManaCost, Constants.IceBoltDamage)
    {
    }
}

public class LightningSpell : DamageSpell
{
    public LightningSpell() : base("Lightning", Constants.LightningManaCost, Constants.LightningDamage)
    {
    }
}

