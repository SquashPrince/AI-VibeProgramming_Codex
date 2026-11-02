using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpVelocity = 6f;
    [SerializeField] private float _wallJumpSpeed = 6f;
    [SerializeField] private float _wallJumpControlLockTime = 0.2f;

    private Rigidbody _rigidbody;
    private Collider _lastJumpWall;
    private Vector3 _wallJumpVelocity;
    private float _wallJumpTimeRemaining;
    private int _airJumpsRemaining = 1;

    public bool IsJumping { get; private set; }
    public bool IsPushOffActive => _wallJumpTimeRemaining > 0f;

    private void Awake() => CacheComponent();

    public void UpdateJumpState()
    {
        if (IsJumping && _rigidbody.linearVelocity.y <= 0f) IsJumping = false;
    }

    public void ResetGroundedJump(bool isGrounded)
    {
        if (!isGrounded) return;
        _airJumpsRemaining = 1;
        _lastJumpWall = null;
        _wallJumpTimeRemaining = 0f;
    }

    public void RecordWallContact(Collider wall)
    {
        if (wall != _lastJumpWall) _lastJumpWall = null;
    }

    public Vector3 GetHorizontalVelocity(Vector3 moveVelocity, float deltaTime)
    {
        if (_wallJumpTimeRemaining <= 0f) return moveVelocity;
        _wallJumpTimeRemaining = Mathf.Max(0f, _wallJumpTimeRemaining - deltaTime);
        return _wallJumpVelocity;
    }

    public bool TryJump(bool isGrounded, List<ContactPoint> wallContacts, Vector2 moveInput)
    {
        if (!isGrounded)
        {
            // 공중 점프를 먼저 사용하고, 소진한 뒤에만 벽점프를 시도한다.
            if (_airJumpsRemaining > 0)
            {
                _airJumpsRemaining--;
                ApplyWallContactAirJump(wallContacts, moveInput);
            }
            else if (!TryWallJump(wallContacts)) return false;
        }

        IsJumping = true;
        _rigidbody.useGravity = true;
        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.y = _jumpVelocity;
        _rigidbody.linearVelocity = velocity;
        return true;
    }

    public void ResetJumpState()
    {
        IsJumping = false;
        _lastJumpWall = null;
        _airJumpsRemaining = 1;
        _wallJumpTimeRemaining = 0f;
        _wallJumpVelocity = Vector3.zero;
    }

    private void ApplyWallContactAirJump(List<ContactPoint> wallContacts, Vector2 moveInput)
    {
        // 벽 접촉 중의 공중 점프만 입력 반대쪽으로 튕긴다. 횟수 회복은 하지 않는다.
        if (wallContacts.Count == 0 || moveInput.sqrMagnitude == 0f) return;
        Vector3 direction = new Vector3(-moveInput.x, 0f, -moveInput.y).normalized;
        ApplyJumpPushOff(direction);
    }

    private bool TryWallJump(List<ContactPoint> wallContacts)
    {
        foreach (ContactPoint contact in wallContacts)
        {
            if (contact.otherCollider == _lastJumpWall) continue;
            _lastJumpWall = contact.otherCollider;
            Vector3 direction = Vector3.ProjectOnPlane(contact.normal, Vector3.up).normalized;
            ApplyJumpPushOff(direction);
            return true;
        }
        return false;
    }

    private void ApplyJumpPushOff(Vector3 direction)
    {
        // 이동 입력이 다음 물리 프레임에 발사 속도를 덮어쓰지 않도록 잠시 유지한다.
        _wallJumpTimeRemaining = _wallJumpControlLockTime;
        _wallJumpVelocity = direction * _wallJumpSpeed;
        _rigidbody.linearVelocity = _wallJumpVelocity;
        _rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void CacheComponent()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
