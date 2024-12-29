using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void OnRestartButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }
}
