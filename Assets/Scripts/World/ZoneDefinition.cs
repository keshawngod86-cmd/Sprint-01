using UnityEngine;

[DisallowMultipleComponent]
public class ZoneDefinition : MonoBehaviour
{
    [field: SerializeField] public ZoneType ZoneType { get; private set; } = ZoneType.None;

    private void Reset()
    {
        Collider2D zoneCollider = GetComponent<Collider2D>();
        if (zoneCollider != null)
        {
            zoneCollider.isTrigger = true;
        }
    }
}
