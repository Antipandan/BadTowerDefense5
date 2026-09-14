using System;

public class HealthCounter : UICounter
{
    private void Awake()
    {
        CheckReferences();
        SubscribeEvents();
    }
    
    private void SubscribeEvents()
    {
        gameEvents.onLivesLost += LivesLost;
    }

    public void LivesLost(uint livesLost)
    {
        if (Economy.Instance is null) return;
        Economy.Instance.LoseHealth(livesLost);
        ConfigureUIText();
    }

    private void ConfigureUIText()
    {
        if (UIText is not null)
        {
            UIText.text = $"{Economy.Instance.CurrentHealth}";
        }
    }
}