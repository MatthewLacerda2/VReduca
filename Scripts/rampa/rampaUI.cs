using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rampaUI : MonoBehaviour {

    public Transform rampaTransf;
    public GameObject cube;

    BoxCollider rampaCollider;
    TextMeshProUGUI textMeshPro;
    Slider slider;

    void Awake() {
        rampaCollider = rampaTransf.GetComponentInChildren<BoxCollider>();
        slider = GetComponent<Slider>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Reset() {
        Rigidbody rigidbudddy = cube.GetComponent<Rigidbody>();
        rigidbudddy.Sleep();

        cube.transform.rotation = rampaTransf.rotation;
        cube.transform.position = rampaTransf.position;
    }

    public void friccao() {
        BoxCollider cubeCol = cube.GetComponent<BoxCollider>();
        PhysicMaterial mat = new PhysicMaterial("Friccao");
        mat.dynamicFriction = slider.value;
        mat.staticFriction = slider.value;
        mat.frictionCombine = PhysicMaterialCombine.Average;

        rampaCollider.material = mat;
        cubeCol.material = mat;

        textMeshPro.text = slider.value.ToString("F2");
    }

    public void rotate() {
        rampaTransf.localRotation = Quaternion.Euler(0, 0, slider.value);
        textMeshPro.text = slider.value.ToString("F2") + "°";
    }
}