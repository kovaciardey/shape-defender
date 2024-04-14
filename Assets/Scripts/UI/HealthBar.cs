using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Slider healthSlider;

        public void UpdateMaximumValue(float value)
        {
            healthSlider.maxValue = value;
        }

        public void SetValue(float value)
        {
            healthSlider.value = value;
        }
    }
}
