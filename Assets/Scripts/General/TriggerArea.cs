using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    [SerializeField] private LayerMask _layer;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer) == _layer.value)
            OnTriggerEnter?.Invoke();
    }
}
