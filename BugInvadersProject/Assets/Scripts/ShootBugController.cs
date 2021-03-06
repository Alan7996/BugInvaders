using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBugController : Enemy
{
    public override void Update()
    {
        base.Update();

        if (StartTime) CurrTime += Time.deltaTime;
        if (CurrTime > FireRate)
        {
            CurrTime = 0;
            Fire();
        }
    }

    public override void Fire()
    {
        base.Fire();
        EnemyBulletController b = Object.Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBulletController>();
        b.Initialize(Target.transform);
    }
}
