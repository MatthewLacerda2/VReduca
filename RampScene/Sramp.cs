using UnityEngine;

public class Sramp : MonoBehaviour {

    public PhysicMaterial defaultMat;

    public Transform Cubo, ramp, spawn;

    BoxCollider cuboCollider, rampCollider;
    Rigidbody rigidbuddy;

    void Awake() {
        cuboCollider = Cubo.GetComponent<BoxCollider>();
        rigidbuddy = Cubo.GetComponent<Rigidbody>();
        rampCollider = ramp.GetComponent<BoxCollider>();
    }

    public void resetCubo() {
        rigidbuddy.Sleep();

        Cubo.transform.position = spawn.position;
        Cubo.localRotation.eulerAngles.Set(0, 0, 0);
    }

    public void setFrictionDynamic(float value) {
        float valueFinal = defaultMat.dynamicFriction;
        valueFinal += value;
        if(valueFinal < 0) {
            valueFinal = 0;
        }else if(valueFinal > 1) {
            valueFinal = 1;
        }

        defaultMat.dynamicFriction = valueFinal;
        rampCollider.material = defaultMat;
        cuboCollider.material = defaultMat;
        //resetCubo();
    }

    public void setFrictionStatic(float value) {
        float valueFinal = defaultMat.staticFriction;
        valueFinal += value;
        if(valueFinal < 0) {
            valueFinal = 0;
        } else if(valueFinal > 1) {
            valueFinal = 1;
        }

        defaultMat.staticFriction = valueFinal;
        rampCollider.material = defaultMat;
        cuboCollider.material = defaultMat;
        //resetCubo();
    }

    public void setBounciness(float value) {
        float valueFinal = defaultMat.bounciness;
        valueFinal += value;
        if(valueFinal < 0) {
            valueFinal = 0;
        } else if(valueFinal > 1) {
            valueFinal = 1;
        }

        defaultMat.bounciness = valueFinal;
        rampCollider.material = defaultMat;
        cuboCollider.material = defaultMat;
        //resetCubo();
    }

    void OnDestroy() {
        defaultMat.dynamicFriction = 0;
        defaultMat.staticFriction = 0;
        defaultMat.bounciness = 0;
    }
}