namespace ConsoleApp1;

public interface ISpell
{
    string Name { get; }
    int ManaCost { get; }
    string Description { get; }
    bool TargetsSelf { get; }
    SpellResult Cast(Wizard caster, Wizard target);
}

