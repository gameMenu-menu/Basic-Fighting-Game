using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class RightPunchMovement : Movement
    {
        public RightPunchMovement()
        {
            Name = "Right Punch";
            Trigger = "RightPunch";
            Type = MovementType.RightPunch;
            Damage = 6;
        }

        public override void Inisiate()
        {
            base.Inisiate();

            //sprite = MovementManager.Instance.ThreePunchSprite;
            //Range = MovementManager.Instance.CloseRange;
        }

        public override void OnUpgrade()
        {
            base.OnUpgrade();
        }
    }

    public class LeftPunchMovement : Movement
    {
        public LeftPunchMovement()
        {
            Name = "Left Punch";
            Trigger = "LeftPunch";
            Type = MovementType.LeftPunch;
            Damage = 6;
        }

        public override void Inisiate()
        {
            base.Inisiate();

            //sprite = MovementManager.Instance.ThreePunchSprite;
            //Range = MovementManager.Instance.CloseRange;
        }

        public override void OnUpgrade()
        {
            base.OnUpgrade();
        }
    }

    public class KickMovement : Movement
    {
        public KickMovement()
        {
            Name = "Kick";
            Trigger = "Kick";
            Type = MovementType.Kick;
            Damage = 6;
        }

        public override void Inisiate()
        {
            base.Inisiate();

            //sprite = MovementManager.Instance.ThreePunchSprite;
            //Range = MovementManager.Instance.CloseRange;
        }

        public override void OnUpgrade()
        {
            base.OnUpgrade();
        }
    }

    public class RunMovement : Movement
    {
        public RunMovement()
        {
            Name = "Run";
            Trigger = "Run";
            Type = MovementType.Run;
        }
    }

    public class IdleMovement : Movement
    {
        public IdleMovement()
        {
            Name = "Idle";
            Trigger = "Idle";
            Type = MovementType.Idle;
        }
        
    }

    public class HitMovement : Movement
    {
        public HitMovement()
        {
            Name = "Hit";
            Trigger = "Hit";
            Type = MovementType.Hit;
        }
        
    }

    public class FallMovement : Movement
    {
        public FallMovement()
        {
            Name = "Fall";
            Trigger = "Fall";
            Type = MovementType.Hit;
        }
        
    }

    public class DieMovement : Movement
    {
        public DieMovement()
        {
            Name = "Die";
            Trigger = "Die";
            Type = MovementType.Die;
        }
        
    }
