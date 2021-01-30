using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    public bool isLogin = false;

    public List<Color> collectedColor = new List<Color>();
    public List<Color> collectedKeys = new List<Color>();
    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SceneManager.activeSceneChanged += SetPlayerState;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerState(Scene oldScene, Scene newScene)
    {
        GameObject player = null;
        player = GameObject.Find("Player");
        if(player != null)
        {
            GamePlayer gp = player.GetComponent<GamePlayer>();
            if(gp != null)
            {
                gp.collectedColors = collectedColor;
                gp.collectedKeys = collectedKeys;
                gp.playerName.text = playerName;
                SyncPlayerState(gp);
            }
        }
    }

    public void SyncPlayerState(GamePlayer gp)
    {
        collectedColor = gp.collectedColors;
        collectedKeys = gp.collectedKeys;

        GameObject LevelCanvas = null;
        LevelCanvas = GameObject.Find("LevelCanvas");
        if(LevelCanvas!=null)
        {
            GlobalCanvasUI lc = LevelCanvas.GetComponent<GlobalCanvasUI>();
            lc.SetBottleNum(collectedColor.Count);
            lc.SetKeyNum(collectedKeys.Count);
        }
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }
}
