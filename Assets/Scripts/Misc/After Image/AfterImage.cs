using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.CoreSystems.UI
{
    public class AfterImage : MonoBehaviour
    {
        float activeTime = .15f;
        float alphaSet = .8f;
        float alphaDecay = .85f;

        Transform model;


        SpriteRenderer SR;
        SpriteRenderer modelSR;

        Color color;
        float timeActivated;
        float alpha;

        void OnEnable()
        {
            SR = GetComponent<SpriteRenderer>();
            model = GameObject.FindGameObjectWithTag("Player").transform;
            modelSR = model.GetComponent<SpriteRenderer>();

            alpha = alphaSet;
            SR.sprite = modelSR.sprite;
            transform.position = model.position;
            transform.rotation = model.rotation;
            timeActivated = Time.time;
        }

        void Update()
        {
            alpha -= alphaDecay * Time.deltaTime;
            color = new Color(1f, 1f, 1f, alpha);
            SR.color = color;

            if (Time.time >= timeActivated + activeTime)
            {
                Entity.PlayerAfterImagePool.Instance.ReturnToPool(this);
            }

        }
    }
}