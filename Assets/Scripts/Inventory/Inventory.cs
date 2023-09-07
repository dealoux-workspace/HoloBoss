using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeaLoux.Equipment;

namespace DeaLoux.Entity
{
    public class Inventory : MonoBehaviour
    {
        Animator anim;
        [SerializeField]
        EntityData data;
        [SerializeField]
        AnimatorOverrideController overrideController;
        [SerializeField]
        readonly AnimationNames names;
        PlayerInputHandler inputHandler;

        void Start()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<PlayerInputHandler>();
            SetAnim();
        }

        void Update()
        {
            if (inputHandler.CycleInput)
            {
                inputHandler.TickCycleInput();
                Switch();
            }
        }

        void SetAnim()
        {
            //overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
            anim.runtimeAnimatorController = overrideController;
            LoadSlot1Anim();
            LoadSlot2Anim();
        }

        void Switch()
        {
            var tempSlot = data.slot1;
            data.slot1 = data.slot2;
            data.slot2 = tempSlot;

            LoadSlot1Anim();
            LoadSlot2Anim();
        }

        void LoadSlot1Anim()
        {
            foreach (EquipmentAnimation anim in data.slot1.specificAnimations)
            {
                overrideController[names.list[anim.id]] = anim.clip;
            }
        }

        void LoadSlot2Anim()
        {
            foreach (EquipmentAnimation anim in data.slot2.specificAnimations)
            {
                overrideController[names.list[anim.id + 31]] = anim.clip;
            }
        }
    }
}