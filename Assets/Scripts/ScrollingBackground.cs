using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public float backgroundSize;
    //public float paralaxSpeed;
    public bool scrolling, paralax;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 19.2f;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;



    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;

        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    void Update()
    {
        //if (paralax)
        //{
        //    float deltaX = cameraTransform.position.x - lastCameraX;
        //    transform.position += Vector3.right * (deltaX * paralaxSpeed);
        //}

        lastCameraX = cameraTransform.position.x;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone)) Scrolling("left");
            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone)) Scrolling("right");
        }
    }

    void Scrolling(string direction)
    {
        //Vector3 vectorRight = new Vector3(1, 0, layers[rightIndex].position.z);     // Vector3.right but taking the layer's z value into account
        float targetX;

        switch(direction)
        {
            case "left":
                int lastRight = rightIndex;        // Temporary pointer to last index
                //layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);

                targetX = layers[leftIndex].position.x - backgroundSize;
                layers[rightIndex].position = new Vector3(targetX, 0, layers[rightIndex].position.z);
                leftIndex = rightIndex;
                rightIndex--;

                if (rightIndex < 0)
                {
                    rightIndex = layers.Length - 1;
                }
                break;

            case "right":
                int lastLeft = rightIndex;        // Temporary pointer to last index
                //layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);

                targetX = layers[rightIndex].position.x + backgroundSize;
                layers[leftIndex].position = new Vector3(targetX, 0, layers[leftIndex].position.z);
                rightIndex = leftIndex;
                leftIndex++;

                if (leftIndex == layers.Length)
                {
                    leftIndex = 0;
                }
                break;
        }

        ////layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        //layers[rightIndex].position = vectorRight * (layers[leftIndex].position.x - backgroundSize);
        //leftIndex = rightIndex;
        //rightIndex--;

        //if (rightIndex < 0)
        //{
        //    rightIndex = layers.Length - 1;
        //}
    }

    void ScrollRight()
    {
        int lastLeft = rightIndex;        // Temporary pointer to last index
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}

