using ConsoleApp1;
using ConsoleApp1.Factories;
using ConsoleApp1.Spells;
using Xunit;

namespace ConsoleApp1.Tests;

public class SpellFactoryTests
{
    [Fact]
    public void SpellFactory_CreateSpell_Fireball_ReturnsFireballSpell()
    {
        // Act
        var spell = SpellFactory.CreateSpell("fireball");

        // Assert
        Assert.IsType<FireballSpell>(spell);
        Assert.Equal("Fireball", spell.Name);
    }

    [Fact]
    public void SpellFactory_CreateSpell_Heal_ReturnsHealSpell()
    {
        // Act
        var spell = SpellFactory.CreateSpell("heal");

        // Assert
        Assert.IsType<HealSpell>(spell);
        Assert.Equal("Heal", spell.Name);
    }

    [Fact]
    public void SpellFactory_CreateSpell_UnknownSpell_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => SpellFactory.CreateSpell("unknown"));
    }

    [Fact]
    public void SpellFactory_CreateDefaultSpellSet_ReturnsAllSpells()
    {
        // Act
        var spells = SpellFactory.CreateDefaultSpellSet();

        // Assert
        Assert.Equal(7, spells.Count);
        Assert.Contains(spells, s => s is FireballSpell);
        Assert.Contains(spells, s => s is HealSpell);
        Assert.Contains(spells, s => s is ShieldSpell);
        Assert.Contains(spells, s => s is PoisonSpell);
        Assert.Contains(spells, s => s is SilenceSpell);
    }
}


