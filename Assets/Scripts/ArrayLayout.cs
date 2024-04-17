using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArrayLayout
{
	[System.Serializable]
	public struct rowData_byte
	{
		public byte[] data;
	}

	public rowData_byte[] data = new rowData_byte[ 10 ];
}
