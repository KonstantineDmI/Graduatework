using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.PlayerBehaviour
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float movementForce;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float distanceBetweenGround;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Collider collider;
        
        
        public void Move(Vector2 axises)
        {
            ApplyMovement(axises);
        }
        
        private void ApplyMovement(Vector2 axises)
        {
            var velocity = GetYVelocity();
            Vector3 direction = new Vector3(axises.x, velocity, axises.y);
            rigidBody.velocity = direction * (Time.deltaTime * movementSpeed) * movementForce;
            UpdateRotation(rigidBody.velocity);
        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, collider.bounds.size.y + distanceBetweenGround);
        }
        
        public void ResetVelocity(Vector2 direction)
        {
            rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y, 0f);
        }

        private void UpdateRotation(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);
        }


        private float GetYVelocity()
        {
            var velocity = rigidBody.velocity.y;
            
            if (!IsGrounded() && velocity < 0)
            {
                var gravity = Physics.gravity.y;
                velocity -= gravity * Time.deltaTime;
            }

            return velocity;
        }
        
        
    }
}
