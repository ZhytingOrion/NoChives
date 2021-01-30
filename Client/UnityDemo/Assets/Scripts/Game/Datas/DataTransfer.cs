﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    [System.Serializable]
    public struct ColorPaths
    {
        public Color color;
        public Sprite SBottle;
        public Sprite SDoor;
        public Sprite SDoorHightlight;
        public Sprite SKey;
    }
    public List<ColorPaths> Colors = new List<ColorPaths>();

    public static DataTransfer Instance = null;

    void Start()
    {
        DataTransfer.Instance = this;
    }

    public Sprite GetBottleSprite(Color color)
    {
        ColorPaths cp = Colors.Find(x => x.color == color);
        if (cp.color == color)
        {
            return cp.SBottle;
        }
        else return null;
    }

    public Sprite GetDoorSprite(Color color)
    {
        ColorPaths cp = Colors.Find(x => x.color == color);
        if (cp.color == color)
        {
            return cp.SDoor;
        }
        else return null;
    }

    public Sprite GetDoorHLSprite(Color color)
    {
        ColorPaths cp = Colors.Find(x => x.color == color);
        if (cp.color == color)
        {
            return cp.SDoorHightlight;
        }
        else return null;
    }

    public Sprite GetKeySprite(Color color)
    {
        ColorPaths cp = Colors.Find(x => x.color == color);
        if (cp.color == color)
        {
            return cp.SKey;
        }
        else return null;
    }
}
