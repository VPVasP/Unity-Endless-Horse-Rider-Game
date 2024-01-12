using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target; //target refrence
    [SerializeField] private Vector3 offset; //offset value

    void Start()
    {
        //we find the horse object with the tag Horse
        GameObject Horse = GameObject.FindGameObjectWithTag("Horse");
        target = Horse.transform;
        //we calculate the offset between the camera and the target
        offset = transform.position - target.position;
    }

    void Update()
       
    {
        //we update the camera position according to the target position and the offset
        transform.position = target.position + offset;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
