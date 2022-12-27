using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _crosshairTarget : MonoBehaviour
{
    public Camera fpsCam;
    public LayerMask ignore;
    Ray ray;
    RaycastHit hit;

    void Update(){
        ray.origin = fpsCam.transform.position;
        ray.direction = fpsCam.transform.forward;
        if (Physics.Raycast(ray, out hit ,9999 , ~ignore)){
            transform.position = hit.point;
        }
        else{
            transform.position = ray.origin + ray.direction * 1000.0f;
        }
    }
}
