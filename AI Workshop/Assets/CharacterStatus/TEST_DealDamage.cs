using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_DealDamage : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<BodyPart>().TakeDamage(5, this);
        }

        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<BodyPart>().Heal(2, this);
        }
    }
}
