using UnityEngine;

public class print : MonoBehaviour {

    Camera myCam;
    bool takeNextFrame;

    void Start() {
        myCam = GetComponent<Camera>();
    }

    void Update() {
        if(Input.GetKey(KeyCode.Space)) {
            takeScreen(1440,2560);
        }
    }

    void OnPostRender() {
        if(takeNextFrame) {
            takeNextFrame = false;
            RenderTexture renderTexture = myCam.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);

            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/camerascreen.png", byteArray);
            Debug.Log("tirou, porra");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCam.targetTexture = null; 

        }
    }

    void takeScreen(int width, int height) {
        myCam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeNextFrame = true;
    }
}