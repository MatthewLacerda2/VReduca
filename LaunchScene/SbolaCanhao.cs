using UnityEngine;

public class SbolaCanhao : MonoBehaviour {

    public GameObject altitudeSignalPrefab;
    
    GameObject instantiatedSignal;
    Rigidbody rigidbuddy;
    Vector3 highestAltitude;

    bool altitudeMarked = false;
    float mass;

    void Awake() {
        rigidbuddy = GetComponent<Rigidbody>();

        mass = rigidbuddy.mass;
    }

    // Start is called before the first frame update
    void Start() {
        highestAltitude = transform.position;
        StartCoroutine(startPass());
        Debug.Log(transform.position);
    }

    System.Collections.IEnumerator startPass() {
        //long story short essa funcao faz o collider ignorar o mesh da CubeRoom quando for instanciado
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = true;
    }
    
    // Update is called once per frame
    void Update() {
        //transform.position = new Vector3(transform.position.x, transform.position.y <= 0.125f ? 0.125f : transform.position.y, transform.position.z);
        Vector3 currentAltitude = transform.position;
        if(currentAltitude.y > highestAltitude.y) {
            highestAltitude = currentAltitude;
        }else if(currentAltitude.y < highestAltitude.y && altitudeMarked == false) {
            instantiatedSignal = Instantiate(altitudeSignalPrefab, highestAltitude, Quaternion.identity);
            Debug.Log(highestAltitude);
            altitudeMarked = true;
        }
    }
    
    public void onGrab() {
        rigidbuddy.Sleep();
        rigidbuddy.isKinematic = true;
        rigidbuddy.freezeRotation = true;
        rigidbuddy.mass = 0;
        rigidbuddy.useGravity = false;

        Transform pointransform = GvrPointerInputModule.Pointer.PointerTransform;
        transform.SetParent(pointransform, true);
    }

    private void OnTriggerExit(Collider other)
    {        
        GetComponent<Collider>().isTrigger = false;
        transform.position = new Vector3(transform.position.x, transform.position.y <= 0.125f ? 0.125f : transform.position.y, transform.position.z);
    }

    public void onRelease() {
        transform.SetParent(null, true);
        rigidbuddy.freezeRotation = false;
        rigidbuddy.mass = mass;
        rigidbuddy.useGravity = true;
        
        rigidbuddy.isKinematic = false;
    }

    void OnDestroy() {
        Destroy(instantiatedSignal);
    }

    void OnCollisionEnter(Collision collision) {
        rigidbuddy.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rigidbuddy.isKinematic = true;
    }
}