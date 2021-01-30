using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCanvasUI : MonoBehaviour
{
    //public Text TKeyNum;
    //public Image SUsefulKey;
    //public Text TBottleNum;

    public List<GameObject> collectedBottles = new List<GameObject>();
    public float bottlesLerp = 75.0f;
    public Transform bottleParent;

    public List<GameObject> collectedKeys = new List<GameObject>();
    public float keysLerp = -70.0f;
    public Transform keysParent;

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
        //TBottleNum.text = num.ToString();
        for (int i = 0; i < num; i++)
        {
            if (i >= collectedBottles.Count)
            {
                GameObject bottle = new GameObject("bottleImg", typeof(Image));
                bottle.transform.parent = bottleParent;
                bottle.transform.localPosition = new Vector3(bottlesLerp * i, 0, 0);
                collectedBottles.Add(bottle);
            }
            collectedBottles[i].GetComponent<Image>().sprite = DataTransfer.Instance.GetBottleSprite(PlayerManager.Instance.collectedColor[i]);
        }
        if(collectedBottles.Count > num)
        {
            for(int i = collectedBottles.Count; i>num; i--)
            {
                Destroy(collectedBottles[i-1]);
                collectedBottles.RemoveAt(i - 1);
            }
        }
    }

    public void SetKeyNum(int num)
    {
        //TKeyNum.text = num.ToString();
        for (int i = 0; i < num; i++)
        {
            if (i >= collectedKeys.Count)
            {
                GameObject obj = new GameObject("keyImg", typeof(Image));
                obj.transform.parent = keysParent;
                obj.transform.localPosition = new Vector3(keysLerp * i, 0, 0);
                collectedKeys.Add(obj);
            }
            collectedKeys[i].GetComponent<Image>().sprite = DataTransfer.Instance.GetKeySprite(PlayerManager.Instance.collectedKeys[i]);
        }
        if (collectedKeys.Count > num)
        {
            for (int i = collectedKeys.Count; i > num; i--)
            {
                Destroy(collectedKeys[i - 1]);
                collectedKeys.RemoveAt(i - 1);
            }
        }
    }

    public void ShowUsefulKey(Color color)
    {
        //SUsefulKey.color = color;
        //SUsefulKey.enabled = true;
    }

    public void HideUsefulKey()
    {
        //SUsefulKey.enabled = false;
    }

}
