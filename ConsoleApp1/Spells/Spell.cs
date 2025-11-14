namespace ConsoleApp1.Spells;

public abstract class Spell : ISpell
{
    public string Name { get; protected set; } = string.Empty;
    public int ManaCost { get; protected set; }
    public string Description { get; protected set; } = string.Empty;
    public virtual bool TargetsSelf { get; protected set; } = false;

    protected Spell(string name, int manaCost, string description, bool targetsSelf = false)
    {
        Name = name;
        ManaCost = manaCost;
        Description = description;
        TargetsSelf = targetsSelf;
    }

    public SpellResult Cast(Wizard caster, Wizard target)
    {
        var result = new SpellResult();
        
        if (!CanCast(caster))
        {
            result.Message = caster.IsSilenced 
                ? $"{caster.Name} is silenced and cannot cast spells!" 
                : "Not enough mana!";
            return result;
        }

        ConsumeMana(caster);
        ExecuteEffect(caster, target, result);
        return result;
    }

    protected abstract void ExecuteEffect(Wizard caster, Wizard target, SpellResult result);

    protected bool CanCast(Wizard caster)
    {
        return caster.CurrentMana >= ManaCost && !caster.IsSilenced;
    }

    protected void ConsumeMana(Wizard caster)
    {
        caster.UseMana(ManaCost);
    }
}

