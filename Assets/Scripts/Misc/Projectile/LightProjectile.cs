using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeaLoux.Entity;

public class LightProjectile : Projectile
{
    protected override void HandleGroundCollision(RaycastHit2D hit) 
    {
        pool.Return(this);
    }

    protected override void HandleHitboxCollision(RaycastHit2D hit) 
    { 

    }

    protected override void HandleEntityCollision(Entity entity) 
    {
        base.HandleEntityCollision(entity);
    }
}
