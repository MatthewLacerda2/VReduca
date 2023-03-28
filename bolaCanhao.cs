using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class bolaCanhao : MonoBehaviour {

    public GameObject sinalizador;
    GameObject sinalizadorRT;
    TextMeshProUGUI sinalizaText;

    public Transform pivot;

    public TextMeshProUGUI tmpro;

    public GameObject vetores;

    public bool trailReset, sleepOnContact;

    public float launchForce = 1.0f;

    AudioSource sauce;
    Rigidbody rigidbuddy;
    TrailRenderer trail;

    void Awake() {
        sauce = GetComponent<AudioSource>();
        rigidbuddy = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        sinalizadorRT = Instantiate(sinalizador, new Vector3(0, 100, 0), Quaternion.identity);
        sinalizaText = sinalizadorRT.GetComponent<TextMeshProUGUI>();
    }

    float velmax;
    void Update() {
        if (velmax < rigidbuddy.velocity.magnitude) {
            velmax = rigidbuddy.velocity.magnitude;
        }
        if (tmpro) {
            tmpro.text = "Velocidade maxima: " + velmax.ToString("n3") + "\nVelocidade atual: " + rigidbuddy.velocity.magnitude.ToString("n3");
        }

        if (vetores == null) {
            return;
        }

        if (rigidbuddy.velocity.magnitude == 0) {
            if (vetores.activeSelf==true) {
                vetores.SetActive(false);
            }
        } else {
            if (vetores.activeSelf==false) {
                vetores.SetActive(true);
            }
        }
    }

    public void Stop() {
        rigidbuddy.Sleep();
        rigidbuddy.isKinematic = true;
    }

    public void lancar() {
        rigidbuddy.isKinematic = false;
        rigidbuddy.AddForce(transform.right * launchForce, ForceMode.Impulse);

        if (sauce != null) {
            sauce.Play();
        }

        StartCoroutine(sinalizar());
    }

    System.Collections.IEnumerator sinalizar() {
        sinalizadorRT.SetActive(false);

        Vector3 prevY = transform.position;
        Vector3 currentY = transform.position;
        while (currentY.y >= prevY.y) {
            prevY = currentY;
            currentY = transform.position;
            yield return null;
        }
        sinalizadorRT.transform.position = prevY;

        if (sinalizaText) {
            sinalizaText.text = rigidbuddy.velocity.magnitude.ToString();
        }

        sinalizadorRT.SetActive(true);
    }

    public void setAngle(float z) {
        pivot.rotation = Quaternion.Euler(0, 0,z);

        transform.rotation = pivot.rotation;
    }

    public void setForca(float z) {
        launchForce = z;
    }

    public void Reset() {
        sinalizadorRT.SetActive(false);
        velmax = 0;
        Stop();
        transform.position = pivot.position;
        transform.SetPositionAndRotation(pivot.position, pivot.rotation);

        trail.Clear();
    }

    void OnCollisionEnter(Collision collision) {
        if (sleepOnContact) {
            Stop();
        }
    }
}