using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MysteryEventManager
{
    public static MysteryEventManager Instance { get; private set; }

    private List<MysteryEvent> allEvents = new();
    private Dictionary<MysteryEvent.EventType, List<MysteryEvent>> eventsByType = new();

    public MysteryEvent currentEvent;

    public List<MysteryEvent> seenEvents = new();

    public MysteryEventManager()
    {
        Instance = this;
    }

    public async void LoadResources()
    {
        var eventLoadHandle = Addressables.LoadAssetsAsync<MysteryEvent>("MysteryEvents", mysteryEvent =>
        {
            Debug.Log($"Loaded mystery event: {mysteryEvent.name}");

            allEvents.Add(mysteryEvent);

            if (!eventsByType.ContainsKey(mysteryEvent.eventType))
            {
                eventsByType[mysteryEvent.eventType] = new List<MysteryEvent>();
            }
            eventsByType[mysteryEvent.eventType].Add(mysteryEvent);
        });

        await eventLoadHandle.Task;

        if (eventLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Successfully loaded all mystery events: {allEvents.Count} total");
        }
        else
        {
            Debug.LogError("Failed to load mystery events");
        }
    }

    public MysteryEvent GetRandomEvent()
    {
        if (allEvents.Count == 0) return null;

        // todo: improve 
        MysteryEvent randomEvent = allEvents[Random.Range(0, allEvents.Count)];
        if (seenEvents.Contains(randomEvent))
        {
            return GetRandomEvent();
        }

        seenEvents.Add(randomEvent);
        return randomEvent;
    }

    public MysteryEvent GetRandomEventOfType(MysteryEvent.EventType type)
    {
        if (!eventsByType.ContainsKey(type) || eventsByType[type].Count == 0) return null;
        return eventsByType[type][Random.Range(0, eventsByType[type].Count)];
    }

    public void StartEvent(MysteryEvent mysteryEvent)
    {
        currentEvent = mysteryEvent;
        mysteryEvent.OnEventStart();

        if (mysteryEvent.choices.Count == 1)
        {
            MakeChoice(0);
        }

        // TODO: Show UI for mystery event
        Debug.Log($"Starting mystery event: {mysteryEvent.eventName}");
    }

    public void MakeChoice(int choiceIndex)
    {
        if (currentEvent == null) return;

        currentEvent.ExecuteChoice(choiceIndex);

        // TODO: Show result UI
        // TODO: Handle event completion

        currentEvent = null;
    }
}