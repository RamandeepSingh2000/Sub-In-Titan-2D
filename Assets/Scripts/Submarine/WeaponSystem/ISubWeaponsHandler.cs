using UnityEngine;

public interface ISubWeaponsHandler
{
    public bool CanFireCannon { get; }
    public bool CanUseAOE { get; }
    public bool IsAOEReady { get; }
    public int CurrentCannonAmmo { get; }
    public int MaxAmmo { get; }
    public float AOECooldownTimeLeft { get; }

    public void SetCannonAimDirection(Vector2 direction);
    public void FireCannon();
    public void UseAOE();
    public void ReloadCannon(int ammo);
    public void ChargeAOE();
    public void CancelAOE();    
}
