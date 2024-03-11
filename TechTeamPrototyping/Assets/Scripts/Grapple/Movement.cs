using System;
using UnityEngine;

// ReSharper disable All

public class Movement : MonoBehaviour {
	#region Variables

	[SerializeField] private float _speed, layerChangeDist;

	[SerializeField]
	private int
		layerCount; //amount of layers in the scene starting from 0, each layer is further away from camera than layer 0

	protected bool canShiftLayers, isActive;
	private int currentLayer;
	private float horizontalInput;

	protected Rigidbody rb;

	/*
	 [SerializeField]
	 private GameObject prefab; //used in "Shoot()"
	 */

	#endregion

	private void Start() {
		rb = GetComponent<Rigidbody>();
		currentLayer = 1;
	}

	protected virtual void Update() {
		Move();
		CheckInput();
	}

	private void CheckInput() {
		if (Input.GetKeyDown(KeyCode.W)) {
			ChangeLayer(1);
		}
		else if (Input.GetKeyDown(KeyCode.S)) {
			ChangeLayer(-1);
		}
	}

	protected void Move() {
		horizontalInput = Input.GetAxis("Horizontal");
		var pos = transform.position;
		transform.position = new Vector3(pos.x + horizontalInput * _speed * Time.deltaTime, pos.y, pos.z);
	}

	protected void ChangeLayer(int direction) {
		var checkPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f,
			gameObject.transform.position.z);

		switch (direction) {
			case -1:
				canShiftLayers = !Physics.Raycast(checkPos, Vector3.back, layerChangeDist);
				break;
			case 1:
				canShiftLayers = !Physics.Raycast(checkPos, Vector3.forward, layerChangeDist);
				break;
		}

		if (direction + currentLayer > layerCount || direction + currentLayer < 0) return;

		if (canShiftLayers) {
			currentLayer += direction;
			var pos = gameObject.transform.position;
			gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z + direction * layerChangeDist);
		}
	}

	public void ResetLayer() {
		currentLayer = 1;
	}

	public void Toggle() {
		isActive = !isActive;

		switch (isActive) {
			case true:
				gameObject.tag = "Player";
				break;
			case false:
				gameObject.tag = "DeactivePlayer";
				break;
		}
	}
}