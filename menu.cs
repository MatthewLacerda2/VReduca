using UnityEngine;

public class menu : MonoBehaviour {

    [System.Serializable] public enum Scene {    //Assim evita ter que procurar o nome da cena e erros de digitacao
        RampScene, LaunchScene, SpringScene, aulaVR
    }

    public Scene scene;

    public void changeScene() {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString());
    }
}