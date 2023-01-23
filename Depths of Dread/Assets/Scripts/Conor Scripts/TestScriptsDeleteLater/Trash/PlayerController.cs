using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float speed = 11f;

    Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 moveVelocity = (transform.right * moveInput.x + transform.forward * moveInput.y) * speed;// new Vector3(moveInput.x, moveInput.y,0f ); 
        controller.Move(moveInput * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _moveInput)
    {
        moveInput = _moveInput;
        print(moveInput);
    }
}
