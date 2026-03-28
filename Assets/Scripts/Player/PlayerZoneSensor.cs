using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneSensor : MonoBehaviour
{
    [SerializeField] private Transform samplePoint;

    private readonly HashSet<ZoneType> sampledZones = new HashSet<ZoneType>();
    private readonly Collider2D[] results = new Collider2D[16];

    private void Reset()
    {
        samplePoint = transform;
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        sampledZones.Clear();

        Vector2 origin = samplePoint != null ? samplePoint.position : transform.position;
        int hitCount = Physics2D.OverlapPointNonAlloc(origin, results);
        for (int i = 0; i < hitCount; i++)
        {
            Collider2D hit = results[i];
            if (hit == null)
            {
                continue;
            }

            ZoneType zoneType = ResolveZoneType(hit);
            if (zoneType != ZoneType.None)
            {
                sampledZones.Add(zoneType);
            }
        }
    }

    public bool IsInZone(ZoneType zoneType)
    {
        return sampledZones.Contains(zoneType);
    }

    private static ZoneType ResolveZoneType(Collider2D hit)
    {
        ZoneDefinition zoneDefinition = hit.GetComponent<ZoneDefinition>();
        if (zoneDefinition != null)
        {
            return zoneDefinition.ZoneType;
        }

        return hit.tag switch
        {
            "Road" => ZoneType.Road,
            "Water" => ZoneType.Water,
            "Cliff" => ZoneType.Cliff,
            "Blizzard" => ZoneType.Blizzard,
            "Obstacle" => ZoneType.Obstacle,
            _ => ZoneType.None
        };
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = samplePoint != null ? samplePoint.position : transform.position;
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(origin, 0.06f);
    }
}
