using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMixer))]
public class CamMixPrueba : MonoBehaviour
{
    [SerializeField]
    Camera[] cameras;
    CameraMixer mixer;

    public Camera cameraPlayer;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mixer = GetComponent<CameraMixer>();
    }

    public void changeCamera()
    {
        //mainCamera.enabled = true;
        //cameraPlayer.enabled = false;
        mixer.blendCamera(mainCamera, 5f, Interpolators.bounceInOut);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            mixer.blendCamera(cameras[0], 10.0f, Interpolators.bounceInOut);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mixer.blendCamera(cameras[1], 5.0f, Interpolators.circularIn);
        }

        if (Input.GetKey(KeyCode.D))
        {
            mixer.blendCamera(cameras[2], 3.0f, Interpolators.expoIn);
        }

    }
}
