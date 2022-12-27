using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicCrosshair : MonoBehaviour{
    public RectTransform crosshair;
    bool isFire = false;

    [Range(25, 250f)]
    public float size = 25;

    void Update(){
        crosshair.sizeDelta = new Vector2(size, size);

        if (Input.GetButtonDown("Fire1")){
            isFire = true;
        }
        if (Input.GetButtonUp("Fire1")){
            isFire = false;
        }

        if (isFire){
            size = Mathf.Lerp(size, 50f, Time.deltaTime * 6.5f);
        }
        else if(!isFire){
            size = Mathf.Lerp(size, 25f, Time.deltaTime * 6.5f);                        
        }
    }
}
