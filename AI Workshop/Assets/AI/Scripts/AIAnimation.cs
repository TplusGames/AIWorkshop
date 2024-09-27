using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private NavMeshAgent _agent;
    private bool _dead;

    public void SetAgent(NavMeshAgent agent)
    {
        _agent = agent;
    }
    
    public void SynchAnimationsToVelocity()
    {
        if (_dead) return;
        animator.SetFloat("Velocity", _agent.velocity.magnitude);
    }

    public void Die()
    {
        _dead = true;
        animator.enabled = false;
    }

    public void Revive()
    {
        animator.enabled = true;
        _dead = false;
    }

    public void SetAnimationController(AnimatorController controller)
    {
        animator.runtimeAnimatorController = controller;
    }
}
