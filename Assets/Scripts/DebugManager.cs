using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _textMeshPro;
    private static DebugManager _instance;
    private void Start()
    {
        _instance = this;
    }
    public static void SetText(string text)
    {
        _instance._textMeshPro.text = text;
    }
}
