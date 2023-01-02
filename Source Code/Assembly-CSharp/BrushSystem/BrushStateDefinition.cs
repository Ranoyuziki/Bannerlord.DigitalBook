using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook.BrushSystem
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class BrushStateDefinition
	{
		// Token: 0x040002D4 RID: 724
		public BrushState State;

		// Token: 0x040002D5 RID: 725
		public Color Color;

		// Token: 0x040002D6 RID: 726
		public Sprite Sprite;

		// Token: 0x040002D7 RID: 727
		public bool UseMargins;

		// Token: 0x040002D8 RID: 728
		[Tooltip("Bottom Left Top Right")]
		public Vector4 Margins;
	}
}
