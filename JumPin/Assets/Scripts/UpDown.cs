 using UnityEngine;

    public class UpDown : MonoBehaviour
    {
        public float bobHeight = 0.5f; // How high the sprite bobs
        public float bobSpeed = 2f;    // How fast the sprite bobs

        private Vector2 initialPosition;

        void Start()
        {
            initialPosition = transform.position;
        }

        void Update()
        {
            // Use Mathf.Sin for smooth oscillation
            float newY = initialPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector2(initialPosition.x, newY);
        }
    }
