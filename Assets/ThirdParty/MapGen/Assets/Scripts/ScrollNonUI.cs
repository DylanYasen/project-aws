using UnityEngine;

namespace Map
{
    public class ScrollNonUI : MonoBehaviour
    {
        public float tweenBackDuration = 0.3f;
        public LeanTweenType tweenBackEase = LeanTweenType.easeInOutQuad;
        public bool freezeX;
        public FloatMinMax xConstraints = new FloatMinMax();
        public bool freezeY;
        public FloatMinMax yConstraints = new FloatMinMax();
        private Vector2 offset;
        // distance from the center of this Game Object to the point where we clicked to start dragging 
        private Vector3 pointerDisplacement;
        private float zDisplacement;
        private bool dragging;
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
            zDisplacement = -mainCamera.transform.position.z + transform.position.z;
        }

        public void OnMouseDown()
        {
            pointerDisplacement = -transform.position + MouseInWorldCoords();
            LeanTween.cancel(gameObject);
            dragging = true;
        }

        public void OnMouseUp()
        {
            dragging = false;
            TweenBack();
        }

        private void Update()
        {
            if (!dragging) return;

            Vector3 mousePos = MouseInWorldCoords();
            //Debug.Log(mousePos);
            transform.position = new Vector3(
                freezeX ? transform.position.x : mousePos.x - pointerDisplacement.x,
                freezeY ? transform.position.y : mousePos.y - pointerDisplacement.y,
                transform.position.z);
        }

        // returns mouse position in World coordinates for our GameObject to follow. 
        private Vector3 MouseInWorldCoords()
        {
            Vector3 screenMousePos = Input.mousePosition;
            //Debug.Log(screenMousePos);
            screenMousePos.z = zDisplacement;
            return mainCamera.ScreenToWorldPoint(screenMousePos);
        }

        private void TweenBack()
        {
            if (freezeY)
            {
                if (transform.localPosition.x >= xConstraints.min && transform.localPosition.x <= xConstraints.max)
                    return;

                float targetX = transform.localPosition.x < xConstraints.min ? xConstraints.min : xConstraints.max;
                LeanTween.moveLocalX(gameObject, targetX, tweenBackDuration).setEase(tweenBackEase);
            }
            else if (freezeX)
            {
                if (transform.localPosition.y >= yConstraints.min && transform.localPosition.y <= yConstraints.max)
                    return;

                float targetY = transform.localPosition.y < yConstraints.min ? yConstraints.min : yConstraints.max;
                LeanTween.moveLocalY(gameObject, targetY, tweenBackDuration).setEase(tweenBackEase);
            }
        }
    }
}
