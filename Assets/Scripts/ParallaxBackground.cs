using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    public Transform[] backgroundLayers;        // Array of all the back and foregrounds
    public Transform[] invertedLayers;          // Array of all the layers inverted at the X axis, used for the scrolling
    public float smoothing = 1;                 // How smooth the parallax is going to be. Make sure to set this above 0

    float[] parallaxScales;                     // The proportion of the camera's movement to move the backgrounds by

    Transform cam;                              // reference to the main camera's transform
    Vector3 lastCamPos;                         // camera position during the last frame

    int leftIndex, rightIndex;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start () {
        // The previous frame had the current frame's camera position
        lastCamPos = cam.position;
        backgroundLayers = new Transform[transform.childCount];
        invertedLayers = new Transform[backgroundLayers.Length];
        parallaxScales = new float[backgroundLayers.Length];

        for(int i = 0; i < backgroundLayers.Length; i++)
        {
            // get all the different layers into the arrays
            backgroundLayers[i] = transform.GetChild(i);
            //layer(i) = new Layer();
            //invertedLayers[i] = backgroundLayers[i].GetChild(0);

            // assign the corresponding parallax scales
            // the different layers start on z = 0 and are moved down the z axis towards the camera, hence the multiplier by -1
            parallaxScales[i] = backgroundLayers[i].position.z * -1;
        }

	}
	
	void Update () {
        for (int i = 0; i < backgroundLayers.Length; i++)       // for each background layer
        {
            // the parallax is the opposite of the camera movement (previous frame minus current) multiplied by the scale
            float parallax = (lastCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position (current position plus the parallax)
            float backgroundTargetPosX = backgroundLayers[i].position.x + parallax;

            // create a target position, the background's current position with adjusted x value
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundLayers[i].position.y, backgroundLayers[i].position.z);

            // fade between current position and the target position using lerp
            backgroundLayers[i].position = Vector3.Lerp(backgroundLayers[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

            // scroll the background layers
            //if (cam.position.x < (backgroundLayers[i].position.x + viewZone)) ScrollLeft(backgroundLayers[i]);
            //if (cam.position.x > (backgroundLayers[i].position.x - viewZone)) ScrollRight();
        }

        // set the lastCamPos to the camera's position at the end of the frame
        lastCamPos = cam.position;
	}

    void ScrollLeft(Transform layer)
    {
        //if (layer.childCount == 0) Transform scrollingLayer = new Transform();
    }

    void ScrollRight()
    {
        int previousRight = rightIndex;         // Temporary pointer to last right index

    }

    public class Layer
    {

        int leftIndex, rightIndex;
        Transform[] backgroundImages;
    }
}
