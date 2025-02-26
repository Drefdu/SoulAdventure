using UnityEngine;


public class cameraController
{
    public Transform personaje;

    private float sizeCamera;
    private float myDisplaySize;

    void start()
    {
        sizeCamera = Camera.main.orthographicSize;
        myDisplaySize = sizeCamera * 2;
    }
}
