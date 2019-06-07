using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class hologram : MonoBehaviour {

    public GameObject holoControl;
    public Material[] mats;

    GameObject holoControlInstance;
    Quaternion auxQuat;

    bool beingDragged = false;
    bool beingPointed = false;

    void Awake() {

    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(beingDragged) {
            transform.rotation = auxQuat;
        }
        if(holoControl == null) {
            transform.LookAt(stdLib.cameraPos);
        }
        if(beingPointed) {
            if(holoControlInstance == null) {
                Vector3 pos = transform.position + Vector3.right * 0.4f;
                holoControlInstance = Instantiate(holoControl, pos, Quaternion.LookRotation(stdLib.cameraPos));
            } else if(holoControlInstance != null) {
                holoControlInstance.GetComponent<hologramController>().resetCountdown(gameObject, mats);
            }
        }
    }

    public void onGrab() {
        Transform pointransform = GvrPointerInputModule.Pointer.PointerTransform;
        transform.SetParent(pointransform, true);

        beingDragged = true;

        auxQuat = transform.rotation;
    }

    public void onRelease() {
        transform.SetParent(null, true);

        beingDragged = false;
    }

    public void onPointerEnter() {
        beingPointed = true;
    }

    public void onPointerExit() {
        beingPointed = false;
    }

    void FixedUpdate() {
        
        float x = transform.position.x;
        float y = transform.position.y;
        if(y < 1.5f) {
            y = 1.5f;
        }else if(y > 3.5f) {
            y = 3.5f;
        }
        float z = transform.position.z;
        
        transform.position = new Vector3(x, y, z);
    }
}