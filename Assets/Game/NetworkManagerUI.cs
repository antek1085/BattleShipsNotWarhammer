using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] Button hostBtn, clientBtn, tutorialBtn;

    [SerializeField] GameObject buttons, tutorial,canvas;
    
    // Start is called before the first frame update

    void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            Destroy(canvas);
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            Destroy(canvas);
        });
    }

    public void Tutorial()
    {
        tutorial.SetActive(!tutorial.activeSelf);
        buttons.SetActive(!buttons.activeSelf);
    }
}
