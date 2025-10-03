using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.HealthSystemCM {

    /// <summary>
    /// Deal Damage to the HealthSystem on Click
    /// </summary>
    public class Demo_DamageButton : MonoBehaviour {

        [SerializeField] private GameObject getHealthSystemGameObject;

        private void Start() {
            HealthSystem.TryGetHealthSystem(getHealthSystemGameObject, out HealthSystem healthSystem, true);

            
                healthSystem.Damage(10);
            
        }

    }

}