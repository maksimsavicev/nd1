using ConsoleApp1;
using ConsoleApp1.Spells;
using Xunit;

namespace ConsoleApp1.Tests;

public class SpellTests
{
    [Fact]
    public void FireballSpell_Cast_DealsDamage()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 50);
        var target = new TestWizard("Target", 100, 50);
        var spell = new FireballSpell();

        // Act
        var result = spell.Cast(caster, target);

        // Assert
        Assert.Equal(Constants.FireballDamage, result.DamageDealt);
        Assert.Equal(100 - Constants.FireballDamage, target.CurrentHealth);
        Assert.Equal(50 - Constants.FireballManaCost, caster.CurrentMana);
        Assert.Contains("Fireball", result.Message);
    }

    [Fact]
    public void FireballSpell_Cast_WithoutEnoughMana_Fails()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 5); // Not enough mana
        var target = new TestWizard("Target", 100, 50);
        var spell = new FireballSpell();
        int initialTargetHealth = target.CurrentHealth;

        // Act
        var result = spell.Cast(caster, target);

        // Assert
        Assert.Equal(0, result.DamageDealt);
        Assert.Equal(initialTargetHealth, target.CurrentHealth);
        Assert.Contains("Not enough mana", result.Message);
    }

    [Fact]
    public void HealSpell_Cast_RestoresHealth()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 50);
        var target = new TestWizard("Target", 100, 50);
        target.TakeDamage(40);
        int healthBeforeHeal = target.CurrentHealth;
        var spell = new HealSpell();

        // Act
        var result = spell.Cast(caster, target);

        // Assert
        Assert.Equal(Constants.HealAmount, result.HealingDone);
        Assert.Equal(healthBeforeHeal + Constants.HealAmount, target.CurrentHealth);
        Assert.Contains("heals", result.Message);
    }

    [Fact]
    public void ShieldSpell_Cast_AddsProtection()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 50);
        var target = new TestWizard("Target", 100, 50);
        var spell = new ShieldSpell();

        // Act
        var result = spell.Cast(caster, target);

        // Assert
        Assert.Equal(Constants.ShieldProtection, result.ProtectionAdded);
        Assert.Equal(Constants.ShieldProtection, target.Protection);
    }

    [Fact]
    public void PoisonSpell_Cast_AppliesPoisonStatus()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 50);
        var target = new TestWizard("Target", 100, 50);
        var spell = new PoisonSpell();

        // Act
        var result = spell.Cast(caster, target);

        // Assert
        Assert.NotNull(result.StatusEffectApplied);
        Assert.Equal(StatusEffectType.Poison, result.StatusEffectApplied.Type);
        Assert.Equal(Constants.PoisonDuration, result.StatusEffectApplied.Duration);
    }

    [Fact]
    public void SilenceSpell_Cast_AppliesSilenceStatus()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 50);
        var target = new TestWizard("Target", 100, 50);
        var spell = new SilenceSpell();

        // Act
        var result = spell.Cast(caster, target);

        // Assert
        Assert.True(result.WasSilenced);
        Assert.True(target.IsSilenced);
        Assert.NotNull(result.StatusEffectApplied);
        Assert.Equal(StatusEffectType.Silence, result.StatusEffectApplied.Type);
    }

    [Fact]
    public void SilenceSpell_PreventsCasting()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 50);
        var target = new TestWizard("Target", 100, 50);
        var silenceSpell = new SilenceSpell();
        var fireballSpell = new FireballSpell();
        
        // Apply silence
        silenceSpell.Cast(caster, target);
        int initialTargetHealth = target.CurrentHealth;

        // Act - Try to cast while silenced
        var result = fireballSpell.Cast(target, caster);

        // Assert
        Assert.Contains("silenced", result.Message);
        Assert.Equal(initialTargetHealth, target.CurrentHealth);
    }

    [Fact]
    public void ManaDrainSpell_Cast_DrainsMana()
    {
        // Arrange
        var caster = new TestWizard("Caster", 100, 50);
        var target = new TestWizard("Target", 100, 50);
        int initialTargetMana = target.CurrentMana;
        var spell = new ManaDrainSpell();

        // Act
        var result = spell.Cast(caster, target);

        // Assert
        Assert.Equal(Constants.ManaDrainAmount, result.ManaDrained);
        Assert.Equal(initialTargetMana - Constants.ManaDrainAmount, target.CurrentMana);
    }
}


