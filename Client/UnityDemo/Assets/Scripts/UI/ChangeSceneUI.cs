using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<Button>().onClick.AddListener()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterSingleMode()
    {
        NetworkManager.Instance.SendStartGame(PlayerManager.Instance.playerName, 1);
        SceneManager.LoadScene("Level_" + PlayerManager.Instance.nextProcess);
    }

    public void EnterMultiMode()
    {
        NetworkManager.Instance.SendStartGame(PlayerManager.Instance.playerName, 2);
        SceneManager.LoadScene("Level_" + PlayerManager.Instance.nextProcess);
    }

    public void ChangeScene(string NextSceneName)
    {
        SceneManager.LoadScene(NextSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPlayerName(Text text)
    {
        PlayerManager.Instance.SetPlayerName(text.text);
    }

    public void SetDefaultPic(Sprite sprite)
    {
        this.GetComponent<Image>().sprite = sprite;
    }
}
