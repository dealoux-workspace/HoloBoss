using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.CoreSystems.Collision
{
    public class HurtBox : MonoBehaviour
    {
        public bool Left { get; private set; }
        public bool Right { get; private set; }
        public bool Top { get; private set; }
        public bool Bottom { get; private set; }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var contactPoint = collision.GetContact(0).normal;
            if (contactPoint.y < 0)
            {
                Top = true;
            }
            else
            {
                Bottom = true;
            }

            if (contactPoint.x < 0)
            {
                Right = true;
            }
            else
            {
                Left = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            Left = Right = Top = Bottom = false;
        }
    }
}