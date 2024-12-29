using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        GameManager.Instance.StartGame();
    }
}
