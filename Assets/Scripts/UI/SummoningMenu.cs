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

        // Update is called once per frame
        void Update()
        {
        
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
