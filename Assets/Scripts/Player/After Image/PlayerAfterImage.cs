using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerAfterImage : MonoBehaviour
    {
        [SerializeField]
        float activeTime = .2f;
        float timeActivated;
        float alpha;
        [SerializeField]
        float alphaSet = .8f;
        [SerializeField]
        float alphaDecay = .85f;

        private Transform player;

        private SpriteRenderer SR;
        private SpriteRenderer playerSR;

        private Color color;

        private void OnEnable()
        {
            SR = GetComponent<SpriteRenderer>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerSR = player.GetComponent<SpriteRenderer>();

            alpha = alphaSet;
            SR.sprite = playerSR.sprite;
            transform.position = player.position;
            transform.rotation = player.rotation;
            timeActivated = Time.time;
        }

        private void Update()
        {
            alpha -= alphaDecay * Time.deltaTime;
            color = new Color(1f, 1f, 1f, alpha);
            SR.color = color;

            if (Time.time >= timeActivated + activeTime)
            {
                PlayerAfterImagePool.Instance.ReturnToPool(this);
            }

        }
    }
}