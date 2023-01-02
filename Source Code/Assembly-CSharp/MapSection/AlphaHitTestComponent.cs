using System;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x0200004E RID: 78
	[RequireComponent(typeof(Image))]
	public class AlphaHitTestComponent : MonoBehaviour
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00008F94 File Offset: 0x00007194
		private void Start()
		{
			Image image;
			if (base.gameObject.TryGetComponent<Image>(ref image))
			{
				image.alphaHitTestMinimumThreshold = this.AlphaHitTestMinimumThreshold;
			}
		}

		// Token: 0x040001E7 RID: 487
		[Range(0f, 1f)]
		public float AlphaHitTestMinimumThreshold = 0.2f;
	}
}
