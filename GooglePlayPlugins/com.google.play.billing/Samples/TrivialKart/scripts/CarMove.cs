// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

/// <summary>
/// A component script for a specific car game object.
/// It controls movement of a specific car in the play page.
/// CarMove script is added as a component to every car game object in the play page.
/// </summary>
public class CarMove : MonoBehaviour
{
    public GameObject tapToDriveText;
    public CarName carName;

    private Rigidbody2D _rigidbody2D;
    private GameManager _gameManger;
    private CarList.Car _carObj;
    private const float NoVelocity = 0.01f;

    private void Start()
    {
        _gameManger = FindObjectOfType<GameManager>();
        // Get the carObj corresponding to the car game object the script attached to.
        _carObj = CarList.GetCarByName(carName);
    }

    // Triggered when player tap the car.
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