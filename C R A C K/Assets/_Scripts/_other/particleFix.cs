using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleFix : MonoBehaviour{
    void Start(){
        ParticleSystemRenderer renderer = GetComponent<ParticleSystemRenderer>();
        renderer.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;
    }
}
