using UnityEngine;

public class soundCue : MonoBehaviour {

    public AudioClip[] array;

    AudioSource source;
    GameObject go;

    void Start() {
        go = Instantiate(new GameObject("audioCuer"), transform.position, Quaternion.identity, transform);
        go.transform.localRotation = Quaternion.Euler(0, 0, 0);

        source = go.AddComponent<AudioSource>();

        source.playOnAwake = false;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 0;
        source.maxDistance = 30;    //Uma ideia seria mudar a distancia baseado na velocidade de impacto
    }

    void OnCollisionEnter(Collision collision) {
        go.transform.position = collision.GetContact(0).point;

        source.clip = array[Random.Range(0, array.Length - 1)];
        source.Play();
    }
}