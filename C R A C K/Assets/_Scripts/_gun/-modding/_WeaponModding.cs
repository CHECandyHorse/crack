using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WeaponModding : MonoBehaviour{
    [Header("GameObjects Muzzle")]
    public GameObject SiclorX6LargeObj;
    public GameObject SiclorX5MiniObj;

    [Header("GameObjects Magazines")]
    public GameObject SmallRndObj;
    public GameObject MediumRndObj;
    public GameObject LargeRndObj;

    [Header("GameObjects Stocks")]
    public GameObject stockX1MiniObj;
    public GameObject stockX2_0LargeObj;

    [Header("GameObjects Grips")]
    public GameObject KV01Obj;

    int muzzle = 0;
    int magazine = 0;
    int stock = 0;
    int grip = 0;

    void Start(){
        muzzle = PlayerPrefs.GetInt("muzzle");
        magazine = PlayerPrefs.GetInt("magazine");
        stock = PlayerPrefs.GetInt("stock");
        grip = PlayerPrefs.GetInt("grip");

        if(muzzle == 0)
            addNoMuzzle();
        if (muzzle == 1)
            addSiclorX5Mini();
        if (muzzle == 2)
            addSiclorX6Large();
        if (magazine == 0)
            addRndSmall();
        if (magazine == 1)
            addRndMedium();
        if (magazine == 2)
            addRndLarge();
        if (stock == 0)
            addNoStock();
        if (stock == 1)
            addStockX1Mini();
        if (stock == 2)
            addStockX2_0Large();
        if (grip == 0)
            addNoGrip();
        if (grip == 1)
            addKV01();
    }

    public void addSiclorX6Large(){
        SiclorX6LargeObj.SetActive(true);
        SiclorX5MiniObj.SetActive(false);

        muzzle = 2;
        PlayerPrefs.SetInt("muzzle", muzzle);
    }
    public void addSiclorX5Mini(){
        SiclorX6LargeObj.SetActive(false);
        SiclorX5MiniObj.SetActive(true);

        muzzle = 1;
        PlayerPrefs.SetInt("muzzle", muzzle);
    }
    public void addNoMuzzle(){
        SiclorX6LargeObj.SetActive(false);
        SiclorX5MiniObj.SetActive(false);

        muzzle = 0;
        PlayerPrefs.SetInt("muzzle", muzzle);
    }

    public void addRndSmall(){
        SmallRndObj.SetActive(true);
        MediumRndObj.SetActive(false);
        LargeRndObj.SetActive(false);

        magazine = 0;
        PlayerPrefs.SetInt("magazine", magazine);
    }
    public void addRndMedium(){
        SmallRndObj.SetActive(false);
        MediumRndObj.SetActive(true);
        LargeRndObj.SetActive(false);

        magazine = 1;
        PlayerPrefs.SetInt("magazine", magazine);
    }
    public void addRndLarge(){
        SmallRndObj.SetActive(false);
        MediumRndObj.SetActive(false);
        LargeRndObj.SetActive(true);

        magazine = 2;
        PlayerPrefs.SetInt("magazine", magazine);
    }

    public void addStockX1Mini(){
        stockX1MiniObj.SetActive(true);
        stockX2_0LargeObj.SetActive(false);

        stock = 1;
        PlayerPrefs.SetInt("stock", magazine);
    }
    public void addStockX2_0Large(){
        stockX1MiniObj.SetActive(false);
        stockX2_0LargeObj.SetActive(true);

        stock = 2;
        PlayerPrefs.SetInt("stock", stock);
    }
    public void addNoStock(){
        stockX1MiniObj.SetActive(false);
        stockX2_0LargeObj.SetActive(false);

        stock = 0;
        PlayerPrefs.SetInt("stock", stock);
    }

    public void addKV01(){
        KV01Obj.SetActive(true);

        grip = 1;
        PlayerPrefs.SetInt("grip", grip);
    }
    public void addNoGrip(){
        KV01Obj.SetActive(false);

        grip = 0;
        PlayerPrefs.SetInt("grip", grip);
    }
}
