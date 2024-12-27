using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
    public void OnTurnEndClicked()
    {
        TurnManager.Instance.EndPlayerTurn();
    }
}
