using TMPro;
using UnityEngine;

public class LootUI : MonoBehaviour
{
    public GameObject lootEntryPrefab;

    public TMP_Text goldText;

    public void ShowLoot(int gold)
    {
        goldText.text = $"{gold}";
    }

    public void OnContinuePressed()
    {
        GameManager.Instance.EnterMapScene();
    }
}
