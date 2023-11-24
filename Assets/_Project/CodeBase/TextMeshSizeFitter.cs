using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class TextMeshSizeFitter : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private float size;

    private void OnValidate()
    {
        if (texts == null || texts.Count == 0) return;
        foreach (var item in texts)
            if (item != null) item.fontSize = size;
        
        texts.RemoveAll(x => x == null);
    }
}
