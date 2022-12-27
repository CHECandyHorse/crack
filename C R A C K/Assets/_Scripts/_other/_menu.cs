using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _menu : MonoBehaviour{
    public GameObject background;

    public GameObject moddingTab;
    public GameObject moddingObj;

    public GameObject createLobbyButton;
    public GameObject moddingButton;
    public GameObject moddingBackButton;

    public void openModding(){  
        createLobbyButton.SetActive(false);
        moddingBackButton.SetActive(true);
        moddingButton.SetActive(false);

        background.SetActive(true);
        moddingTab.SetActive(true);
        moddingObj.SetActive(true);
    }
    public void closeModding(){
        createLobbyButton.SetActive(true);
        moddingButton.SetActive(true);
        moddingBackButton.SetActive(false);

        background.SetActive(false);
        moddingTab.SetActive(false);
        moddingObj.SetActive(false);
    }
}
