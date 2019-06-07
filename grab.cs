using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class grab : MonoBehaviour {

    Rigidbody rigidbuddy;

    float mass;

    void Awake() {
        rigidbuddy = GetComponent<Rigidbody>();

        mass = rigidbuddy.mass;
    }

    // Start is called before the first frame update
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
}