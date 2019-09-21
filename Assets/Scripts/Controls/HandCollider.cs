﻿using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour {

    #region fields
    private Hand hand;
    private static string iTag = "Interactable";
    public HashSet<GameObject> GrabObjects { get; private set; }
    #endregion

    private void Start() {
        GrabObjects = new HashSet<GameObject>();
    }

    public Hand Hand {
        get {
            if (hand == null) {
                hand = transform.parent.GetComponent<Hand>();
            }
            return hand;
        }
    }

    public void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag != iTag) {
            return;
        }

        GrabObjects.Add(coll.gameObject);

        ObjectHighlight hObject = coll.gameObject.GetComponent<ObjectHighlight>();
        hObject.StartCoroutine(hObject.InsideCheck(this));
    }
    public void OnTriggerExit(Collider coll) {
        if (coll.gameObject.tag != iTag) {
            return;
        }

        GrabObjects.Remove(coll.gameObject);
    }

    public bool Contains(GameObject obj) {
        return GrabObjects.Contains(obj);
    }

    public Interactable GetGrab() {
        GameObject o = GetGrabObject();
        return o?.GetComponent<Interactable>() ?? null;
    }

    public GameObject GetGrabObject() {
        float closestDistance = float.MaxValue;
        GameObject closest = null;

        foreach (GameObject rb in GrabObjects) {
            float distance = Vector3.Distance(transform.position, rb.transform.position);

            if (distance < closestDistance) {
                closestDistance = distance;
                closest = rb;
            }
        }

        return closest;
    }
}
