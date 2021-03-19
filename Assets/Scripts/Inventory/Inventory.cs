using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class Inventory : MonoBehaviour
    {
        Animator anim;
        [SerializeField]
        Data.PlayerData data;
        [SerializeField]
        AnimatorOverrideController overrideController;
        PlayerInputHandler inputHandler;

        // Primary Attack
        const string PRIMATK_IDLE4 = "PrimAtk_Idle4";
        const string PRIMATK_IDLE4_2 = "PrimAtk_Idle4_2";
        const string PRIMATK_IDLE4_3 = "PrimAtk_Idle4_3";
        const string PRIMATK_IDLE4_UP = "PrimAtk_Idle4Up";
        const string PRIMATK_IDLE4_CHARGED = "PrimAtk_Idle4Charged";
        const string PRIMATK_AERIAL4 = "PrimAtk_Aerial4";
        const string PRIMATK_AERIAL4_UP = "PrimAtk_Aerial4Up";
        const string PRIMATK_AERIAL4_DOWN = "PrimAtk_Aerial4Down";
        const string PRIMATK_AERIAL8 = "PrimAtk_Aerial8";
        const string PRIMATK_AERIAL4_CHARGED = "PrimAtk_Aerial4Charged";
        const string PRIMATK_WALL = "PrimAtk_Wall";
        const string PRIMATK_WALL_CHARGED = "PrimAtk_WallCharged";
        const string PRIMATK_DASH = "PrimAtk_Dash";
        const string PRIMATK_MOVE = "PrimAtk_Move";
        const string PRIMATK_IDLE8 = "PrimAtk_Idle8";
        const string PRIMATK_IDLE8_UP = "PrimAtk_Idle8Up";
        const string PRIMATK_IDLE8_UPDIAG = "PrimAtk_Idle8UpDiag";
        const string PRIMATK_IDLE8_DOWNDIAG = "PrimAtk_Idle8DownDiag";
        const string PRIMATK_AERIAL8_UP = "PrimAtk_Aerial8Up";
        const string PRIMATK_AERIAL8_UPDIAG = "PrimAtk_Aerial8UpDiag";
        const string PRIMATK_AERIAL8_DOWN = "PrimAtk_Aerial8Down";
        const string PRIMATK_AERIAL8_DOWNDIAG = "PrimAtk_Aerial8DownDiag";
        // Charged
        const string PRIMATK_IDLE8_CHARGED = "PrimAtk_Idle8Charged";
        const string PRIMATK_IDLE8_CHARGED_UP = "PrimAtk_Idle8ChargedUp";
        const string PRIMATK_IDLE8_CHARGED_UPDIAG = "PrimAtk_Idle8ChargedUpDiag";
        const string PRIMATK_IDLE8_CHARGED_DOWNDIAG = "PrimAtk_Idle8ChargedDownDiag";
        const string PRIMATK_AERIAL8_CHARGED = "PrimAtk_Aerial8Charged";
        const string PRIMATK_AERIAL8_CHARGED_UP = "PrimAtk_Aerial8ChargedUp";
        const string PRIMATK_AERIAL8_CHARGED_UPDIAG = "PrimAtk_Aerial8ChargedUpDiag";
        const string PRIMATK_AERIAL8_CHARGED_DOWN = "PrimAtk_Aerial8ChargedDown";
        const string PRIMATK_AERIAL8_CHARGED_DOWNDIAG = "PrimAtk_Aerial8ChargedDownDiag";

        // Secondary Attack
        const string SECATK_IDLE4 = "SecAtk_Idle4";
        const string SECATK_IDLE4_2 = "SecAtk_Idle4_2";
        const string SECATK_IDLE4_3 = "SecAtk_Idle4_3";
        const string SECATK_IDLE4_UP = "SecAtk_Idle4_Up";
        const string SECATK_IDLE4_CHARGED = "SecAtk_Idle4Charged";
        const string SECATK_AERIAL4 = "SecAtk_Aerial4";
        const string SECATK_AERIAL4_UP = "SecAtk_Aerial4Up";
        const string SECATK_AERIAL4_DOWN = "SecAtk_Aerial4Down";
        const string SECATK_AERIAL8 = "SecAtk_Aerial8";
        const string SECATK_AERIAL4_CHARGED = "SecAtk_Aerial4Charged";

        const string SECATK_WALL = "SecAtk_Wall";
        const string SECATK_WALL_CHARGED = "SecAtk_WallCharged";
        const string SECATK_DASH = "SecAtk_Dash";
        const string SECATK_MOVE = "SecAtk_Move";

        const string SECATK_IDLE8 = "SecAtk_Idle8";
        const string SECATK_IDLE8_UP = "SecAtk_IdleUp";
        const string SECATK_IDLE8_UPDIAG = "SecAtk_Idle8UpDiag";
        const string SECATK_IDLE8_DOWNDIAG = "SecAtk_Idle8DownDiag";
        const string SECATK_AERIAL8_UP = "SecAtk_Aerial8Up";
        const string SECATK_AERIAL8_UPDIAG = "SecAtk_Aerial8UpDiag";
        const string SECATK_AERIAL8_DOWN = "SecAtk_Aerial8Down";
        const string SECATK_AERIAL8_DOWNDIAG = "SecAtk_Aerial8DownDiag";
        // Charged
        const string SECATK_IDLE8_CHARGED = "SecAtk_Idle8Charged";
        const string SECATK_IDLE8_CHARGED_UP = "SecAtk_Idle8ChargedUp";
        const string SECATK_IDLE8_CHARGED_UPDIAG = "SecAtk_Idle8ChargedUpDiag";
        const string SECATK_IDLE8_CHARGED_DOWNDIAG = "SecAtk_Idle8ChargedDownDiag";
        const string SECATK_AERIAL8_CHARGED = "SecAtk_Aerial8Charged";
        const string SECATK_AERIAL8_CHARGED_UP = "SecAtk_Aerial8ChargedUp";
        const string SECATK_AERIAL8_CHARGED_UPDIAG = "SecAtk_Aerial8ChargedUpDiag";
        const string SECATK_AERIAL8_CHARGED_DOWN = "SecAtk_Aerial8ChargedDown";
        const string SECATK_AERIAL8_CHARGED_DOWNDIAG = "SecAtk_Aerial8ChargedDownDiag";

        private void Start()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<PlayerInputHandler>();
            SetAnim();
        }

        private void Update()
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
            overrideController[PRIMATK_WALL] = data.slot1.wall;
            overrideController[PRIMATK_WALL_CHARGED] = data.slot1.wallCharged;
            overrideController[PRIMATK_DASH] = data.slot1.dash;

            switch (data.slot1.type)
            {
                case EquipmentType.FOUR_WAY:
                    overrideController[PRIMATK_AERIAL4] = data.slot1.aerial4;
                    overrideController[PRIMATK_AERIAL4_UP] = data.slot1.aerial4Up;
                    overrideController[PRIMATK_AERIAL4_DOWN] = data.slot1.aerial4Down;
                    overrideController[PRIMATK_IDLE4] = data.slot1.idle4;
                    overrideController[PRIMATK_IDLE4_2] = data.slot1.idle4_2;
                    overrideController[PRIMATK_IDLE4_3] = data.slot1.idle4_3;
                    overrideController[PRIMATK_IDLE4_UP] = data.slot1.idle4Up;
                    overrideController[PRIMATK_MOVE] = data.slot1.move;
                    overrideController[PRIMATK_IDLE4_CHARGED] = data.slot1.idle4Charged;
                    overrideController[PRIMATK_AERIAL4_CHARGED] = data.slot1.aerial4Charged;
                    break;

                case EquipmentType.EIGHT_WAY:
                    overrideController[PRIMATK_AERIAL8] = data.slot1.aerial8;
                    overrideController[PRIMATK_AERIAL8_UP] = data.slot1.aerial8Up;
                    overrideController[PRIMATK_AERIAL8_UPDIAG] = data.slot1.aerial8UpDiag;
                    overrideController[PRIMATK_AERIAL8_DOWN] = data.slot1.aerial8Down;
                    overrideController[PRIMATK_AERIAL8_DOWNDIAG] = data.slot1.aerial8DownDiag;
                    overrideController[PRIMATK_IDLE8] = data.slot1.idle8;
                    overrideController[PRIMATK_IDLE8_UP] = data.slot1.idle8Up;
                    overrideController[PRIMATK_IDLE8_UPDIAG] = data.slot1.idle8UpDiag;
                    overrideController[PRIMATK_IDLE8_DOWNDIAG] = data.slot1.idle8DownDiag;

                    // Charged
                    overrideController[PRIMATK_AERIAL8_CHARGED] = data.slot1.aerial8Charged;
                    overrideController[PRIMATK_AERIAL8_CHARGED_UP] = data.slot1.aerial8ChargedUp;
                    overrideController[PRIMATK_AERIAL8_CHARGED_UPDIAG] = data.slot1.aerial8ChargedUpDiag;
                    overrideController[PRIMATK_AERIAL8_CHARGED_DOWN] = data.slot1.aerial8ChargedDown;
                    overrideController[PRIMATK_AERIAL8_CHARGED_DOWNDIAG] = data.slot1.aerial8ChargedDownDiag;
                    overrideController[PRIMATK_IDLE8_CHARGED] = data.slot1.idle8Charged;
                    overrideController[PRIMATK_IDLE8_CHARGED_UP] = data.slot1.idle8ChargedUp;
                    overrideController[PRIMATK_IDLE8_CHARGED_UPDIAG] = data.slot1.idle8ChargedUpDiag;
                    overrideController[PRIMATK_IDLE8_CHARGED_DOWNDIAG] = data.slot1.idle8ChargedDownDiag;
                    break;
            }

            switch (data.slot1.Ctype)
            {
                case EquipmentType.FOUR_WAY:
                    overrideController[PRIMATK_IDLE4_CHARGED] = data.slot1.idle4Charged;
                    overrideController[PRIMATK_AERIAL4_CHARGED] = data.slot1.aerial4Charged;
                    break;

                case EquipmentType.EIGHT_WAY:
                    overrideController[PRIMATK_AERIAL8_CHARGED] = data.slot1.aerial8Charged;
                    overrideController[PRIMATK_AERIAL8_CHARGED_UP] = data.slot1.aerial8ChargedUp;
                    overrideController[PRIMATK_AERIAL8_CHARGED_UPDIAG] = data.slot1.aerial8ChargedUpDiag;
                    overrideController[PRIMATK_AERIAL8_CHARGED_DOWN] = data.slot1.aerial8ChargedDown;
                    overrideController[PRIMATK_AERIAL8_CHARGED_DOWNDIAG] = data.slot1.aerial8ChargedDownDiag;
                    overrideController[PRIMATK_IDLE8_CHARGED] = data.slot1.idle8Charged;
                    overrideController[PRIMATK_IDLE8_CHARGED_UP] = data.slot1.idle8ChargedUp;
                    overrideController[PRIMATK_IDLE8_CHARGED_UPDIAG] = data.slot1.idle8ChargedUpDiag;
                    overrideController[PRIMATK_IDLE8_CHARGED_DOWNDIAG] = data.slot1.idle8ChargedDownDiag;
                    break;
            }
        }

        void LoadSlot2Anim()
        {
            overrideController[SECATK_WALL] = data.slot2.wall;
            overrideController[SECATK_WALL_CHARGED] = data.slot2.wallCharged;
            overrideController[SECATK_DASH] = data.slot2.dash;

            switch (data.slot2.type)
            {
                case EquipmentType.FOUR_WAY:
                    overrideController[SECATK_AERIAL4] = data.slot2.aerial4;
                    overrideController[SECATK_AERIAL4_UP] = data.slot2.aerial4Up;
                    overrideController[SECATK_AERIAL4_DOWN] = data.slot2.aerial4Down;
                    overrideController[SECATK_IDLE4] = data.slot2.idle4;
                    overrideController[SECATK_IDLE4_2] = data.slot2.idle4_2;
                    overrideController[SECATK_IDLE4_3] = data.slot2.idle4_3;
                    overrideController[SECATK_IDLE4_UP] = data.slot2.idle4Up;
                    overrideController[SECATK_MOVE] = data.slot2.move;
                    break;

                case EquipmentType.EIGHT_WAY:
                    overrideController[SECATK_AERIAL8] = data.slot2.aerial8;
                    overrideController[SECATK_AERIAL8_UP] = data.slot2.aerial8Up;
                    overrideController[SECATK_AERIAL8_UPDIAG] = data.slot2.aerial8UpDiag;
                    overrideController[SECATK_AERIAL8_DOWN] = data.slot2.aerial8Down;
                    overrideController[SECATK_AERIAL8_DOWNDIAG] = data.slot2.aerial8DownDiag;
                    overrideController[SECATK_IDLE8] = data.slot2.idle8;
                    overrideController[SECATK_IDLE8_UP] = data.slot2.idle8Up;
                    overrideController[SECATK_IDLE8_UPDIAG] = data.slot2.idle8UpDiag;
                    overrideController[SECATK_IDLE8_DOWNDIAG] = data.slot2.idle8DownDiag;
                    break;
            }

            switch (data.slot2.Ctype)
            {
                case EquipmentType.FOUR_WAY:
                    overrideController[SECATK_IDLE4_CHARGED] = data.slot2.idle4Charged;
                    overrideController[SECATK_AERIAL4_CHARGED] = data.slot2.aerial4Charged;
                    break;

                case EquipmentType.EIGHT_WAY:
                    overrideController[SECATK_AERIAL8_CHARGED] = data.slot2.aerial8Charged;
                    overrideController[SECATK_AERIAL8_CHARGED_UP] = data.slot2.aerial8ChargedUp;
                    overrideController[SECATK_AERIAL8_CHARGED_UPDIAG] = data.slot2.aerial8ChargedUpDiag;
                    overrideController[SECATK_AERIAL8_CHARGED_DOWN] = data.slot2.aerial8ChargedDown;
                    overrideController[SECATK_AERIAL8_CHARGED_DOWNDIAG] = data.slot2.aerial8ChargedDownDiag;
                    overrideController[SECATK_IDLE8_CHARGED] = data.slot2.idle8Charged;
                    overrideController[SECATK_IDLE8_CHARGED_UP] = data.slot2.idle8ChargedUp;
                    overrideController[SECATK_IDLE8_CHARGED_UPDIAG] = data.slot2.idle8ChargedUpDiag;
                    overrideController[SECATK_IDLE8_CHARGED_DOWNDIAG] = data.slot2.idle8ChargedDownDiag;
                    break;
            }
        }
    }
}