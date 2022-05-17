using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier = 1;
    [SerializeField] bool isOnGround = true;
    [SerializeField] public bool gameOver { get; private set; }
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            _animator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            _audioSource.PlayOneShot(jumpSound, 1.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            _animator.SetInteger("DeathType_int", 1);
            _animator.SetBool("Death_b", true);
            dirtParticle.Stop();
            explosionParticle.Play();
            _audioSource.PlayOneShot(crashSound, 1.0f);
        }
    }
}
