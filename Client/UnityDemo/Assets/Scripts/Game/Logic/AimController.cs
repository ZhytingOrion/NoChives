﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Color color;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.Find("sprite").GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}