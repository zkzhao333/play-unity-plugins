using UnityEngine;

// Control car movement
public class CarMove : MonoBehaviour
{
    public GameObject tapToDriveText;
    public string carName;

    private Rigidbody2D _rigidbody2D;
    private GameManager _gameManger;
    private Car _carObj;
    private const float NoVelocity = 0.01f;

    private void Start()
    {
        _gameManger = FindObjectOfType<GameManager>();
        _carObj = CarList.GetCarByName(carName);
    }

    // Trigger when player tap the car
    private void OnMouseDown()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D.velocity.magnitude < NoVelocity &&
            transform.parent.gameObject.GetComponent<Gas>().HasGas() &&
            _gameManger.IsInPlayPage())
        {
            Drive();
        }
    }

    private void Drive()
    {
        tapToDriveText.SetActive(false);
        _rigidbody2D.AddForce(new Vector2(_carObj.Speed, 0));
    }
}