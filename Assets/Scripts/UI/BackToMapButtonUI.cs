using UnityEngine;
using UnityEngine.UI;

public class BackToMapButtonUI : MonoBehaviour
{
    Button backToMapButton;

    void Start()
    {
        backToMapButton = GetComponent<Button>();
        backToMapButton.onClick.AddListener(OnBackToMapClicked);
    }

    public void OnBackToMapClicked()
    {
        GameManager.Instance.EnterMapScene();
    }
}
