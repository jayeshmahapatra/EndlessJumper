using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{   
    public float jumpForce = 10f;

    private AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.relativeVelocity.y <= 0f) {
                
                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
    
                if (rb != null) {
    
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                }

        }
        
    }

    private void OnCollisionExit2D(Collision2D other) {



        // Get the audio source component of the other object
        audioSource = other.gameObject.GetComponent<AudioSource>();

        // Play the audio clip one shot
        audioSource.PlayOneShot(audioSource.clip);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
