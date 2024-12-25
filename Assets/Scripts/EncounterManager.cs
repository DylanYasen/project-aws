using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager
{
    public static EncounterManager Instance { get; private set; }

    public EncounterManager()
    {
        Instance = this;
    }

    public void StartCombatEncounter()
    {
        Debug.Log("Encounter started!");

        SceneManager.LoadScene("CombatScene"); // @todo: fix hardcode
    }

    public void OnCombatEncounterEnd()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void StartRestSite()
    {
        Debug.Log("Starting rest site encounter.");
        // Open rest site UI and allow player to heal or upgrade
    }

    public void StartTreasure()
    {
        Debug.Log("Starting treasure encounter.");
        // Reward the player with a relic or powerful card
    }

    public void StartStore()
    {
        Debug.Log("Starting store encounter.");
        // Open the store UI for purchasing items
    }

    public void StartMysteryEvent()
    {
        Debug.Log("Starting mystery event.");
        // Trigger a random event with unpredictable outcomes
    }


}
