using UnityEngine;

public class Player : Unit
{
    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}