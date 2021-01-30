using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Color color;
    SpriteRenderer keySprite;
    bool hasPlayerInside = false;
    int moveDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        keySprite = this.transform.Find("key_pic").GetComponent<SpriteRenderer>();
        keySprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasPlayerInside)
        {
            if (keySprite != null)
                keySprite.transform.localPosition += new Vector3(0, Time.deltaTime*0.5f, 0) * moveDir;

            if (keySprite.transform.localPosition.y > 0.2f)
                moveDir = -1;
            else if (keySprite.transform.localPosition.y < -0.2f)
                moveDir = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            hasPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            hasPlayerInside = false;
            if (keySprite)
                keySprite.transform.localPosition = Vector3.zero;
        }
    }

}
