using UnityEngine;

namespace Submarine
{
    public abstract class BaseModule : MonoBehaviour
    {
        public bool IsEnabled { get; private set; } = true;
        
        public virtual void EnableModule()
        {
            this.enabled= true;
            IsEnabled = true;
        }

        public virtual void DisableModule() 
        {
            this.enabled = false;
            IsEnabled = false;
        }
    }
}