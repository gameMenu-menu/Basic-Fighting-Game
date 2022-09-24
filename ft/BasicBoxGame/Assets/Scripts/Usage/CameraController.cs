using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
    {

        public Transform Target;
        public Vector3 Offset;

        void Awake()
        {
            Offset = transform.position - Target.position;
        }

        void LateUpdate()
        {

            Vector3 pos = Target.position + Offset;

            transform.position = Vector3.Lerp(transform.position, pos, 0.17f);

        }

        void SmoothLookAt(Vector3 pos)
        {

            Vector3 dir = pos - Target.position;

            Quaternion lookRot = Quaternion.LookRotation(dir, Vector3.up);

            if(dir.sqrMagnitude > 0.01f) Target.rotation = Quaternion.RotateTowards(Target.rotation, lookRot, 30f);
        }

        



    }
