using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class GroundChecker : MonoBehaviour
{
    private const float GROUND_CHECK_DISTANCE = 0.08f;
    public const float MIN_GROUND_NORMAL_Y = 0.5f;

    private CapsuleCollider _capsuleCollider;
    private LayerMask _groundLayers;

    public bool IsGrounded { get; private set; }
    public Vector3 GroundNormal { get; private set; } = Vector3.up;

    private void Awake() => CacheComponent();

    public void CheckGround(bool isJumping)
    {
        ClearGround();
        // 점프 상승 중에는 발밑에 지면이 남아 있어도 다시 접지하지 않는다.
        if (isJumping) return;

        Bounds bounds = _capsuleCollider.bounds;
        float radius = Mathf.Min(bounds.extents.x, bounds.extents.z) * 0.95f;
        float distance = bounds.extents.y - radius + GROUND_CHECK_DISTANCE;
        if (!Physics.SphereCast(bounds.center, radius, Vector3.down, out RaycastHit hit,
            distance, _groundLayers, QueryTriggerInteraction.Ignore)) return;
        if (hit.normal.y < MIN_GROUND_NORMAL_Y) return;

        IsGrounded = true;
        GroundNormal = hit.normal;
    }

    public void ClearGround()
    {
        IsGrounded = false;
        GroundNormal = Vector3.up;
    }

    private void CacheComponent()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _groundLayers = LayerMask.GetMask("Ground");
    }
}
