using UnityEngine;

public class EditorOnlyObject : MonoBehaviour
{
    private void Awake()
    {
        #if !UNITY_EDITOR
            gameObject.SetActive(false);
        #endif
    }
} 