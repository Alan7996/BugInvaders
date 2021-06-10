using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBugController : Enemy
{
    private bool startTime = false;
    private bool fired = false;

    public override void Update()
    {
        base.Update();

        if (startTime) CurrTime += Time.deltaTime;
        if (!fired && CurrTime > FireRate)
        {
            Fire();
        }
    }

    public override void Initialize(GameObject targetPlayer)
    {
        base.Initialize(targetPlayer);
        startTime = true;
    }

    public override void Fire()
    {
        base.Fire();
        fired = true;
        EnemyBulletController b = Object.Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBulletController>();
        b.Initialize(Target.transform);
    }
}
