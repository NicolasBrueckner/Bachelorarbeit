using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor( typeof( obsolete_sectordata ) )]
public class obsolete_sectoreditor : Editor
{
	public override void OnInspectorGUI()
	{
		obsolete_sectordata sectorData = ( obsolete_sectordata )target;

		sectorData.InitializeCostField();
		EditorGUI.BeginChangeCheck();

		sectorData.background = ( Sprite )EditorGUILayout.ObjectField(
			"Background", sectorData.background, typeof( Sprite ), allowSceneObjects: false );
		DisplayDetailedBackground( sectorData );

		Vector2Int newSize = EditorGUILayout.Vector2IntField( "Size", sectorData.editorSize );

		if ( EditorGUI.EndChangeCheck() )
		{
			sectorData.editorSize = newSize;
			sectorData.InitializeCostField();
		}

		if ( sectorData.costs != null )
		{
			EditorGUILayout.LabelField( "Cost Field" );
			EditorGUI.indentLevel++;

			for ( int y = 0; y < sectorData.editorSize.y; y++ )
			{
				EditorGUILayout.BeginHorizontal();
				for ( int x = 0; x < sectorData.editorSize.x; x++ )
				{
					int intValue = EditorGUILayout.IntField( sectorData.costs[ x ][ y ] );
					intValue = Mathf.Clamp( intValue, 0, 255 );
					sectorData.costs[ x ][ y ] = ( byte )intValue;
				}
				EditorGUILayout.EndHorizontal();
			}
		}
		EditorGUI.indentLevel--;

		if ( EditorGUI.EndChangeCheck() )
			EditorUtility.SetDirty( sectorData );

		if ( GUI.changed )
			EditorUtility.SetDirty( target );
	}

	private void DisplayDetailedBackground( obsolete_sectordata sectorData )
	{
		if ( sectorData.background != null )
		{
			Rect rect = GUILayoutUtility.GetRect( GUIContent.none, GUIStyle.none, GUILayout.Height( EditorGUIUtility.currentViewWidth ) );

			if ( Event.current.type == EventType.Repaint )
			{
				var fullRect = new Rect( rect.x, rect.y, rect.width, rect.height );
				GUI.DrawTexture( fullRect, sectorData.background.texture, ScaleMode.ScaleToFit );
				DrawCostFieldOverlay( fullRect, sectorData );
			}
		}
	}

	private void DrawCostFieldOverlay( Rect textureRect, obsolete_sectordata sectorData )
	{
		float cellWidth = textureRect.width / sectorData.editorSize.x;
		float cellHeight = textureRect.height / sectorData.editorSize.y;

		GUIStyle textStyle = new GUIStyle( GUI.skin.label )
		{
			alignment = TextAnchor.MiddleCenter,
			fontSize = 20,
			normal = { textColor = Color.white }
		};

		for ( int x = 0; x < sectorData.editorSize.x; x++ )
		{
			for ( int y = 0; y < sectorData.editorSize.y; y++ )
			{
				float cellX = textureRect.x + ( x * cellWidth );
				float cellY = textureRect.y + ( y * cellHeight );
				var cellRect = new Rect( cellX, cellY, cellWidth, cellHeight );

				Handles.color = Color.black;
				Handles.DrawLine(
					new Vector3( cellRect.xMin, cellRect.yMin ),
					new Vector3( cellRect.xMax, cellRect.yMin ) );
				Handles.DrawLine(
					new Vector3( cellRect.xMin, cellRect.yMin ),
					new Vector3( cellRect.xMin, cellRect.yMax ) );

				GUI.Label( cellRect, sectorData.costs[ x ][ y ].ToString(), textStyle );
			}
		}
	}
}