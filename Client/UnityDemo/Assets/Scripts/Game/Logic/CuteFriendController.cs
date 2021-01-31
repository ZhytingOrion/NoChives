using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteFriendController : MonoBehaviour
{
    public Transform player;
    public Transform player_body;
    public Transform myLocR;
    public Transform myLocL;

    public bool moveObj = false;
    private Vector3 aimLoc = Vector3.zero;
    private bool isFollowing = false;
    private Vector3 randomOffset;
    private Transform sprite;
    private GameObject push_Object;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("player").transform;
        if (player_body == null)
            player_body = player.Find("Body");
        myLocR = player.Find("CuteFriendLocR");
        myLocL = player.Find("CuteFriendLocL");
        sprite = this.transform.Find("Sprite");
        aimLoc = player.position + new Vector3(0, 0.5f, 0);
        StartCoroutine(setRandomOffset());
    }

    // Update is called once per frame
    void Update()
    {
        if(!moveObj)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, aimLoc, Time.deltaTime*0.75f);
            if(player_body.localScale.x <0)
            {
                aimLoc = myLocL.position + new Vector3(-randomOffset.x, randomOffset.y, randomOffset.z);
            }
            else
            {
                aimLoc = myLocR.position + randomOffset;
            }

            sprite.localScale = new Vector3(this.transform.position.x < player.transform.position.x ? -1 : 1, 1, 1) * 0.35f;
        }
    }

    IEnumerator setRandomOffset()
    {
        while (true)
        {
            randomOffset = new Vector3(Random.Range(0, 0.8f), Random.Range(-0.5f, 0.5f), 0);
            yield return new WaitForSeconds(1.0f);
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void PushObject(GameObject obj, float time)
    {
        StopAllCoroutines();
        moveObj = true;
        push_Object = obj;
        Debug.Log("push_obj " + obj.name);
        StartCoroutine(flytoObj(time));
    }

    IEnumerator flytoObj(float time)
    {
        for(float t = 0.0f; t<time; t+=Time.deltaTime)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, push_Object.transform.position, t / time);
            yield return null;
        }

        while(moveObj)
        {
            this.transform.position = push_Object.transform.position;
            yield return null;
        }
    }

    public void EndPush()
    {
        StopAllCoroutines();
        moveObj = false;
        StartCoroutine(setRandomOffset());
    }
}
