using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Mystery Event", menuName = "Events/Mystery Event")]
public class MysteryEvent : ScriptableObject
{
    public string eventName;
    [TextArea(3, 10)]
    public string description;

    public enum EventType
    {
        Positive,   // Good events (heal, gain gold, etc)
        Negative,   // Bad events (damage, lose gold, etc)
        Mixed      // Events that could be good or bad based on player choice
    }

    public EventType eventType;
    public Sprite eventImage;

    [System.Serializable]
    public class EventChoice
    {
        public string choiceText;
        [TextArea(2, 5)]
        public string resultText;

        [SerializeReference]
        public List<StatusEffect> effects = new();
    }

    public List<EventChoice> choices = new();

    public virtual void OnEventStart()
    {
        // Override this to do any setup when the event begins
        Debug.Log($"Mystery Event Started");


    }

    public virtual void ExecuteChoice(int choiceIndex)
    {
        if (choiceIndex < 0 || choiceIndex >= choices.Count) return;

        var choice = choices[choiceIndex];

        for (int i = choice.effects.Count - 1; i >= 0; i--)
        {
            StatusEffectManager.Instance.AddEffect(Player.Instance, choice.effects[i]);
        }
    }
}