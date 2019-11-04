﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildConnection : ItemConnection {

    protected override ItemConnector Connector { get; set; }
    private Transform target;
    private Interactable interactable;

    private void Awake() {
        Logger.Print("Child connection awake");
        interactable = GetComponent<Interactable>();
        interactable.RigidbodyContainer.Disable();
        SafeSetParent(target, transform);
    }

    public static ChildConnection Configuration(ItemConnector connector, Transform target, GameObject addTo) {

        Logger.Print("Child connection config");

        ChildConnection conn = addTo.AddComponent<ChildConnection>();

        conn.Connector = connector;
        conn.target = target;

        return conn;
    }

    protected override void OnDestroy() {
        interactable.RigidbodyContainer.EnableAndDeparent();
        Connector.OnReleaseItem();
    }

    public static void SafeSetParent(Transform parent, Transform child) {
        Logger.PrintVariables("set", "variables", "parent", parent.name, "child", child.name);
        child.SetParent(parent);
    }
}
