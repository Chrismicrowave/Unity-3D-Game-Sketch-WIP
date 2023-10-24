using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjThrow : MonoBehaviour
{
    [SerializeField] GameObject HitSplash;

    private new ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem.GetCollisionEvents(other, collisionEvents);

        foreach (ParticleCollisionEvent collisionEvent in collisionEvents)
        {
            // Instantiate a hit splash object at the contact point
            Vector3 contactPoint = collisionEvent.intersection;
            GameObject hitSplash = Instantiate(HitSplash, contactPoint, Quaternion.identity);

            // Set the parent of the hit splash object to the collided object
            hitSplash.transform.localScale = Vector3.one;
            Destroy(hitSplash, 1f);
        }

        // Emit zero particles at the collision position to remove the collided particles instantly
        particleSystem.Emit(0);

        // Stop emitting particles
        particleSystem.Stop();
    }
}
