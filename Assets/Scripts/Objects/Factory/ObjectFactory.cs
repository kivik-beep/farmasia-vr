﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour {

    #region Fields
    public GameObject CopyObject { get; set; }
    private GameObject latestCopy;
    private Interactable interactable;

    private GameObject lastPicked;

    public ColliderHitCount triggerColliderCount;
    #endregion

    private void Start() {
        CreateColliderCopy();
        CreateNewCopy();
    }


    #region Collider initialiazation

    private void CreateColliderCopy() {

        GameObject triggerCopy = new GameObject();

        triggerCopy.transform.position = CopyObject.transform.position;
        triggerCopy.transform.rotation = CopyObject.transform.rotation;
        triggerCopy.transform.localScale = CopyObject.transform.localScale;
        triggerCopy.transform.name = CopyObject.transform.name + " (TriggerCopy)";

        triggerColliderCount = triggerCopy.AddComponent<ColliderHitCount>();

        RecursiveCopyColliders(CopyObject.transform, triggerCopy.transform);
    }

    private void RecursiveCopyColliders(Transform original, Transform copy) {

        Collider coll = original.GetComponent<Collider>();
        if (coll != null) {
            ComponentCopy.Copy<Collider>(coll, copy.gameObject).isTrigger = true;
            triggerColliderCount.SubscribeToCollisions(copy.gameObject);
        }

        foreach (Transform originalChild in original) {

            GameObject copyChild = new GameObject();
            copyChild.transform.parent = copy.transform;
            CopyTransform(originalChild, copyChild.transform);

            RecursiveCopyColliders(originalChild, copyChild.transform);
        }
    }
    private void CopyTransform(Transform from, Transform to) {
        to.transform.localPosition = from.transform.localPosition;
        to.transform.localRotation = from.transform.localRotation;
        to.transform.localScale = from.transform.localScale;
        to.transform.name = from.transform.name + " (TriggerCopy)";
    }
    #endregion

    private void Update() {
        if (interactable.State == InteractState.Grabbed) {
            latestCopy.GetComponent<Rigidbody>().isKinematic = false;
            CreateNewCopy();
        }
    }

    private IEnumerator CheckCollisionRelease(GameObject g1, GameObject g2, Interactable currentInteractable) {
        yield return null;

        while (triggerColliderCount.Inside) {
            if (currentInteractable != interactable) {
                yield break;
            }
            yield return null;
        }

        CollisionIgnore.IgnoreCollisions(g1.transform, g2.transform, false);
    }

    private void CreateNewCopy() {
        GameObject handObject = latestCopy;

        if (latestCopy != null) {
            triggerColliderCount.SetInteractable(handObject);

            if (latestCopy.GetComponent<Rigidbody>().isKinematic == true) {
                Destroy(latestCopy);
            }
        }

        latestCopy = Instantiate(CopyObject);
        latestCopy.transform.position = transform.position;

        interactable = latestCopy;
        latestCopy.SetActive(true);

        if (handObject != null) {
            CollisionIgnore.IgnoreCollisions(handObject.transform, latestCopy.transform, true);
            StartCoroutine(CheckCollisionRelease(handObject, latestCopy, interactable));
        }
        if (lastPicked != null) {
            CollisionIgnore.IgnoreCollisions(lastPicked.transform, handObject.transform, false);
        }
        lastPicked = handObject;
    }
}
