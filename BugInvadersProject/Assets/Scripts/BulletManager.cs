using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    [SerializeField] PlayerBulletController bulletGunPrefab;
    [SerializeField] PlayerBulletController bulletMissilePrefab;
    [SerializeField] PlayerBulletController bulletLaserPrefab;

    List<PlayerBulletController>[] bulletReady = new List<PlayerBulletController>[3];
    List<PlayerBulletController>[] bulletNotReady = new List<PlayerBulletController>[3];

    List<PlayerBulletController> bulletGunReady = new List<PlayerBulletController>();
    List<PlayerBulletController> bulletGunNotReady = new List<PlayerBulletController>();

    List<PlayerBulletController> bulletMissileReady = new List<PlayerBulletController>();
    List<PlayerBulletController> bulletMissileNotReady = new List<PlayerBulletController>();

    List<PlayerBulletController> bulletLaserReady = new List<PlayerBulletController>();
    List<PlayerBulletController> bulletLaserNotReady = new List<PlayerBulletController>();

    private int mechType;

    private void Awake()
    {
        bulletReady[0] = bulletGunReady;
        bulletReady[1] = bulletMissileReady;
        bulletReady[2] = bulletLaserReady;

        bulletNotReady[0] = bulletGunNotReady;
        bulletNotReady[1] = bulletMissileNotReady;
        bulletNotReady[2] = bulletLaserNotReady;

        instance = this;

        for (int i = 0; i < 20; i++)
        {
            var bG = Instantiate(bulletGunPrefab);
            bulletGunReady.Add(bG);
            bG.transform.position = new Vector3(-100, -100, -100);
            var bM = Instantiate(bulletMissilePrefab);
            bulletMissileReady.Add(bM);
            bM.transform.position = new Vector3(-100, -100, -100);
            var bL = Instantiate(bulletLaserPrefab);
            bulletLaserReady.Add(bL);
            bL.transform.position = new Vector3(-100, -100, -100);
        }
    }

    public void FireBullet(int bType, Vector3 position)
    {
        if (bulletReady[bType].Count == 0) return;
        var bullet = bulletReady[bType][0];
        bulletReady[bType].RemoveAt(0);
        bulletNotReady[bType].Add(bullet);

        bullet.gameObject.SetActive(true);
        bullet.transform.position = position;
    }

    private void Update()
    {
        mechType = (int)PlayerController.instance.mechType;
        for (int i = bulletNotReady[mechType].Count - 1; i >= 0; i--)
        {
            var bNotReady = bulletNotReady[mechType][i];
            bool isReset = false;
            if (bNotReady.IsHit)
            {
                isReset = true;
            }

            if (isReset)
            {
                ResetBullet(bNotReady);
                bulletNotReady[mechType].RemoveAt(i);
            }
        }
    }

    void ResetBullet(PlayerBulletController b)
    {
        b.Reset();
        b.transform.position = new Vector3(100, 100, 100);
        bulletReady[(int)b.bulletType].Add(b);
    }
}
