using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TMP_Text cardNameText;
    public TMP_Text descriptionText;
    public Image cardArtImage;

    private Card card;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    public bool IsTargeting { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = transform.localScale;
    }

    public void Setup(Card cardData)
    {
        card = cardData;
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        if (card.cardArt != null)
        {
            cardArtImage.sprite = card.cardArt;
        }
    }

    public void OnCardClicked()
    {
        // Debug.Log($"Playing card: {card.cardName}");

        // GameManager.Instance.SelectCard(card);

        // transform.position += new Vector3(0, 20, 0); // Raise card
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // @todo: smooth this out with tweening
        transform.localScale = originalScale * 1.2f; // Enlarge card
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // Reset scale
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
        canvasGroup.blocksRaycasts = false; // Make card ignore raycasts while dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position; // Follow the cursor
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // reenable raycasts

        Debug.Log($"Dropped card: {card.cardName} on {eventData.pointerEnter?.name}");

        // Check if the card was dropped on a valid target
        if (eventData.pointerEnter == null || !eventData.pointerEnter.CompareTag("PlayZone"))
        {
            rectTransform.position = originalPosition; // Snap back to original position
        }
        else
        {
            if (card.requiresTarget)
            {
                EnterTargetingMode();
            }
            else
            {
                Player.Instance.PlayCard(card);

                // @todo: pool
                Destroy(gameObject);
            }
        }
    }

    public void EnterTargetingMode()
    {
        IsTargeting = true;
    }

    public void ExitTargetingMode()
    {
        IsTargeting = false;
        TargetingArrow.Instance.HideArrow();
    }

    private void Update()
    {
        if (IsTargeting)
        {
            UpdateTargeting();
        }
    }

    public void UpdateTargeting()
    {
        Vector3 start = Camera.main.ScreenToWorldPoint(rectTransform.position);
        start.z = 0;

        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0; // Ensure it's on the same plane

        TargetingArrow.Instance.UpdateArrow(start, currentMousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            rectTransform.position = originalPosition; // Snap back to original position
            ExitTargetingMode();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // case a ray from mouse position to see if hit enemy
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                ExitTargetingMode();
                Player.Instance.PlayCard(card, hit.collider.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
