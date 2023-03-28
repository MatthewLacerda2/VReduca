using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rampTablet : MonoBehaviour {

    public GameObject rampRoot;

    public float value;

    Slider slider;
    Sramp ramp;
    TextMeshProUGUI myText;

    void Awake() {
        myText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        slider = transform.parent.GetComponentInChildren<Slider>();

        ramp = rampRoot.GetComponent<Sramp>();
    }

    public void setRot() {
        float valueFinal = slider.value + value;
        if(valueFinal < 0) {
            valueFinal = 0;
        }else if(valueFinal > 89) {
            valueFinal = 89;
        }

        slider.value = valueFinal;
        myText.text = "Angulo: " + valueFinal;

        Quaternion quat = Quaternion.Euler(0, 0, valueFinal);
        ramp.transform.rotation = quat;

        ramp.resetCubo();
    }

    public void setFrictionDynamic() {
        ramp.setFrictionDynamic(value);

        slider.value = ramp.defaultMat.dynamicFriction;

        myText.text = "Atrito Dinâmico: " + slider.value.ToString();
    }

    public void setFrictionStatic() {
        ramp.setFrictionStatic(value);

        slider.value = ramp.defaultMat.staticFriction;

        myText.text = "Atrito Estático: " + slider.value.ToString();
    }

    public void setBounciness() {
        ramp.setBounciness(value);

        slider.value = ramp.defaultMat.bounciness;

        myText.text = "Bounciness: " + slider.value.ToString();
    }

    public void play() {
        ramp.resetCubo();
    }
}