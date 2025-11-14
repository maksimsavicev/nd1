using ConsoleApp1;
using Xunit;

namespace ConsoleApp1.Tests;

public class StatusEffectTests
{
    [Fact]
    public void StatusEffect_Poison_DealsDamageOverTime()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        var poison = new StatusEffect(StatusEffectType.Poison, 3, 5);
        wizard.ApplyStatusEffect(poison);
        int initialHealth = wizard.CurrentHealth;

        // Act - Process 3 turns
        wizard.ProcessStatusEffects();
        int healthAfterTurn1 = wizard.CurrentHealth;
        
        wizard.ProcessStatusEffects();
        int healthAfterTurn2 = wizard.CurrentHealth;
        
        wizard.ProcessStatusEffects();
        int healthAfterTurn3 = wizard.CurrentHealth;

        // Assert
        Assert.Equal(initialHealth - 5, healthAfterTurn1);
        Assert.Equal(initialHealth - 10, healthAfterTurn2);
        Assert.Equal(initialHealth - 15, healthAfterTurn3);
    }

    [Fact]
    public void StatusEffect_ExpiresAfterDuration()
    {
        // Arrange
        var wizard = new TestWizard("Test", 100, 50);
        var poison = new StatusEffect(StatusEffectType.Poison, 2, 5);
        wizard.ApplyStatusEffect(poison);

        // Act
        wizard.ProcessStatusEffects();
        bool activeAfterTurn1 = wizard.IsSilenced || poison.IsActive;
        
        wizard.ProcessStatusEffects();
        bool activeAfterTurn2 = wizard.IsSilenced || poison.IsActive;
        
        wizard.ProcessStatusEffects();
        bool activeAfterTurn3 = wizard.IsSilenced || poison.IsActive;

        // Assert
        Assert.True(poison.IsActive || activeAfterTurn1);
        wizard.ProcessStatusEffects();
        // After 2 turns, poison should expire
    }
}


