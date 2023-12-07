using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShootingBehavior : MonoBehaviour
{

    PlayerDetection _playerDetection;
    EnemyBehavior _enemyBehavior;

    [SerializeField]
    Transform _bulletSpawnPoint;



    bool _isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _enemyBehavior = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(_playerDetection.GetLOS() && !_isShooting)
        {
            StartCoroutine(BulletShootingRoutine());
        }


    }


    IEnumerator BulletShootingRoutine()
    {
        _isShooting = true;

        _enemyBehavior.SetIsShooting(true);

        BulletManager.Instance().GetBullet(_bulletSpawnPoint.position);

        yield return new WaitForSeconds(1);

        _isShooting = false;

        _enemyBehavior.SetIsShooting(false);


    }
}
