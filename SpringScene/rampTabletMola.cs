using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rampTabletMola : MonoBehaviour {

    public Mola teto;
    public Peso peso;
    SpringJoint mola;

    public float value;

    Slider slider;
    TextMeshProUGUI myText;

    void Awake() {
        myText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        slider = transform.parent.GetComponentInChildren<Slider>();
        mola = teto.GetComponent<SpringJoint>();

    }

    public void setMass()
    {
        peso.setMass(value);

        slider.value = peso.getMass();
        myText.text = "Massa: " + string.Format("{0:N1}", slider.value);
    }

    public void setGravity()
    {
        peso.setGravity(value);
        slider.value = -1 * peso.getGravity();
        myText.text = "Gravidade: " + slider.value.ToString();

    }

    

    public void setConstanteElastica()
    {
        teto.setConstanteElastica(value);
        slider.value = teto.getConstanteElastica();
        myText.text = "Constante Elastica: " + slider.value.ToString();


    }

    public void setAmortecimento()
    {
        teto.setAmortecimento(value);
        slider.value = teto.getAmortecimento();
        myText.text = "Amortecimento " + slider.value.ToString();

    }

}