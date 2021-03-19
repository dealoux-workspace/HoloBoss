using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float speed = 80f;
    [SerializeField]
    protected ProjectilePoolSO pool;
    [SerializeField]
    protected LayerMask collisionMask;
    protected Vector3 dir;

    public void Init(Vector3 _dir)
    {
        dir = _dir;
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        DetectCollision();
    }

    void DetectCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Time.deltaTime * speed, collisionMask);
        DrawRay(transform.position, dir, Color.blue);

        if (hit)
        {
            HandleCollision(hit);
        }
    }

    protected virtual void HandleCollision(RaycastHit2D hit)
    {

    }

    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetContacts(contactPoint);
        dir = Vector3.Reflect(transform.position, contactPoint[0].normal);
    }*/

    void DrawRay(Vector3 start, Vector3 dir, Color color)
    {
        Debug.DrawRay(start, dir, color);
    }

    void OnBecameInvisible()
    {
        pool.Return(this);
    }

}
