using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TPlus.AI
{
    public class DetectableObjectManager : MonoBehaviour
    {
        public static DetectableObjectManager Instance;

        private List<AI_Base> _detectableObjects = new List<AI_Base>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        public void RegisterDetectableObject(AI_Base obj)
        {
            if (!_detectableObjects.Contains(obj))
            {
                Debug.Log(obj.gameObject.name + " has been registered");
                _detectableObjects.Add(obj);
            }
        }

        public void UnregisterDetectableObject(AI_Base obj)
        {
            if (_detectableObjects.Contains(obj))
            {
                Debug.Log(obj.gameObject.name + " has been unregistered");
                _detectableObjects.Remove(obj);
            }
        }

        public List<AI_Base> GetAllDetectableObjects()
        {
            return _detectableObjects;
        }
    }
}

