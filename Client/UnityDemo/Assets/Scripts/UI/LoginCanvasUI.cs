using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCanvasUI : MonoBehaviour
{
    public GameObject ModeChoosePlane;
    public GameObject LoginPlane;

    // Start is called before the first frame update
    void Start()
    {
        bool isLogin = false;
        if (PlayerManager.Instance != null)
            isLogin = PlayerManager.Instance.isLogin;
        ModeChoosePlane.SetActive(isLogin);
        LoginPlane.SetActive(!isLogin);
    }

    public void BackToLogin()
    {
        ModeChoosePlane.SetActive(false);
        LoginPlane.SetActive(true);
        
        //disconnect
    }

    public void ChooseMode(int index)   //0:solo  1:multi
    {
        //send message

    }

    public void RegisterOrLogin(int index)  //0:register 1:login in
    {
        //send message
        if (index == 0)
            NetworkManager.Instance.SendRegister(PlayerManager.Instance.playerName);
        else
            NetworkManager.Instance.SendLogin(PlayerManager.Instance.playerName);
        //PlayerManager.Instance.playerName;

        ModeChoosePlane.SetActive(true);
        LoginPlane.SetActive(false);

        //delete
        PlayerManager.Instance.isLogin = true;
    }
}
