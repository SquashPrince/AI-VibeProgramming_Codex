using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(GroundChecker), typeof(PlayerJump))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 720f;

    private readonly List<ContactPoint> _wallContacts = new List<ContactPoint>();
    private Rigidbody _rigidbody;
    private GroundChecker _groundChecker;
    private PlayerJump _playerJump;
    private LayerMask _wallLayers;
    private Vector2 _moveInput;
    private bool _isJumpRequested;

    private void Awake() => CacheComponent();

    private void OnEnable() => EnableInput();
    private void OnDisable() => DisableInput();
    private void OnCollisionEnter(Collision collision) => RecordWallContacts(collision);
    private void OnCollisionStay(Collision collision) => RecordWallContacts(collision);

    private void Update()
    {
        ReadMoveInput();
        ReadJumpInput();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        Jump();
        RotateTowardsMovement();
        ClearWallContacts();
    }

    private void CheckGround()
    {
        _playerJump.UpdateJumpState();
        _groundChecker.CheckGround(_playerJump.IsJumping);
        _playerJump.ResetGroundedJump(_groundChecker.IsGrounded);
    }

    private void RecordWallContacts(Collision collision)
    {
        // Wall 레이어만 벽점프 대상으로 사용한다.
        if ((_wallLayers.value & (1 << collision.collider.gameObject.layer)) == 0) return;
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint contact = collision.GetContact(i);
            // 지면과 천장을 제외하고 가파른 측면만 벽으로 취급한다.
            if (Mathf.Abs(contact.normal.y) >= GroundChecker.MIN_GROUND_NORMAL_Y) continue;
            _wallContacts.Add(contact);
            _playerJump.RecordWallContact(contact.otherCollider);
        }
    }

    private void ClearWallContacts() => _wallContacts.Clear();

    private void ReadMoveInput()
    {
        _moveInput = Vector2.ClampMagnitude(_moveAction.action.ReadValue<Vector2>(), 1f);
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        _rigidbody.useGravity = !_groundChecker.IsGrounded;

        if (_groundChecker.IsGrounded)
        {
            // 접지 중에는 경사면을 따라 이동하며, 정지하면 상승 속도도 함께 제거한다.
            Vector3 groundDirection = Vector3.ProjectOnPlane(moveDirection, _groundChecker.GroundNormal);
            _rigidbody.linearVelocity = groundDirection.normalized * (_moveInput.magnitude * _moveSpeed);
            return;
        }

        Vector3 velocity = _playerJump.GetHorizontalVelocity(moveDirection * _moveSpeed, Time.fixedDeltaTime);
        _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
    }

    private void ReadJumpInput()
    {
        if (_jumpAction.action.WasPressedThisFrame())
        {
            _isJumpRequested = true;
        }
    }

    private void RotateTowardsMovement()
    {
        // 벽에서 튕기는 동안에는 점프가 설정한 발사 방향을 유지한다.
        if (_playerJump.IsPushOffActive) return;
        Vector3 direction = Vector3.ProjectOnPlane(_rigidbody.linearVelocity, Vector3.up);
        if (direction.sqrMagnitude < 0.0001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion rotation = Quaternion.RotateTowards(
            _rigidbody.rotation, targetRotation, Mathf.Max(0f, _rotationSpeed) * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(rotation);
    }

    private void Jump()
    {
        if (!_isJumpRequested) return;
        // 공중 입력을 보관하지 않고 다음 물리 프레임에 한 번만 처리한다.
        _isJumpRequested = false;
        if (_playerJump.TryJump(_groundChecker.IsGrounded, _wallContacts, _moveInput)) _groundChecker.ClearGround();
    }

    private void EnableInput()
    {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
    }

    private void DisableInput()
    {
        _moveAction.action.Disable();
        _jumpAction.action.Disable();
        _moveInput = Vector2.zero;
        _isJumpRequested = false;
        _wallContacts.Clear();
        _playerJump.ResetJumpState();
        _groundChecker.ClearGround();
        _rigidbody.useGravity = true;
        _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
    }

    private void CacheComponent()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<GroundChecker>();
        _playerJump = GetComponent<PlayerJump>();
        _wallLayers = LayerMask.GetMask("Wall");
    }
}
