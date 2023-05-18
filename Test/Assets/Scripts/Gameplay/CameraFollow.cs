using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform transformToFollow;
        [SerializeField] private float followSmooth;
        [SerializeField] private Vector3 offset;

        private Vector3 _velocity;

        private void Update()
        {
            FollowTick();
        }

        private void FollowTick()
        {
            var offsetPosition = transformToFollow.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, offsetPosition, ref _velocity, followSmooth);
        }
    }

}
