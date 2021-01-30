using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreAnimUI : MonoBehaviour
{

    public List<Sprite> changeSprites = new List<Sprite>();
    public Image image;
    public float changeTime = 0.5f;
    private int index = 0;
    private float timeLeft;

    public float fadeOutTime = 2.0f;
    public float stayTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = changeSprites[0];
        timeLeft = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= changeSprites.Count-1)
            return;

        if (timeLeft > 0.0f)
            timeLeft -= Time.deltaTime;
        else
        {
            timeLeft = changeTime;
            index++;
            image.sprite = changeSprites[index];
        }

        if(index == changeSprites.Count - 1)
        {
            StartCoroutine(Fadeout(fadeOutTime, stayTime));
        }
    }

    IEnumerator Fadeout(float time = 1.0f, float stayTime = 1.0f)
    {
        for(float t = 0.0f; t<time; t+=Time.deltaTime)
        {
            image.color = new Color(1, 1, 1, 1.0f - Mathf.Clamp01(t / time));
            yield return null;
        }

        yield return new WaitForSeconds(stayTime);

        SceneManager.LoadScene("MainScene");
        
    }
}
