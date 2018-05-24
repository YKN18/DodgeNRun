using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    //[SerializeField] float smoothing = 1f;
    [SerializeField] public Vector3 offset;

    void LateUpdate() {
        transform.position = target.position + offset;
        //transform.position = Vector3.Lerp(transform.position, pos, smoothing);
    }
}
