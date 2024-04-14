using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private Slider _healthSlider;

        public int maximumValue;
        
        void Start()
        {
            _healthSlider = GetComponent<Slider>();
            
            _healthSlider.maxValue = maximumValue;
        }

        public void UpdateMaximumValue(float value)
        {
            _healthSlider.maxValue = value;
        }

        public void SetValue(float value)
        {
            _healthSlider.value = value;
        }
    }
}
