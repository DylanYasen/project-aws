using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitGameUI : MonoBehaviour
{
    private Button exitButton;

    private void Awake()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveListener(QuitGame);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
} 