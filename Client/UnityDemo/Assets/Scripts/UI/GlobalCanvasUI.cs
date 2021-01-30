using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCanvasUI : MonoBehaviour
{
    public Text TKeyNum;
    public Image SUsefulKey;
    public Text TBottleNum;

    // Start is called before the first frame update
    void Start()
    {
        HideUsefulKey();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBottleNum(int num)
    {
        TBottleNum.text = num.ToString();
    }

    public void SetKeyNum(int num)
    {
        TKeyNum.text = num.ToString();
    }

    public void ShowUsefulKey(Color color)
    {
        SUsefulKey.color = color;
        SUsefulKey.enabled = true;
    }

    public void HideUsefulKey()
    {
        SUsefulKey.enabled = false;
    }

}
