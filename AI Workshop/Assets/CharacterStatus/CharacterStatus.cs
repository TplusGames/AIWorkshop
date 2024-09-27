using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPlus.Status
{
    public class CharacterStatus : MonoBehaviour
    {
        private bool _dead;

        [SerializeField] private int maxHealth;
        public HealthComponent HealthComponent { get; private set; }

        private int _currentStamina;
        [SerializeField] protected int maxStamina;

        public void InitializeCharacterStatus()
        {
            HealthComponent = new HealthComponent();
            HealthComponent.InitializeComponent(maxHealth);
        }
    }
}

