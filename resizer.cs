using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resizer : MonoBehaviour {

    public GameObject target;

    Slider slider;
    TextMeshProUGUI textPro;

    public Material[] mats;
    //Material[] original;

    void Awake() {
        slider = GetComponent<Slider>();
        textPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void resize() {
        float size = slider.value;
        target.transform.localScale = new Vector3(size, size, size);
        textPro.text = size.ToString();
    }

    public void gravidade() {
        Rigidbody rigidbuddy = target.GetComponent<Rigidbody>();

        rigidbuddy.Sleep();

        rigidbuddy.useGravity = !rigidbuddy.useGravity;
    }

    public void dropdownMatSelector(int valor) {
        MeshRenderer render = target.GetComponent<MeshRenderer>();

        Material[] materiais = new Material[render.materials.Length];

        for(int i = 0; i < materiais.Length; i++) {
            materiais[i] = mats[valor];
        }

        render.materials = materiais;
    }
}