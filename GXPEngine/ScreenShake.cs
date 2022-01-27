using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class ScreenShake : GameObject
    {


        private GameObject transform;

        // Desired duration of the shake effect
        private float shakeDuration = 0f;

        // A measure of magnitude for the shake. Tweak based on your preference
        readonly private float shakeMagnitude = 0.7f;

        // A measure of how quickly the shake effect should evaporate
        readonly private float dampingSpeed = 1.0f;

        // The initial position of the GameObject
        readonly private float initialPositionX;
        readonly private float initialPositionY;

        public ScreenShake( GameObject playField, float duration, float strength)
        {
            shakeDuration = duration;
            shakeMagnitude = strength;
            transform = playField;
            initialPositionX = transform.x;
            initialPositionY = transform.y;

        }

        void Update()
        {
            if (shakeDuration > 0)
            {
                Random rand = new Random();
                transform.x = initialPositionX + rand.Next(10) * shakeMagnitude;
                transform.y = initialPositionY + rand.Next(10) * shakeMagnitude;


                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.x = initialPositionX;
                transform.y = initialPositionY;
            }
        }


        
    }
}
