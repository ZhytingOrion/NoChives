using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedObjController : MonoBehaviour
{
    public Transform leftcontrol;
    public Transform rightcontrol;
    public float MoveDistance = 1.0f;

    public List<GameObject> destPos = new List<GameObject>();
    private int index = 0;

    private GameObject player;
    private BoxCollider2D collider;
    private CuteFriendController push_cf = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("foot").gameObject;
        collider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            collider.isTrigger = false;
        }
        else
            collider.isTrigger = true;
    }

    void OnMouseDown()
    {
        float waittime = 1.0f;
        Vector3 dir;
        //Debug.Log("OnMouseDown response" + name);
        push_cf = GameObject.FindObjectOfType<CuteFriendController>();
        if(push_cf.transform.position.x < this.transform.position.x)
        {
            push_cf.PushObject(leftcontrol.gameObject, waittime);
            dir = new Vector3(1, 0, 0);
        }
        else
        {
            push_cf.PushObject(rightcontrol.gameObject, waittime);
            dir = new Vector3(-1, 0, 0);
        }
        StartCoroutine(Move(waittime, this.transform.position + dir * MoveDistance));
    }

    public void MoveToNextPoint()
    {
        index = (index + 1) % destPos.Count;
        StartCoroutine(Move(0.0f, destPos[index].transform.position));
        
    }

    IEnumerator Move(float waitTime, Vector3 destPos)
    {
        yield return new WaitForSeconds(waitTime);
        if(Equals(transform.position, destPos) == true)
        {
            yield break;
        }
        Vector3 start_pos = transform.position;
        float move_distance = 0;
        float move_speed = 0.75f;
        float move_ratio = 0;
        float total_distance = Vector3.Distance(start_pos, destPos);
        this.GetComponent<AudioSource>().Play();

        while(true)
        {
            if(Equals(transform.position, destPos))
            {
                break;
            }
            move_distance += move_speed * Time.deltaTime;
            move_ratio = move_distance / total_distance;
            Vector3 position = Vector3.Lerp(start_pos, destPos, move_ratio);
            transform.position = position;
            yield return null;
        }
        this.GetComponent<AudioSource>().Stop();

        if (push_cf != null)
            push_cf.EndPush();
    }
}
