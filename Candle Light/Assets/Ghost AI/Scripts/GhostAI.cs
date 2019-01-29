using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    // Hover Variables
    private float _hoverHeight;                         // Hovering height the ghost will try to reach
    private float _initialHoverHeight = 3.8f;           // The initial hover height that doesn't change
    private readonly float _hoverForce = 5.0f;          // The force applied per unit of distance below desired height
    private readonly float _hoverDamp = 0.5f;           // The lifting force is reduced per unit of upward speed.
    private readonly float _hoverAmplitude = 0.05f;     // The max and min height of the sine function to adjust height.
    private readonly float _hoverFrequency = 0.76f;     // The rate at which the hover height is adjusted.

    // Movement Variables
    private bool _isMoving;                              // Determines if the MoveTo() fuction should be called or not.
    private Vector3 _moveTarget;                         // A vector position of the ghost's movement target.
    private readonly float _moveSpeed = 2.2f;           // The speed at which the ghost moves.

    // Player Greeting Behavior
    private bool _hasGreetedPlayer;                     // Simple flag the determines if the ghost has greeted the player.
    private GameObject _playerGameObject;               // Stores a reference to the player gameobject.
    private float _greetTimer = 7;                      // A countdown timer (in ms) for the greeting length.
    private bool _playedGreetingAudio;                  // Prevents the greeting audio clip from playing more than once.

    // Waypoint Tracking Behavior
    public bool _lookForWaypoint;                      // A flag that, when true, tells the ghost to find a waypoint.
    private GameObject _currentWaypoint;                // A reference to the current waypoint to move to and idle at.
    public GameObject[] _waypoints = new GameObject[4]; // A set of predefined waypoints for the ghost to move to.
    public int _waypointIndex;                          // Tracker index of which waypoint we are currently on.

    // Audio Components
    public AudioSource _audioSource;                    // An audio source component attached to our gameobject.
    public AudioClip _greetingAudioClip;                // An audio clip to play the greeting sound effect.

    // Animation Components
    public Animator _animator;                          // An animator controller that handles animations.

    // Rigidbody Component
    public Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // Get our object's rigidbody component, otherwise create a new one.
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null)
        {
            _rigidBody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }

        // Set the rigidbody's drag, making it easier to control.
        _rigidBody.drag = 0.5f;
        _rigidBody.angularDrag = 0.5f;

        // Get our object's audio source component, otherwise create a new one.
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }

        // Get our object's animator component.
        _animator = GetComponent<Animator>();

        // Set the initial hover height of the ghost.
        _hoverHeight = _initialHoverHeight;

        // Set the ghost as initially not moving.
        _isMoving = false;

        // The first thing the ghost should do is greet the player.
        _hasGreetedPlayer = false;
        _playedGreetingAudio = false;

        // Set the first ghost waypoint.
        _waypointIndex = 0;
        _lookForWaypoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Greet the player
        if (!_hasGreetedPlayer)
        {
            _hasGreetedPlayer = GreetPlayer();
            _lookForWaypoint = _hasGreetedPlayer;
        }

        // Look for a new waypoint to move to.
        if (_lookForWaypoint)
        {
            FindWaypoint();
        }

        if (!_isMoving)
        {
            // Adjust the hover height based on a sine wave.
            _hoverHeight += Mathf.Sin(Time.fixedTime * Mathf.PI * _hoverFrequency) * _hoverAmplitude;
        }
    }

    // Fixed Update is called once per physics update
    private void FixedUpdate()
    {
        // Move the ghost
        if (_isMoving)
        {
            _isMoving = MoveTo(_moveTarget);
        }
        // Hover the ghost and face the player
        else
        {
            Hover();
            FacePlayer();
        }
    }

    // Causes the ghost to float up and down.
    private void Hover()
    {
        // Variables to store our raycast hit and to create a ray pointing down to the floor.
        RaycastHit raycastHit;
        Ray downRay = new Ray(transform.position, Vector3.down);

        // Cast the ray straight down, and add forces bases upon the ghost's height.
        if (Physics.Raycast(downRay, out raycastHit))
        {
            // The hover delta is the difference between our desired hover height and the raycast distance.
            float hoverDelta = _hoverHeight - raycastHit.distance;

            // Only apply a "hover" lifting force if the object falls too low.
            if (hoverDelta > 0)
            {
                // Subtract the hover damping from the lifting force.
                float upwardSpeed = _rigidBody.velocity.y;
                float liftForce = hoverDelta * _hoverForce - upwardSpeed * _hoverDamp;

                // Apply the lifting force to the rigidbody.
                _rigidBody.AddForce(liftForce * Vector3.up);

                // Reset the hover height to it's initial value.
                _hoverHeight = _initialHoverHeight;
            }
        }
        // If no floor is underneath the ghost for the raycast to hit, continually add an upwards force.
        else
        {
            // Subtract the hover damping from the lifting force, and push against gravity.
            float upwardSpeed = _rigidBody.velocity.y;
            float liftForce = -(Physics.gravity.y / 4.0f) * _hoverForce - upwardSpeed * _hoverDamp;

            // Apply the lifting force to the rigidbody.
            _rigidBody.AddForce(liftForce * Vector3.up);
        }
    }

    // Moves the ghost to a target location, returning true when the location is reached.
    private bool MoveTo(Vector3 targetPosition)
    {
        // Set the hover height to the target Y position.
        _initialHoverHeight = targetPosition.y + 3.0f;
        _hoverHeight = _initialHoverHeight;

        // Disable gravity while moving.
        _rigidBody.useGravity = false;

        // Move the rigidbody towards the target position, smoothing the movement over time.
        _rigidBody.MovePosition(transform.position + targetPosition * (Time.fixedDeltaTime / _moveSpeed));

        // Rotate the ghost to face in the direction he is moving to.
        Vector3 targetXPosition = new Vector3(targetPosition.x, 0.0f, 0.0f);
        Quaternion lookRotation = Quaternion.LookRotation(targetXPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * _moveSpeed);

        // Determine if we have reached our target position, or if we need to continue moving.
        if (Vector3.Distance(transform.position, targetPosition) > 0.9f)
        {
            // Teleport the ghost to the target location if he deciedes to wander off.
            if (Vector3.Distance(transform.position, targetPosition) > 72.0f)
            {
                transform.position = targetPosition;

                // Re-enable gravity before returning false (stopping movement).
                _rigidBody.useGravity = true;
                return false;
            }

            // Continue moving (return true).
            return true;
        }
        else
        {
            // Re-enable gravity before returning false (stopping movement).
            _rigidBody.useGravity = true;
            return false;
        }
    }

    // Finds the player's position, moves right infront of him, then plays a simple animation.
    private bool GreetPlayer()
    {
        // Decrease the greeting timer each update.
        _greetTimer -= Time.deltaTime;

        // TODO: Temporary Sound Effect
        if (!_playedGreetingAudio && _greetTimer < 5)
        {
            _playedGreetingAudio = true;
            //_audioSource.PlayOneShot(_greetingAudioClip, 0.65f);
        }

        // If the greeting timer reaches 0, end the greeting sequence.
        if (_greetTimer <= 0)
            return true;

        return false;
    }

    // Finds the player via a tag lookup, then returns their current position.
    private Vector3 FindPlayer()
    {
        if (_playerGameObject == null)
        {
            // Find and store the player gameobject.
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Return an empty vector if the player cannot be found.
            if (player == null)
                return Vector3.zero;

            _playerGameObject = player;
        }

        return _playerGameObject.transform.position;
    }

    // The ghost will face in the direction that the player is located.
    private void FacePlayer()
    {
        // Find our player and get their position.
        Vector3 playerPosition = FindPlayer();

        // Don't allow a zero vector look rotation.
        if (playerPosition == Vector3.zero)
            return;

        // Rotate the ghost to face in the direction that the player is in.
        Quaternion lookRotation = Quaternion.LookRotation(playerPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * _moveSpeed * 5);
    }

    // Find a waypoint to travel to, then move towards it.
    private void FindWaypoint()
    {
        if (_waypointIndex >= _waypoints.Length)
        {
            _lookForWaypoint = false;
            return;
        }

        // Get the current waypoint position and then increment the waypoint index.
        if (_waypoints != null)
            _moveTarget = _waypoints[_waypointIndex].transform.position;
        _waypointIndex++;
        _isMoving = true;
        _lookForWaypoint = false;
    }
}
