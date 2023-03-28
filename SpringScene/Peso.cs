using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class Peso : MonoBehaviour {
    
    Rigidbody rigidbuddy;
    Transform transform;
    SpringJoint mola;
    TextMeshProUGUI playerCanvasText;
    public GameObject cube;
    public GameObject teto;
    float posYInicial;

    float lastAccel, mass, gravidade;

    void Awake() {
        rigidbuddy = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        posYInicial = transform.position.y;
        mola = teto.GetComponent<SpringJoint>();
        mass = rigidbuddy.mass;
        playerCanvasText = Camera.main.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start() {
        lastAccel = rigidbuddy.velocity.magnitude;
    }

    void FixedUpdate() {

        Quaternion quat = Quaternion.Euler(0, 0, 0);
        cube.transform.rotation = quat; // impedir rotação do peso
        float posY = cube.transform.position.y;
        cube.transform.position = new Vector3(0, posY <= 2f ? 2f : posY , 2); // Restringir movimentação do peso somente no eixo y
        //cube.transform.position = new Vector3(0, posY <= 0.5f ? 0.5f : posY >= 4.5f ? 4.5f : posY , 2);

        float accel = (rigidbuddy.velocity.magnitude - lastAccel) / Time.deltaTime;
        lastAccel = rigidbuddy.velocity.magnitude;

        string vel = string.Format("{0:N1}", rigidbuddy.velocity.magnitude);
        string ace = string.Format("{0:N1}", accel);
        string def = string.Format("{0:N1}", Math.Abs(posY - posYInicial));// Deformação
        //string constanteElastica = string.Format("{0:N2}", mola.spring);
        //string amortecimento = string.Format("{0:N2}", mola.damper);
        string gravidade = string.Format("{0:N2}", -1 * Physics.gravity.y);

        playerCanvasText.text = "Velocidade: " + vel + "\nAceleracao: " + ace + "\nDeformacao: " + def + "\nMassa: " + mass ;
        //playerCanvasText.text = "Velocidade: " + vel + "\nAceleracao: " + ace + "\nDeformacao: " + def + "\nMassa: " + mass + "\nConstante Elástica: " + constanteElastica + "\nAmortecimento: " + amortecimento + "Gravidade: " + gravidade;

        if(rigidbuddy.mass == 0) {
            rigidbuddy.Sleep();
            Transform pointransform = GvrPointerInputModule.Pointer.PointerTransform;
            transform.SetParent(pointransform, true);
        }
    }
    
    public void onGrab() {
        rigidbuddy.Sleep();
        rigidbuddy.isKinematic = true;

        rigidbuddy.mass = 0;
        rigidbuddy.useGravity = false;

        Transform pointransform = GvrPointerInputModule.Pointer.PointerTransform;
        transform.SetParent(pointransform, true);
    }



    public void onRelease() {
        transform.SetParent(null, true);

        rigidbuddy.mass = mass;
        rigidbuddy.useGravity = true;

        rigidbuddy.isKinematic = false;
    }



    public void setMass(float value)
    {
        float valueFinal = rigidbuddy.mass;
        valueFinal += value;

        if (valueFinal < 0)
        {
            valueFinal = 0;
        }
        else if (valueFinal > 5)
        {
            valueFinal = 5;
        }

        rigidbuddy.mass = valueFinal;

    }

    public float getMass()
    {
        mass = rigidbuddy.mass;
        return mass;

    }


    public void setGravity(float value)
    {
        float valueFinal = Physics.gravity.y;
        valueFinal += value;
        if (valueFinal < -20)
        {
            valueFinal = -20;
        }
        else if (valueFinal > 0)
        {
            valueFinal = 0;
        }
        Physics.gravity = new Vector3(0, valueFinal, 0);

    }

    public float getGravity()
    {
        gravidade =  Physics.gravity.y;
        return gravidade;

    }




}