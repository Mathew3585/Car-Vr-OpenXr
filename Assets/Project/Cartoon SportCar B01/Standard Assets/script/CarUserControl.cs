using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        public HingeJoint streeingWheel;
        public HingeJoint SpeedLever;
        public float MaxSpeedAngle = 42;

        public float maxTurnAngle = 180;


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!

            float h = Mathf.Clamp(streeingWheel.angle / maxTurnAngle, -1,1);
            float v = Mathf.Clamp(SpeedLever.angle / MaxSpeedAngle, -1, 1);
            if (Mathf.Abs(v) < 0.1f) 
            {
                v = 0;
            }
            
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
