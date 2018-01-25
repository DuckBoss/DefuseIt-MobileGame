using UnityEngine;
using System.Collections;

namespace WireClassSpace {
	[System.Serializable]
	public class WireProperties {
		public GameObject wireOBJ = null;
		public Color wireColor = Color.white;
		public string wireName = string.Empty;
		public int wireID = 0;
		public bool correctWire = false;
	}
}
