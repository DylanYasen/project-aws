using UnityEngine;

public class EnergyBarUI : MonoBehaviour
{
    public TMPro.TMP_Text energyNumText;

    public void SetEnergy(int energy)
    {
        energyNumText.text = energy.ToString();
    }

}
