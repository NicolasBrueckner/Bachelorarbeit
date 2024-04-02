using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridController : MonoBehaviour
{
	public int2 gridSize;
	public float cellHalfSize;
	public Grid currentGrid;
	public DebugGizmos debugGizmos;

	private InputActions _actions;
	private InputAction _createGridAction;

	private void Awake()
	{
		_actions = new InputActions();
	}

	private void OnEnable()
	{
		_createGridAction = _actions.Debug.CreateGrid;
		_createGridAction.Enable();

		_createGridAction.performed += OnCreateGrid;
	}

	private void OnDisable()
	{
		_createGridAction.performed -= OnCreateGrid;

		_actions.Debug.CreateGrid.Disable();
	}

	private void InitializeGrid()
	{
		currentGrid = new Grid(cellHalfSize, gridSize);
		currentGrid.CreateGrid();
		debugGizmos.SetFlowField(currentGrid);
	}

	private void OnCreateGrid(InputAction.CallbackContext context)
	{
		InitializeGrid();
	}
}
