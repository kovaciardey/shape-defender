using UnityEngine;

namespace UI
{
    public class SummoningMenu : MonoBehaviour
    {
        public SummonMenuOption[] options;
        
        // Start is called before the first frame update
        void Start()
        {
            Reset();
        }

        public void SelectOption(int optionId)
        {
            Reset();
            
            options[optionId].Select();
        }

        public void Reset()
        {
            foreach (SummonMenuOption option in options)
            {
                option.Unselect();
            }
        }
    }
}
