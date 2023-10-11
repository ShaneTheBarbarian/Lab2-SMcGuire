using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerControl : MonoBehaviour
{
	public float MouseSensitivity;
	public CharacterController CC;
	public Transform CamTransform;
	public float MoveSpeed;
	public float Gravity = -9.8f;
	public float JumpSpeed;
	public float damage = 10f;
	
	public TMPro.TMP_Text healthText;
	public int health;

	public float verticalSpeed;

	private float camRotation = 0f;

    private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
	private void Update()
	{

		float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
		camRotation -= mouseInputY;
		camRotation = Mathf.Clamp(camRotation, -90f, 90f);
		CamTransform.localRotation = Quaternion.Euler(camRotation, 0f, 0f);

		float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX));
		
		Vector3 movement = Vector3.zero;

		// X/Z movement
		float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
		float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

		movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

		if (CC.isGrounded)
		{
			verticalSpeed = 0f;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				verticalSpeed = JumpSpeed;
			}
		}

		verticalSpeed += (Gravity * Time.deltaTime);
		movement += (transform.up * verticalSpeed * Time.deltaTime);

		CC.Move(movement);
		
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if (Physics.Raycast(CamTransform.position, CamTransform.forward, out hit))
			{
				EnemyTarget enemy = hit.collider.GetComponent<EnemyTarget>();

				if (enemy != null)
				{
					enemy.TakeDamage(5);
					health += 5;
					healthText.text = "Health:" + health;
				}
				
				Debug.DrawLine(CamTransform.position + new Vector3(0f, -1f, 0f), hit.point, Color.green, 1f);
				Debug.Log(hit.collider.gameObject.name);
			}
			else
			{
				Debug.DrawRay(CamTransform.position + new Vector3(0f, -1f, 0f), CamTransform.forward * 100f, Color.red, 1f);
			}
			
			
		
		
		}	 
		

			
	}	
}

