using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

    public enum MovementType {RightPunch, LeftPunch, Kick, Run, Idle, Die, Hit}

    public abstract class Movement
    {
        public string Name;
        public MovementType Type;
        protected string Trigger;
        public Sprite sprite;

        protected int Damage;
        
        public int Level;
        public float Range;

        public bool Triggered;

        public Movement()
        {
            Level = 1;
        }

        public virtual void Inisiate()
        {

        }
        public virtual void OnUpgrade()
        {
            Level++;
            Damage *= 2;
        }

        public virtual string ReturnTrigger()
        {
            return Trigger;
        }

        public virtual void ReplenishStocks()
        {
            
        }

        public int ReturnDamage()
        {
            return Damage*Level;
        }

        
    }
