using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProjectile : Projectile
{
    protected override void HandleCollision(RaycastHit2D hit)
    {
        pool.Return(this);
    }
}
