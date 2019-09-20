﻿using UnityEngine;

public class Syringe : GeneralItem {

    [SerializeField]
    private int size = 50;
    public int Size {
        get => size;
        set {
            if (size >= 0)
                size = value;
        } 
    }

    [SerializeField]
    private string content = "";
    public string Content {
        get => content;
        set { content = value; }
    }

    [SerializeField]
    private int contentLeft = 0;
    public int ContentLeft {
        get => contentLeft;
        set {
            if (value <= size && value > 0)
                contentLeft = value;
        }
    }

    void Start() {
        base.Start();
        objectType = ObjectType.Syringe;
    }
}
