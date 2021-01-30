using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedObjController : MonoBehaviour
{
    public Transform leftcontrol;
    public Transform rightcontrol;

    public List<GameObject> destPos = new List<GameObject>();
    private int index = 0;

    private GameObject player;
    private BoxCollider2D collider;

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

    public void MoveToNextPoint()
    {
        index = (index + 1) % destPos.Count;
        StartCoroutine(Move(destPos[index].transform.position));
        
    }

    IEnumerator Move(Vector3 destPos)
    {
        if(Equals(transform.position, destPos) == true)
        {
            yield break;
        }
        Vector3 start_pos = transform.position;
        float move_distance = 0;
        float move_speed = 1.0f;
        float move_ratio = 0;
        float total_distance = Vector3.Distance(start_pos, destPos);

        while(true)
        {
            if(Equals(transform.position, destPos))
            {
                yield break;
            }
            move_distance += move_speed * Time.deltaTime;
            move_ratio = move_distance / total_distance;
            Vector3 position = Vector3.Lerp(start_pos, destPos, move_ratio);
            transform.position = position;
            yield return null;
        }
    }
}
