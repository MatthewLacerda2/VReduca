using UnityEngine;

public class educRig : MonoBehaviour {

    public GameObject canvas;
    public Vector3 startCompensation;

    // Start is called before the first frame update
    void Start() {
        canvas.SetActive(true);

        Vector3 aux = transform.position;
        aux += startCompensation;
        transform.position = aux;
    }
}