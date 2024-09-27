using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/New AI Config")]
public class AIConfig : ScriptableObject
{
    public float VisionRange;
    public float VisionConeAngle;
    public float CombatVisionRange;
    public float AttackRange;
    public float AttackTime;
    public float runSpeed;
    public float walkSpeed;
    public float sprintSpeed;
}
