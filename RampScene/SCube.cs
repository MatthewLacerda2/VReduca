using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class SCube : MonoBehaviour {
    
    Rigidbody rigidbuddy;
    TextMeshProUGUI playerCanvasText;

    float lastAccel, mass;

    void Awake() {
        rigidbuddy = GetComponent<Rigidbody>();
        mass = rigidbuddy.mass;

        playerCanvasText = Camera.main.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start() {
        lastAccel = rigidbuddy.velocity.magnitude;
    }

    void FixedUpdate() {
        float accel = (rigidbuddy.velocity.magnitude - lastAccel) / Time.deltaTime;
        lastAccel = rigidbuddy.velocity.magnitude;

        string vel = string.Format("{0:N2}", rigidbuddy.velocity.magnitude);
        string ace = string.Format("{0:N2}", accel);

        playerCanvasText.text = "Velocidade: " + vel + "\nAceleracao: " + ace;

        if(rigidbuddy.mass == 0) {
            rigidbuddy.Sleep();
            Transform pointransform = GvrPointerInputModule.Pointer.PointerTransform;
            transform.SetParent(pointransform, true);
        }
    }
}