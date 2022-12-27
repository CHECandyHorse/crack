using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class powderEffect : NetworkBehaviour{
    public ParticleSystem powderExplosionEffect;

    [Command(requiresAuthority = false)]
    public void powderExplosion(){
        powderExplosionEffect.Play();
    }
}
