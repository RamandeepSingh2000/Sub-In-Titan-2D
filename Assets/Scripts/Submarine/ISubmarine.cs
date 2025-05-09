using UnityEngine;

public interface ISubmarine
{
    public Transform Transform { get; }
    public Vector2 GetRelativeAttackPosition();
    public void Damage(int amount);
    public void AddAttachedEnemy(IEnemyEffect enemyEffect);
    public void RemoveAttachedEnemy(IEnemyEffect enemyEffect);
}
