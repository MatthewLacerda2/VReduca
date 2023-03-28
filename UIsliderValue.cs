using UnityEngine;
using TMPro;

public class UIsliderValue : MonoBehaviour {

    public GameObject go;

    public string prefixo, sufixo;

    TextMeshProUGUI textM;

    void Awake() {
        if (go) {
            textM = go.GetComponent<TextMeshProUGUI>();
        } else {
            textM = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void stringar(float value) {
        string total = prefixo + value.ToString("00.0") + sufixo;

        textM.text = total;
    }
}