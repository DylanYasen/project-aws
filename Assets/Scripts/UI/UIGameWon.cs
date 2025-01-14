using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameWon : MonoBehaviour
{
    public void OnButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }
}