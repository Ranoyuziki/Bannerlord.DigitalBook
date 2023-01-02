using System;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000077 RID: 119
	[RequireComponent(typeof(VerticalScrollablePanel))]
	public class PageSoftenerHandler : MonoBehaviour
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000EA8F File Offset: 0x0000CC8F
		private void Awake()
		{
			this._scrollablePanel = base.GetComponent<VerticalScrollablePanel>();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		private void Update()
		{
			Color color = this._topSoftenerImage.color;
			color.a = Mathf.Clamp(this._scrollablePanel.content.localPosition.y - 1f, 0f, 10f) / 10f;
			this._topSoftenerImage.color = color;
		}

		// Token: 0x04000323 RID: 803
		[SerializeField]
		private Image _topSoftenerImage;

		// Token: 0x04000324 RID: 804
		private const float _maxAlphaScrollOffset = 10f;

		// Token: 0x04000325 RID: 805
		private VerticalScrollablePanel _scrollablePanel;
	}
}
