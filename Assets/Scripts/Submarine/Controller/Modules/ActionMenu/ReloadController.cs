using UnityEngine;

namespace Submarine
{
    public class ReloadController : SubmarineProcess
    {
        [SerializeField] private AttackModule _attackController;

        public override void StartProcess()
        {
            float timeMultiplier = (float)_attackController.GetCannonAmmo() / (float)_attackController.GetMaxCannonAmmo();
            Debug.Log(timeMultiplier);
            processTime = fullProcessDuration * timeMultiplier;
            base.StartProcess();
        }

        public override void CancelProcess()
        {
            base.CancelProcess();
            int ammo = (int)(_attackController.GetMaxCannonAmmo() * GetProgress());
            _attackController.ReloadCannon(ammo);
        }

        protected override void FinishProcess()
        {
            _attackController.ReloadCannon(_attackController.GetMaxCannonAmmo());
            base.FinishProcess();            
        }

        protected override void Update()
        {
            base.Update();
            if (IsPerformingProcess)
            {
                int ammo = (int)(_attackController.GetMaxCannonAmmo() * GetProgress());                
                _attackController.ReloadCannon(ammo);
            }
        }
    }
}