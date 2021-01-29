using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public Color color;
    public GameObject highlight = null;
    public GameObject door = null;

    private float alpha = 1.0f;
    private int alphaDir = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (highlight == null)
            highlight = transform.Find("highlight").gameObject;

        if (highlight != null)
            highlight.SetActive(false);

        if (door == null)
            door = transform.Find("door").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        highlight.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, alpha);
        alpha += alphaDir * Time.deltaTime;
        
        if(alpha < 0.0f)
        {
            alphaDir = 1;
            alpha = 0.0f;
        }
        else if(alpha > 1.0f)
        {
            alphaDir = -1;
            alpha = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if(collidedObject.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (highlight != null)
            {
                highlight.SetActive(true);
                highlight.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (highlight != null)
            {
                highlight.SetActive(false);
            }
        }
    }

    public Color OnOpened()
    {
        if (highlight != null)
            highlight.SetActive(false);
        if(door!=null)
        {
            door.GetComponent<SpriteRenderer>().color = color;
        }
        return color;
    }
}
