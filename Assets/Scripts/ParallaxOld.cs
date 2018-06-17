using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxOld : MonoBehaviour
{

    public Transform[] backgroundLayers;        // Array of all the back and foregrounds
    public float smoothing = 1;                 // How smooth the parallax is going to be. Make sure to set this above 0

    float[] parallaxScales;                     // The proportion of the camera's movement to move the backgrounds by
    Transform cam;                              // reference to the main camera's transform
    Vector3 lastCamPos;                         // camera position during the last frame

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        // The previous frame had the current frame's camera position
        lastCamPos = cam.position;

        // assigning corresponding parallax scales ??
        parallaxScales = new float[backgroundLayers.Length];
        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            // the different layers start on z = 0 and are moved down the z axis towards the camera, hence the multiplier by -1
            parallaxScales[i] = backgroundLayers[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for each background layer
        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (lastCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgroundLayers[i].position.x + parallax;

            // create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundLayers[i].position.y, backgroundLayers[i].position.z);

            // fade between current positoin and the target position using lerp
            backgroundLayers[i].position = Vector3.Lerp(backgroundLayers[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set the lastCamPos to the camera's position at the end of the frame
        lastCamPos = cam.position;
    }
}
