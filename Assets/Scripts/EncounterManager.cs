using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager
{
    public static EncounterManager Instance { get; private set; }

    public EncounterManager()
    {
        Instance = this;
    }
}
