using ConsoleApp1;
using ConsoleApp1.Spells;
using Xunit;

namespace ConsoleApp1.Tests;

public class WizardTests
{
    [Fact]
    public void Wizard_TakeDamage_ReducesHealth()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        int initialHealth = wizard.CurrentHealth;

        // Act
        int damageDealt = wizard.TakeDamage(25);

        // Assert
        Assert.Equal(25, damageDealt);
        Assert.Equal(initialHealth - 25, wizard.CurrentHealth);
    }

    [Fact]
    public void Wizard_TakeDamage_WithProtection_ReducesDamage()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        wizard.AddProtection(10);
        int initialHealth = wizard.CurrentHealth;

        // Act
        int damageDealt = wizard.TakeDamage(25);

        // Assert
        Assert.Equal(15, damageDealt); // 25 - 10 protection
        Assert.Equal(initialHealth - 15, wizard.CurrentHealth);
    }

    [Fact]
    public void Wizard_Heal_RestoresHealth()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        wizard.TakeDamage(30);
        int healthBeforeHeal = wizard.CurrentHealth;

        // Act
        int healingDone = wizard.Heal(20);

        // Assert
        Assert.Equal(20, healingDone);
        Assert.Equal(healthBeforeHeal + 20, wizard.CurrentHealth);
    }

    [Fact]
    public void Wizard_Heal_DoesNotExceedMaxHealth()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        wizard.TakeDamage(10);

        // Act
        int healingDone = wizard.Heal(50);

        // Assert
        Assert.Equal(10, healingDone); // Can only heal 10 to reach max
        Assert.Equal(100, wizard.CurrentHealth);
    }

    [Fact]
    public void Wizard_UseMana_ReducesMana()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        int initialMana = wizard.CurrentMana;

        // Act
        wizard.UseMana(15);

        // Assert
        Assert.Equal(initialMana - 15, wizard.CurrentMana);
    }

    [Fact]
    public void Wizard_DrainMana_ReducesTargetMana()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        int initialMana = wizard.CurrentMana;

        // Act
        int drained = wizard.DrainMana(20);

        // Assert
        Assert.Equal(20, drained);
        Assert.Equal(initialMana - 20, wizard.CurrentMana);
    }

    [Fact]
    public void Wizard_ProcessStatusEffects_PoisonDealsDamage()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        var poison = new StatusEffect(StatusEffectType.Poison, 2, 5);
        wizard.ApplyStatusEffect(poison);
        int initialHealth = wizard.CurrentHealth;

        // Act
        wizard.ProcessStatusEffects();

        // Assert
        Assert.Equal(initialHealth - 5, wizard.CurrentHealth);
        Assert.True(poison.IsActive);
    }

    [Fact]
    public void Wizard_ProcessStatusEffects_SilencePreventsCasting()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        var silence = new StatusEffect(StatusEffectType.Silence, 2);
        wizard.ApplyStatusEffect(silence);

        // Act & Assert
        Assert.True(wizard.IsSilenced);
    }

    [Fact]
    public void Wizard_IsAlive_ReturnsFalseWhenHealthZero()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);

        // Act
        wizard.TakeDamage(100);

        // Assert
        Assert.False(wizard.IsAlive);
        Assert.Equal(0, wizard.CurrentHealth);
    }
}

// Helper class for testing
public class TestWizard : Wizard
{
    public TestWizard(string name, int health, int mana) : base(name, health, mana)
    {
    }

    public override ISpell? ChooseSpell(Wizard opponent)
    {
        return null;
    }
}


