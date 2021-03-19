using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyProjectile : Projectile
{
    protected override void HandleCollision(RaycastHit2D hit)
    {
        dir = Vector3.Reflect(dir, hit.normal);
        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
