using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    //Singleton
    private static BulletManager instance;

    private BulletManager()
    {
        Setup();
    }

    public static BulletManager Instance()
    {
        return instance ??= new BulletManager();
    }
    //Singleton

    private Queue<GameObject> _bulletPool;
    int _poolSize;
    GameObject _bulletPrefab;
    Transform _bulletParent;


    void Setup()
    {
        _bulletPool = new Queue<GameObject>();
        _poolSize = 30;
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/MushroomBullet");
        _bulletParent = GameObject.Find("[BULLETS]").transform;

        BuildPool();
    }

    void BuildPool()
    {
        for(int i = 0; i <_poolSize; i++)
        {
            _bulletPool.Enqueue(CreateBullet());
        }
    }

    GameObject CreateBullet()
    {
        GameObject bullet;

        bullet = Instantiate(_bulletPrefab, _bulletParent);

        bullet.SetActive(false);

        return bullet;
    }

    public GameObject GetBullet()
    {
        if( _bulletPool.Count <= 0 )
        {
            _bulletPool.Enqueue(CreateBullet());
        }

        GameObject bullet = _bulletPool.Dequeue();

        bullet.SetActive(true);

        return bullet;
    }

    public void ReturnBullet( GameObject bullet)
    {
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
