using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    bool _isActive = false;

    PlayerBehavior _player;
    Rigidbody2D _rigidbody;
    Vector3 _target;

    [SerializeField]
    float _shootingPower;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isActive)
        {
            transform.Translate((_target - transform.position) * Time.deltaTime);
        }
    }

    public void Activate()
    {
        StartCoroutine(BulletShootingRoutine());
    }

    IEnumerator BulletShootingRoutine()
    {
        yield return new WaitForSeconds(.1f);
        //Do shooting actions
        Vector2 dir = (_player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(dir * _shootingPower, ForceMode2D.Impulse);

        //Wait an amount of time
        yield return new WaitForSeconds(1f);

        //Destroy itself

        BulletManager.Instance().ReturnBullet(gameObject);
    }


    
}
