using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBugController : Enemy
{
    public override void Update()
    {
        base.Update();

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
        EnemyBulletController bL = Object.Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBulletController>();
        EnemyBulletController bR = Object.Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBulletController>();
        b.Initialize(Target.transform);
        bL.Initialize(Target.transform, true);
        bR.Initialize(Target.transform, false);
    }
}
