using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MaintanceUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private float stepDuration = 1f;
    [SerializeField] private int totalHeal = 20;

    private readonly string[] maintenanceMessages = new string[]
    {
        "Garbage Collecting...",
        "Redeploying Assets...",
        "Defragmenting Memory...",
        "Downloading More RAM...",
        "Updating Neural Networks...",
        "Clearing Cache...",
        "Running Unit Tests...",
        "Optimizing Algorithms...",
        "Rebooting Systems...",
        "Installing Security Patches..."
    };


    public void Start()
    {
        StartCoroutine(PerformMaintenance());

        continueButton.gameObject.SetActive(false);
        continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    static float[] progressValues = new float[] { 0.15f, 0.35f, 0.76f, 1f };

    private IEnumerator PerformMaintenance()
    {
        progressBar.fillAmount = 0f;

        int healPerStep = totalHeal / 4;

        // Four steps of maintenance
        for (int step = 0; step < 4; step++)
        {
            statusText.text = maintenanceMessages[Random.Range(0, maintenanceMessages.Length)];

            progressBar.fillAmount = progressValues[step];

            CombatManager.Instance.ApplyHealing(Player.Instance, healPerStep);

            if (step < 3)
            {
                yield return new WaitForSeconds(stepDuration);
            }
        }

        statusText.text = "Maintenance Complete";

        progressBar.fillAmount = 1f;

        continueButton.gameObject.SetActive(true);
    }

    private void OnContinueButtonClicked()
    {
        GameManager.Instance.MaintanceComplete();
    }
}
