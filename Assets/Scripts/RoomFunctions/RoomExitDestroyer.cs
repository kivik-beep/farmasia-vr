﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExitDestroyer : MonoBehaviour {

    private void OnTriggerExit(Collider other) {
        GameObject gm = Interactable.GetInteractableObject(other.transform);
        if (gm == null) return;

        GeneralItem item = gm.GetComponent<GeneralItem>();
        if (item == null) return;

        if (MultiColliderTool.CheckCollision(gameObject, item.gameObject)) return;

        //Events.FireEvent(EventType.ItemDroppedOnFloor, CallbackData.Object(item));
        item.DestroyInteractable();
    }
}
