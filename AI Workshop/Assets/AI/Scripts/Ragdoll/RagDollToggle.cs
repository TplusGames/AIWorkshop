using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollToggle : MonoBehaviour
{
    public void ToggleRagdoll(bool isActive)
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        if (isActive)
        {
            foreach (var rb in rigidbodies)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            return;
        }
        foreach (var rb in rigidbodies)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
