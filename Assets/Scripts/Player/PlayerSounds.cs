using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    private AudioSource footstepAudioSource;
    public AudioClip walkingSound;

    private CharacterController characterController;

    // Set the speed at which the walking sound starts playing
    public float walkSoundSpeed = 2.0f;

    void Start()
    {
        footstepAudioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // Check if the player is moving
            if (characterController.velocity.magnitude > 0 && !footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = walkingSound;
                footstepAudioSource.Play();
            }
            else if (characterController.velocity.magnitude < walkSoundSpeed)
            {
                footstepAudioSource.Stop();
            }
        }
        else
        {
            footstepAudioSource.Stop();
        }
    }
}
