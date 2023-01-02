using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook.MenuSection
{
	// Token: 0x02000048 RID: 72
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class MenuLogo : MonoBehaviour
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007F50 File Offset: 0x00006150
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00007F58 File Offset: 0x00006158
		public CanvasGroup CanvasGroup { get; private set; }

		// Token: 0x0600023D RID: 573 RVA: 0x00007F61 File Offset: 0x00006161
		private void Awake()
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this.CanvasGroup = base.GetComponent<CanvasGroup>();
			this._beginPosY = this._rectTransform.localPosition.y;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007F94 File Offset: 0x00006194
		public void OnPositionProgressChanged(float progress)
		{
			Vector3 localPosition = this._rectTransform.localPosition;
			float num = (this._targetPosY - this._beginPosY) * progress;
			localPosition.y = this._beginPosY + num;
			this._rectTransform.localPosition = localPosition;
		}

		// Token: 0x040001B8 RID: 440
		[SerializeField]
		private float _targetPosY;

		// Token: 0x040001B9 RID: 441
		private float _beginPosY;

		// Token: 0x040001BA RID: 442
		private RectTransform _rectTransform;
	}
}
