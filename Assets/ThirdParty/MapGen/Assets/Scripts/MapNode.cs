using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
    public enum NodeStates
    {
        Locked,
        Visited,
        Attainable
    }
}

namespace Map
{
    public class MapNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public SpriteRenderer sr;
        public Image image;
        public SpriteRenderer visitedCircle;
        public Image circleImage;
        public Image visitedCircleImage;

        public Node Node { get; private set; }
        public NodeBlueprint Blueprint { get; private set; }

        private float initialScale;
        private const float HoverScaleFactor = 1.2f;
        private float mouseDownTime;

        private const float MaxClickDuration = 0.5f;

        public void SetUp(Node node, NodeBlueprint blueprint)
        {
            Node = node;
            Blueprint = blueprint;
            if (sr != null) sr.sprite = blueprint.sprite;
            if (image != null) image.sprite = blueprint.sprite;
            if (node.nodeType == NodeType.Boss) transform.localScale *= 1.5f;
            if (sr != null) initialScale = sr.transform.localScale.x;
            if (image != null) initialScale = image.transform.localScale.x;

            if (visitedCircle != null)
            {
                visitedCircle.color = MapView.Instance.visitedColor;
                visitedCircle.gameObject.SetActive(false);
            }

            if (circleImage != null)
            {
                circleImage.color = MapView.Instance.visitedColor;
                circleImage.gameObject.SetActive(false);    
            }
            
            SetState(NodeStates.Locked);
        }

        public void SetState(NodeStates state)
        {
            if (visitedCircle != null) visitedCircle.gameObject.SetActive(false);
            if (circleImage != null) circleImage.gameObject.SetActive(false);
            
            switch (state)
            {
                case NodeStates.Locked:
                    if (sr != null)
                    {
                        LeanTween.cancel(sr.gameObject);
                        sr.color = MapView.Instance.lockedColor;
                    }

                    if (image != null)
                    {
                        LeanTween.cancel(gameObject);
                        image.color = MapView.Instance.lockedColor;
                    }
                    break;
                case NodeStates.Visited:
                    if (sr != null)
                    {
                        LeanTween.cancel(sr.gameObject);
                        sr.color = MapView.Instance.visitedColor;
                    }
                    
                    if (image != null)
                    {
                        LeanTween.cancel(gameObject);
                        image.color = MapView.Instance.visitedColor;
                    }
                    
                    if (visitedCircle != null) visitedCircle.gameObject.SetActive(true);
                    if (circleImage != null) circleImage.gameObject.SetActive(true);
                    break;
                case NodeStates.Attainable:
                    if (sr != null)
                    {
                        sr.color = MapView.Instance.lockedColor;
                        LeanTween.cancel(sr.gameObject);
                        LeanTween.value(sr.gameObject, (Color color) => sr.color = color, 
                            MapView.Instance.lockedColor, MapView.Instance.visitedColor, 0.5f)
                            .setLoopPingPong();
                    }
                    
                    if (image != null)
                    {
                        image.color = MapView.Instance.lockedColor;
                        LeanTween.cancel(gameObject);
                        LeanTween.value(gameObject, (Color color) => image.color = color,
                            MapView.Instance.lockedColor, MapView.Instance.visitedColor, 0.5f)
                            .setLoopPingPong();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public void OnPointerEnter(PointerEventData data)
        {
            if (sr != null)
            {
                LeanTween.cancel(sr.gameObject);
                LeanTween.scale(sr.gameObject, Vector3.one * initialScale * HoverScaleFactor, 0.3f);
            }

            if (image != null)
            {
                LeanTween.cancel(gameObject);
                LeanTween.scale(gameObject, Vector3.one * initialScale * HoverScaleFactor, 0.3f);
            }
        }

        public void OnPointerExit(PointerEventData data)
        {
            if (sr != null)
            {
                LeanTween.cancel(sr.gameObject);
                LeanTween.scale(sr.gameObject, Vector3.one * initialScale, 0.3f);
            }

            if (image != null)
            {
                LeanTween.cancel(gameObject);
                LeanTween.scale(gameObject, Vector3.one * initialScale, 0.3f);
            }
        }

        public void OnPointerDown(PointerEventData data)
        {
            mouseDownTime = Time.time;
        }

        public void OnPointerUp(PointerEventData data)
        {
            if (Time.time - mouseDownTime < MaxClickDuration)
            {
                // user clicked on this node:
                MapPlayerTracker.Instance.SelectNode(this);
            }
        }

        public void ShowSwirlAnimation()
        {
            if (visitedCircleImage == null)
                return;

            const float fillDuration = 0.3f;
            visitedCircleImage.fillAmount = 0;

            LeanTween.value(gameObject, (float val) => visitedCircleImage.fillAmount = val, 0f, 1f, fillDuration);
        }

        private void OnDestroy()
        {
            if (image != null)
            {
                LeanTween.cancel(gameObject);
            }

            if (sr != null)
            {
                LeanTween.cancel(sr.gameObject);
            }
        }
    }
}
