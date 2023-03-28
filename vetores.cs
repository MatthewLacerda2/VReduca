using UnityEngine;

public class vetores : MonoBehaviour {

    public GameObject velocidade, peso;

    public float threshold = 0.05f;

    Vector3 lastPos;

    void Start() {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        Vector3 vel = transform.position - lastPos;

        velocidade.SetActive(vel.magnitude < threshold);

        velocidade.transform.LookAt(transform.position + vel);
        peso.transform.LookAt(transform.position + Vector3.down);

        lastPos = transform.position;
    }
}