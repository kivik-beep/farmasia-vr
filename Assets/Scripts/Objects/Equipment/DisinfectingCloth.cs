using UnityEngine;
using UnityEngine.Assertions;

public class DisinfectingCloth : GeneralItem {

    #region Fields
    [SerializeField]
    private GameObject clothCollider;
    #endregion

    protected override void Start() {
        base.Start();

        ObjectType = ObjectType.DisinfectingCloth;
        IsClean = true;
        Type.On(InteractableType.Interactable, InteractableType.SmallObject);
        CollisionSubscription.SubscribeToTrigger(clothCollider, new TriggerListener().OnEnter(collider => TouchCloth(collider)));
    }

    private void TouchCloth(Collider other) {
        GameObject foundObject = GetInteractableObject(other.transform);
        GeneralItem item = foundObject?.GetComponent<GeneralItem>();
        if (item == null) {
            return;
        }
        if (item.ObjectType == ObjectType.Bottle && this.IsClean) {
            MedicineBottle bottle = item as MedicineBottle;
            if (!bottle.IsClean) {
                bottle.IsClean = true;
                UISystem.Instance.CreatePopup("Lääkepullon korkki puhdistettu.", MsgType.Notify);
            } else {
                UISystem.Instance.CreatePopup("Lääkepullon korkki oli jo puhdas.", MsgType.Notify);
            }
        }
    }
}