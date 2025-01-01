using UnityEngine;

public class MaintanceUI : MonoBehaviour
{
    public void OnMaintenanceButtonPress()
    {
        GameManager.Instance.Maintance();
    }
}
