using TPlus.AI;
using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    public delegate void EnemySpotted(AI_Base target);
    public event EnemySpotted OnEnemySpotted;

    [SerializeField] private AIConfig aiConfig;

    private float _cosVisionConeAngle;

    private AI_Base _ai;

    public void InitializeVisionSensor(AI_Base ai)
    {
        _ai = ai;
        _cosVisionConeAngle = Mathf.Cos(aiConfig.VisionConeAngle * Mathf.Deg2Rad);
    }
    
    public void PerformVisionScan()
    {
        for (int i = 0; i < DetectableObjectManager.Instance.GetAllDetectableObjects().Count; i++)
        {
            var detectable = DetectableObjectManager.Instance.GetAllDetectableObjects()[i];

            if (detectable.gameObject == this.gameObject)
                continue;

            if (detectable == null)
            {
                continue;
            }

            if (detectable.IsDead())
                continue;

            if (!_ai.IsHostile(detectable))
                continue;

            var vectorToTarget = detectable.transform.position - transform.position;

            if (vectorToTarget.sqrMagnitude > aiConfig.VisionRange * aiConfig.VisionRange)
            {
                continue;
            }

            if (Vector3.Dot(vectorToTarget.normalized, transform.forward) < _cosVisionConeAngle)
            {
                continue;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, vectorToTarget, out hit))
            {
                
                var target = hit.transform.GetComponent<IAIAttachment>();
                if (target != null && _ai.IsHostile(detectable))
                {
                    Debug.Log(gameObject.name + " can see " + detectable.gameObject.name);
                    Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

                    OnEnemySpotted?.Invoke(detectable);
                }
            }
        }
    }
}
