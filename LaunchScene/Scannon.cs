using UnityEngine;
using TMPro;

public class Scannon : MonoBehaviour {

    //Só pra eu nao me confundir
    public float rotZ {
        get {
            return Mathf.Abs((360 - transform.rotation.eulerAngles.z) % 360);
        }
    }

    public GameObject cannonBallPrefab;
    public Transform myCanvasText, spawn;

    const float forceMin = 10.0f, forceMax = 50.0f;
    [Range(forceMin, forceMax)] public float force;
    const float ballMassMin = 1.0f, ballMassMax = 5.0f;
    [Range(ballMassMin, ballMassMax)] public float ballMass;
    private float actualRotationZ = 90;

    //Se eu fizer alteracoes no prefab, vai mexer no asset e ficar salvo lá
    GameObject cannonBall;
    Rigidbody rigidbuddy;
    TextMeshProUGUI playerCanvasText, cannonCanvasText;

    void Awake() {
        playerCanvasText = Camera.main.transform.GetComponentInChildren<TextMeshProUGUI>();
        cannonCanvasText = myCanvasText.GetComponent<TextMeshProUGUI>();

        cannonBall = Instantiate(cannonBallPrefab, spawn.position, Quaternion.identity);
        rigidbuddy = cannonBall.GetComponent<Rigidbody>();
        ballMass = ballMassMin;
        force = forceMin;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        float distX = force * Mathf.Cos(rotZ);
        float distY = force * Mathf.Sin(rotZ);

        playerCanvasText.text = "Velocidade Absoluta: " + string.Format("{0:N2}", rigidbuddy.velocity.magnitude) + " m/s\n" +
                                "Velocidade no Eixo X: " + string.Format("{0:N2}", rigidbuddy.velocity.x) + " m/s\n" +
                                "Velocidade no Eixo Y: " + string.Format("{0:N2}", rigidbuddy.velocity.y) + " m/s\n";
                                //"Distancia X,Y: " + distX + " , " + distY;
                                //Tá subtraindo pq o spawn tá com o X negativo lá na scene

        cannonCanvasText.text = "Massa da bola: " + string.Format("{0:N1}", ballMass) + " Kg\n" +
                                "Forca de lancamento: " + string.Format("{0:N1}", force) + " N\n" +
                                "Angulo: " + string.Format("{0:N1}",(90 - rotZ)) + "°";
        //O canhao aponta de cima pra direita
        //Quando apontar pra cima, o angulo z é 0. Pra direita, é -90
        //Pro usuário, fica mais bonita escrever 90 ou 0
    }

    public void shootBall() {
        if(cannonBall != null) {
            Destroy(cannonBall);
        }

        rigidbuddy = null;

        cannonBall = Instantiate(cannonBallPrefab, spawn.position, transform.rotation);

        rigidbuddy = cannonBall.GetComponent<Rigidbody>();

        rigidbuddy.isKinematic = false;
        rigidbuddy.mass = ballMass;

        //ForceModes Impulse e Force são os únicos que levam a massa em conta, e Force mal move 0.1kg
        rigidbuddy.AddForce(transform.up.normalized * force, ForceMode.Impulse);
    }

    public void setRot(float value) {
        if (actualRotationZ + value > 90) return;
        if (actualRotationZ + value < 5) return;
        actualRotationZ += value;
        transform.Rotate(0, 0, value, Space.World);
    }

    public void setForce(float value) {
        force += value;
        if(force < forceMin) {
            force = forceMin;
        }else if(force > forceMax) {
            force = forceMax;
        }
    }

    public void setMass(float value) {
        ballMass += value;
        if(ballMass < ballMassMin) {
            ballMass = ballMassMin;
        }else if(ballMass > ballMassMax) {
            ballMass = ballMassMax;
        }
    }
}