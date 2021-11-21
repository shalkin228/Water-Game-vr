using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionItem : MonoBehaviour
{
    public static InteractionItem currentInteractionItemInstance;
    [HideInInspector] public bool isTaked;
    public InteractionItemType itemType;

    private void Start()
    {
        currentInteractionItemInstance = this;
    }

    public static void RemoveInteractionItem()
    {
        if (currentInteractionItemInstance == null)
            return;

        Destroy(currentInteractionItemInstance.gameObject);
    }
}
public enum InteractionItemType { MultiTool, Glider}
