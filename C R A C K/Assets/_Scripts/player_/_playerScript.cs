using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class _playerScript : NetworkBehaviour{
    public Slider healthBar;
    public Slider shieldBar;
    public TMP_Text healthCount;
    public TMP_Text shieldCount;
    public GameObject gui;
    public gunScript gun_;

    [Header("Setup")]
    public GameObject ragdoll_p;
    public GameObject gun_object;
    public GameObject hands_p;
    public GameObject p_model;
    public movementScript movement_p;
    public GameObject _deathCam;
    public GameObject _camera;

    [Header("Player")]
    public GameObject win_lostSign;
    public TMP_Text winLostText;
    [SyncVar] public bool P_A;
    [SyncVar] public bool P_B;

    bool localPA;
    bool localPB;

    public static bool isLocal;

    [Header("Manage")]
    [SyncVar] public float health = 200;
    float maxHealth = 200;

    bool died = false;

    void Start() {
        if (!P_A){
            P_A = true;
            localPA = true;
        }
        else if (P_A){
            P_B = true;
            localPB = true;
        }


        maxHealth = health;
        healthBar.maxValue = maxHealth / 2;
        shieldBar.maxValue = maxHealth;
        shieldBar.minValue = maxHealth / 2;
        healthBar.value = health / 2;
        shieldBar.value = health;

        if (isLocalPlayer){
            isLocal = true;
            gui.SetActive(true);

            int local = LayerMask.NameToLayer("_ignore-layer");
            gameObject.layer = local;
        }
        if (!isLocalPlayer){
            isLocal = false;
            gui.SetActive(false);
            gun_.enabled = false;

            int none = LayerMask.NameToLayer("_clone");
            gameObject.layer = none;
        }
    }
    void Update() {
        if (health > 100){
            shieldBar.value = health;
            healthBar.value = 100;
        }
        else if (health < 100){
            shieldBar.value = 0;
            healthBar.value = health;
        }

        if(health < 1){
            disableStuff();
        }
        if(health > 200){
            health = 200;
        }
    }

    void disableStuff(){
        died = true;

        if (isLocalPlayer){
            _deathCam.SetActive(true);
        }
        _camera.SetActive(false);
        ragdoll_p.SetActive(true);
        hands_p.SetActive(false);
        p_model.SetActive(false);
        gun_object.SetActive(false);
        movement_p.enabled = false;
    }

    [Command(requiresAuthority = false)]
    public void takeDamage(float damage){
        health -= damage;
    }
}
