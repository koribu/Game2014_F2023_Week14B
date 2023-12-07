using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShootingBehavior : MonoBehaviour
{

    PlayerDetection _playerDetection;

    [SerializeField]
    Transform _bulletSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _playerDetection = GetComponentInChildren<PlayerDetection>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(_playerDetection.GetLOS())
        {
            BulletManager.Instance().GetBullet(_bulletSpawnPoint.position);
        }


    }
}
