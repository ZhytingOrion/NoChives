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
        ModeChoosePlane.SetActive(PlayerManager.Instance.isLogin);
        LoginPlane.SetActive(!PlayerManager.Instance.isLogin);
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
        //PlayerManager.Instance.playerName;

        ModeChoosePlane.SetActive(true);
        LoginPlane.SetActive(false);

        //delete
        PlayerManager.Instance.isLogin = true;
    }
}
