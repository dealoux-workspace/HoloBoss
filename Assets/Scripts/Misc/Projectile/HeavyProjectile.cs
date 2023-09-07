using System.Collections;
using System.Collections.Generic;
using DeaLoux.Entity;
using UnityEngine;

public class HeavyProjectile : Projectile
{
    // ground
    protected override void HandleGroundCollision(RaycastHit2D hit) 
    {
        dir = Vector3.Reflect(dir, hit.normal);
        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    // weapon's hitbox
    protected override void HandleHitboxCollision(RaycastHit2D hit) { }

    // entity
    protected override void HandleEntityCollision(Entity entity)
    {
        base.HandleEntityCollision(entity);
    }
}
