using System;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000056 RID: 86
	[RequireComponent(typeof(RectTransform))]
	public class MapFactionPopupController : MonoBehaviour
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00009A33 File Offset: 0x00007C33
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00009A3B File Offset: 0x00007C3B
		public RectTransform RectTransform { get; private set; }

		// Token: 0x060002B2 RID: 690 RVA: 0x00009A44 File Offset: 0x00007C44
		private void Awake()
		{
			this.RectTransform = base.GetComponent<RectTransform>();
			this._verticalLayout = this._verticalPopup.GetComponent<VerticalLayoutGroup>();
			this._horizontalLayout = this._horizontalPopup.GetComponent<HorizontalLayoutGroup>();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009A74 File Offset: 0x00007C74
		public void Refresh(IFactionMapPieceItem mapPiece)
		{
			if (mapPiece.Layout == FactionMapPieceItemPopupLayout.BottomToTop || mapPiece.Layout == FactionMapPieceItemPopupLayout.TopToBottom)
			{
				this._horizontalPopup.gameObject.SetActive(false);
				this._verticalPopup.gameObject.SetActive(true);
				this._verticalPopup.Refresh(mapPiece);
				this._verticalLayout.reverseArrangement = (mapPiece.Layout == FactionMapPieceItemPopupLayout.BottomToTop);
				return;
			}
			this._verticalPopup.gameObject.SetActive(false);
			this._horizontalPopup.gameObject.SetActive(true);
			this._horizontalPopup.Refresh(mapPiece);
			this._horizontalLayout.reverseArrangement = (mapPiece.Layout == FactionMapPieceItemPopupLayout.RightToLeft);
		}

		// Token: 0x0400020C RID: 524
		[SerializeField]
		private MapFactionPopup _verticalPopup;

		// Token: 0x0400020D RID: 525
		[SerializeField]
		private MapFactionPopup _horizontalPopup;

		// Token: 0x0400020E RID: 526
		private VerticalLayoutGroup _verticalLayout;

		// Token: 0x0400020F RID: 527
		private HorizontalLayoutGroup _horizontalLayout;
	}
}
