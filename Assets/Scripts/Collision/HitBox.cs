using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.CoreSystems.Collision
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField]
        LayerMask hitMask;
        [SerializeField]
        Color colour;
        Vector3 boxSize;
        public Dictionary<float, Vector3> pos = new Dictionary<float, Vector3>();

        // Start is called before the first frame update
        void Awake()
        {
            Vector3 def = new Vector3(1, 0);
            Vector3 upDiag = new Vector3(1, 1);
            Vector3 downDiag = new Vector3(1, -1);
            pos.Add(0, def);
            pos.Add(180, -def);
            pos.Add(90, new Vector3(0, 1));
            pos.Add(-90, new Vector3(0, -1));
            pos.Add(45, upDiag);
            pos.Add(-45, downDiag);
            pos.Add(135, -downDiag);
            pos.Add(-135, -upDiag);
        }

        /*public Collider2D[] HitBoxCheck(Vector3 position, Vector3 _boxSize, Quaternion rotation)
        {
            boxSize = _boxSize;
            return Physics2D.OverlapBoxAll(position + new Vector3(_boxSize.x / 2, 0), _boxSize, 0, hitMask);
        }
*/
        public Collider2D[] HitBoxCheck(Vector3 _boxSize)
        {
            boxSize = _boxSize;
            //return Physics2D.OverlapBoxAll(transform.position + new Vector3(_boxSize.x / 2, 0), _boxSize, 0, hitMask);
            return Physics2D.OverlapBoxAll(transform.position, _boxSize, 0, hitMask);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = colour;
            Gizmos.matrix = transform.localToWorldMatrix;
            var pos = Vector3.zero;
            //pos.x += boxSize.x / 2;
            Gizmos.DrawCube(pos, new Vector3(boxSize.x, boxSize.y, boxSize.z));
        }
    }

    public enum ColliderState
    {
        Closed,
        Open,
        Colliding
    }
}
