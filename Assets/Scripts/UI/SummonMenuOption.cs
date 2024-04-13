using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SummonMenuOption : MonoBehaviour
    {
        public Image selected;
    
        // Start is called before the first frame update
        void Start()
        {
            Unselect();
        }

        public void Select()
        {
            selected.enabled = true;
        }

        public void Unselect()
        {
            selected.enabled = false;
        }
    }
}
