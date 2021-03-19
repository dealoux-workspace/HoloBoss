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

        public void HitBoxCheck(Vector3 position, Vector3 _boxSize, Quaternion rotation)
        {
            boxSize = _boxSize;
            //Debug.Log(boxSize);
            Collider[] colliders = Physics.OverlapBox(position + new Vector3(_boxSize.x / 2, 0), _boxSize, rotation, hitMask);

            if (colliders.Length > 0)
            {
                Debug.Log("We hit something");
            }
        }

        public void HitBoxCheck(Vector3 _boxSize)
        {
            boxSize = _boxSize;
            //Debug.Log(boxSize);
            Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(_boxSize.x / 2, 0), _boxSize, transform.rotation, hitMask);

            if (colliders.Length > 0)
            {
                Debug.Log("We hit something");
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = colour;
            Gizmos.matrix = transform.localToWorldMatrix;
            var pos = Vector3.zero;
            pos.x += boxSize.x / 2;
            Gizmos.DrawCube(pos, new Vector3(boxSize.x * 2, boxSize.y * 2, boxSize.z * 2));
        }
    }

    public enum ColliderState
    {
        Closed,
        Open,
        Colliding
    }
}
