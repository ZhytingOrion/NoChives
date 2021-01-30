using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public Vector4 boundsLimit; //up down left right

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
