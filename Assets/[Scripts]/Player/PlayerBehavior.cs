using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField]
    float _accelerator = 100;
    [SerializeField]
    float _maxSpeed = 5;

    [SerializeField]
    float _jumpSpeedLimit;
    // Start is called before the first frame update

    [SerializeField]
    Transform _groundPoint;

    [SerializeField]
    float _jumpingPower = 20;

    [SerializeField]
    bool _isGrounded = false;

    float _airbornSpeedMultiplier = .6f;
    [SerializeField]
    float _hurtJumpAmount;

    Joystick _leftJoystick;
    [SerializeField]
    [Range(0, 1)]
    float _treshold;

    [SerializeField]
    LayerMask _groundingLayers;

    [Header("Camera Shake Properties")]
    [SerializeField]
    CinemachineVirtualCamera _mainCamera;
    CinemachineBasicMultiChannelPerlin _cameraShakePerlin;

    [SerializeField]
    float _cameraShakePower;
    [SerializeField]
    float _cameraShakeTime;

    bool _isRoutineStated = false;




    Animator _animator;
    SoundManager _soundManager;
    HealthBarController _healthBarController;
    ParticleSystem _dustTrail;
    void Start()
    {
        if(GameObject.Find("OnScreenController"))
        {
            _leftJoystick = GameObject.Find("LeftController").GetComponent<Joystick>();
        }
      
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _soundManager = FindObjectOfType<SoundManager>();
        _healthBarController = FindObjectOfType<HealthBarController>();
        _dustTrail = GetComponentInChildren<ParticleSystem>();

        _cameraShakePerlin = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {

        //Jump
        Jump();

    }

    private void FixedUpdate()
    {
        //Movement
        Move();
        IsGrounded();

        if(_rigidbody.velocity.y > _jumpSpeedLimit)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeedLimit);
        }

        if (!_isGrounded)
        {
            if(_rigidbody.velocity.y >=0)
            {
                _animator.SetInteger("State", (int)AnimationState.JUMP);
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationState.FALL);
            }
        }
        else // is grounded
        {

            if(Mathf.Abs(_rigidbody.velocity.x) > 0.1f)
            {
                _animator.SetInteger("State", (int)AnimationState.WALK);

                _dustTrail.Play();

            }
            else
            {
                _animator.SetInteger("State", (int)AnimationState.IDLE);
            }
         
        }
    }

    private void Move()
    {
        float leftJoystickHorizontalInput = 0;
        if(_leftJoystick != null)
        {
            leftJoystickHorizontalInput = _leftJoystick.Horizontal;
        }

        float xMovementDirection = Input.GetAxisRaw("Horizontal") + leftJoystickHorizontalInput; // Get the direction of the movement


        float applicableAcceleration = _accelerator;

        if (!_isGrounded)
        {
            applicableAcceleration *= _airbornSpeedMultiplier; //Airborne speed
        }



        Vector2 force = xMovementDirection * Vector2.right * applicableAcceleration;


        if(xMovementDirection < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (xMovementDirection > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }

        _rigidbody.AddForce(force);

        _rigidbody.velocity =  Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }

    private void Jump()
    {
        float leftJoystickVerticalInput = 0;
        if(_leftJoystick)
        {
            leftJoystickVerticalInput = _leftJoystick.Vertical;
        }

        if(_isGrounded && (Input.GetKeyDown(KeyCode.Space) || leftJoystickVerticalInput > _treshold))
        {
            _rigidbody.AddForce(Vector2.up * _jumpingPower, ForceMode2D.Impulse);

            _soundManager.PlaySound(Channel.PLAYER_SOUND_CHANNEL, Sound.PLAYER_JUMP_SFX);
        }
    }

    void IsGrounded()
    {
        _isGrounded = Physics2D.CircleCast(_groundPoint.position, .1f, Vector2.down, .1f, _groundingLayers);


    }

    IEnumerator CameraShakeRoutine()
    {
        if (_isRoutineStated)
            yield break;

        _isRoutineStated = true;

        _cameraShakePerlin.m_AmplitudeGain = _cameraShakePower;
        yield return new WaitForSeconds(_cameraShakeTime);
        _cameraShakePerlin.m_AmplitudeGain = 0;

        _isRoutineStated = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //get damage
            _healthBarController.TakeDamage(collision.GetComponent<EnemyBehavior>().GetHitDamageAmount());

            _soundManager.PlaySound(Channel.PLAYER_HURT_CHANNEL, Sound.PLAYER_HURT_SFX);

            StartCoroutine(CameraShakeRoutine());
        }
        else if (collision.CompareTag("Hazard"))
        {
            _healthBarController.TakeDamage(10);
            _soundManager.PlaySound(Channel.PLAYER_HURT_CHANNEL, Sound.PLAYER_HURT_SFX);

            // From hazard to opposite direction
            Vector2 dir = transform.position - collision.transform.position;

            _rigidbody.AddForce(dir * _hurtJumpAmount, ForceMode2D.Impulse);


            StartCoroutine(CameraShakeRoutine());


        }
    }
}
