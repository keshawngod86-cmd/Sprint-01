using UnityEngine;

public class PlayerGroundSensor : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundLayers = ~0;
    [SerializeField] private float checkRadius = GameConstants.DefaultGroundCheckRadius;

    public bool IsGrounded { get; private set; }

    private void Reset()
    {
        groundCheckPoint = transform;
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        Vector2 origin = groundCheckPoint != null ? groundCheckPoint.position : transform.position;
        IsGrounded = Physics2D.OverlapCircle(origin, checkRadius, groundLayers) != null;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = groundCheckPoint != null ? groundCheckPoint.position : transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, checkRadius);
    }
}
