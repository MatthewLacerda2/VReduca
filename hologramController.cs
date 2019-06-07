using UnityEngine;

[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class hologramController : MonoBehaviour {
    
    public Material holoMat;

    Material[] mats;

    public GameObject parent;

    float countdown = 3.0f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(stdLib.cameraPos);
        transform.position = parent.transform.position + Vector3.right * 0.4f;
        
        if(countdown > 0) {
            countdown -= Time.deltaTime;
        } else {
            if(transform.localScale == new Vector3(1, 1, 1)) {
                StartCoroutine(shrink());
            }
        }
    }

    public void resetCountdown(GameObject go, Material[] materials) {
        StopCoroutine(shrink());
        countdown = 3.0f;
        parent = go;
        transform.localScale = new Vector3(1, 1, 1);
        mats = materials;
    }

    System.Collections.IEnumerator shrink() {
        while(countdown > -1.0f) {
            float x = transform.localScale.x - Time.deltaTime;
            float y = transform.localScale.y - Time.deltaTime;
            float z = transform.localScale.z - Time.deltaTime;

            Vector3 scale = new Vector3(x, y, z);

            transform.localScale = scale;

            countdown -= Time.deltaTime;

            yield return null;
        }
        Destroy(gameObject);
    }

    public void rotate(float y) {
        parent.transform.Rotate(0, 0, y);
        resetCountdown(parent, mats);
    }

    public void changeLook(bool hologramLook) {
        if(hologramLook) {
            parent.GetComponent<MeshRenderer>().materials = mats;
        } else {
            Material[] auxMats = new Material[mats.Length];
            for(int i = 0; i < auxMats.Length; i++) {
                auxMats[i] = holoMat;
            }
            parent.GetComponent<MeshRenderer>().materials = auxMats;
        }
        resetCountdown(parent, mats);
    }

    public void onPointerEnter() {
        resetCountdown(parent, mats);
    }
}