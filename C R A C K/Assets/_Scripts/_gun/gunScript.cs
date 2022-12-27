using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class gunScript : MonoBehaviour{
    public static bool isReloading;

    [Header("Setup")]
    public Camera fpsCam;
    public LayerMask ignore_;
    public gunRecoilScript recoil;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource fireSound;
    public AudioSource fireSoundSilenced;

    [Header("Hit Markers")]
    public GameObject uiMarker;
    public GameObject markers;
    public AudioSource hitSound;

    [Header("Tracers")]
    public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public GameObject bullet;
    public Transform bulletSpawn;

    [Header("Animating")]
    float r_clipTime;
    public Animator _animator;
    public AnimationClip r_clip;

    [Header("Stats")]
    public float damage = 12f;
    public float range = 220f;
    public float fireRate = 15f;
    public int ammo = 24;
    public int maxAmmo = 24;

    public float recoilX;
    public float recoilY;
    public float recoilZ;

    bool _onceR = false;

    private float nextTimeToFire = 0f;
    bool silenced = false;

    [Header("Weapon Modding")]
    public _AddModding wM;

    Ray ray;
    RaycastHit hit;
    RaycastHit tracerHit;

    void Start(){
        r_clipTime = r_clip.length;
        ammo = maxAmmo;

        gunRecoilScript.recoilX = recoilX;
        gunRecoilScript.recoilY = recoilY;
        gunRecoilScript.recoilZ = recoilZ;
    }
    void Update(){
        modCheck();

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !movementScript.isMoving && ammo > 0 && !isReloading && movementScript.isGrounded){
                nextTimeToFire = Time.time + 1f / fireRate;
                fire();
                fireTracers();

                _animator.SetBool("recoil", true);
            }
            else if (Input.GetButtonUp("Fire1")){
                _animator.SetBool("recoil", false);
            }

            if (movementScript.isMoving){
                _animator.SetBool("recoil", false);
            }

            if (ammo < 1){
                StartCoroutine(reload());
            }
    }
        void fire()
        {
            if(!_onceR){
                ammo = maxAmmo;
                _onceR = true;
            }
            
            recoil.recoil();
            if (!silenced){
                fireSound.Play();
            }
            else if (silenced){
                fireSoundSilenced.Play();
            }
            muzzleFlash.Play();

            GameObject bulletOBJ = Instantiate(bullet.gameObject, bulletSpawn.position, Quaternion.identity);
            bulletOBJ.transform.rotation = Random.rotation;

            Destroy(bulletOBJ, 6f);

            ammo--;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~ignore_)){
                _playerScript p_script = hit.transform.GetComponent<_playerScript>();
                powderEffect p_effect = hit.transform.GetComponent<powderEffect>();

                if (p_effect != null){
                    GameObject h_marker = Instantiate(uiMarker.gameObject, markers.transform.position, Quaternion.identity);
                    h_marker.transform.parent = markers.transform;

                    hitSound.Play();
                    Destroy(h_marker, 0.2f);

                    p_effect.powderExplosion();
                }

                if (p_script != null){
                    GameObject h_marker = Instantiate(uiMarker.gameObject, markers.transform.position, Quaternion.identity);
                    h_marker.transform.parent = markers.transform;

                    hitSound.Play();
                    Destroy(h_marker, 0.2f);

                    p_script.takeDamage(damage);
                }

                GameObject objEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(objEffect, 5.5f);
            }
        }
        void fireTracers(){
            ray.origin = raycastOrigin.position;
            ray.direction = raycastDestination.position - raycastOrigin.position;

            var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
            tracer.AddPosition(ray.origin);

            if (Physics.Raycast(ray, out tracerHit, 9999f, ~ignore_))
            {
                tracer.transform.position = tracerHit.point;
                Debug.DrawLine(ray.origin, tracerHit.point, Color.red, 2f);
            }
        }
        IEnumerator reload(){
            _animator.SetBool("recoil", false);

            isReloading = true;
            _animator.SetBool("reload", true);
            yield return new WaitForSeconds(r_clipTime);
            _animator.SetBool("reload", false);
            isReloading = false;
            ammo = maxAmmo;
        }
    void modCheck()
    {
        if (wM.muzzle == 0)
            silenced = false;
        if (wM.muzzle == 1){
            silenced = true;
            recoilX = 3.5f;
            recoilY = 3.5f;
        }
        if (wM.muzzle == 2){
            silenced = true;
            recoilX = 2f;
            recoilY = 2f;
        }

        // -

        if (wM.muzzle == 0){
            damage = 8.5f;
            fireRate = 10f;
            maxAmmo = 24;
        }
        if (wM.muzzle == 1){
            damage = 2.5f;
            fireRate = 7.5f;
            maxAmmo = 42;
        }
        if (wM.muzzle == 2){
            damage = 2.5f;
            fireRate = 12.5f;
            maxAmmo = 86;
        }
    }
}