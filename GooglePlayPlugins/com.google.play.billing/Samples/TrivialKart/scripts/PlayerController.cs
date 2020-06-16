using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    public GameObject tapToDriveText;
    public Animator animator;
    [FormerlySerializedAs("MovementSpeed")] public float force = 500;
    
    private Gas _gas;
    private Vector3 _carStartPos;
    private Rigidbody2D _rigidbody2D;
    private int _circleCount;
    private static readonly int Speed = Animator.StringToHash("speed");

    public void Start()
    {
        _circleCount = 0;
        _gas = GetComponent<Gas>();
        _carStartPos = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    // when the player click the car at the play mode, the car drive forward.
    private void OnMouseDown()
    {
        if (_rigidbody2D.velocity.magnitude < 0.01 && _gas.HasGas() &&  FindObjectOfType<GameManager>().IsInPlayPage())
        {
            Drive();
        }
    }

    private void FixedUpdate()
    {
        // back to the start point when reach the end
        if (transform.position.x >= 25)
        {
            _circleCount++;
            transform.position = _carStartPos;
        }
        animator.SetFloat(Speed,  _rigidbody2D.velocity.magnitude);
        _gas.SetGasLevel(25 - _carStartPos.x, _circleCount, (float)Math.Round(transform.position.x - _carStartPos.x, 2));
    }

    private void Drive()
    {
        if (tapToDriveText.activeInHierarchy)
        {
            tapToDriveText.SetActive(false);
        }
        _rigidbody2D.AddForce(new Vector2(force,0));
    }
}
