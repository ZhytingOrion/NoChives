using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Color color;
    private Transform player;
    public float destScale = 0.3f;
    public float animTime = 1.0f;
    public float animStayTime = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = DataTransfer.Instance.GetBottleSprite(color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoldBottle(Transform player)
    {
        this.player = player.Find("head");
        StartCoroutine(BottleQuitAnim());
    }

    IEnumerator BottleQuitAnim()
    {
        Vector3 originScale = this.transform.localScale;
        Vector3 finalScale = originScale * destScale;
        Vector3 originLoc = this.transform.position;
        Cinemachine.CinemachineVirtualCamera vc = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        float originCameraOrthoSize = vc.m_Lens.OrthographicSize;
        float destCameraOrthoSize = 2.5f;

        for(float t = 0; t<animTime; t+=Time.deltaTime)
        {
            this.transform.localScale = Vector3.Lerp(originScale, finalScale, t / animTime);
            this.transform.position = Vector3.Lerp(originLoc, player.position, t / animTime);
            vc.m_Lens.OrthographicSize = Mathf.Lerp(originCameraOrthoSize, destCameraOrthoSize, t / animTime);
            yield return null;
        }
        this.transform.position = player.position;
        this.transform.localScale = finalScale;
        vc.m_Lens.OrthographicSize = destCameraOrthoSize;

        for(float t = 0; t<animStayTime; t+=Time.deltaTime)
        {
            yield return null;
        }

        //send message

        player.parent.GetComponent<GamePlayer>().CollectAim(this);

        Destroy(gameObject);
    }
}
