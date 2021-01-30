using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    public GameObject specialFriend;
    public GameObject cloth_Orange;
    public GameObject cloth_Green;
    public GameObject cloth_Purple;

    // Start is called before the first frame update
    void Start()
    {
        List<Color> colors = PlayerManager.Instance.collectedColor;
        cloth_Green.SetActive(colors.Contains(Color.green));
        cloth_Purple.SetActive(colors.Contains(Color.magenta));
        cloth_Orange.SetActive(colors.Contains(new Color(1, 0.5f, 0)));
        specialFriend.SetActive(colors.Count >= 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        SceneManager.LoadScene("Level_0");

        //send message
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("MainScene");

        //send message
    }
}
