using System;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000075 RID: 117
	[RequireComponent(typeof(LayoutElement))]
	public class IllustrationSizeController : MonoBehaviour
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0000E909 File Offset: 0x0000CB09
		private void Awake()
		{
			this._layoutElement = base.GetComponent<LayoutElement>();
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E917 File Offset: 0x0000CB17
		private void Update()
		{
			this.RefreshSize();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E91F File Offset: 0x0000CB1F
		public void Initialize(RectTransform pageParentRectTransform)
		{
			this._pageParentRectTransform = pageParentRectTransform;
			this.RefreshSize();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E930 File Offset: 0x0000CB30
		private void RefreshSize()
		{
			this._layoutElement.preferredWidth = this._pageParentRectTransform.rect.height * 0.7048387f;
		}

		// Token: 0x04000316 RID: 790
		private LayoutElement _layoutElement;

		// Token: 0x04000317 RID: 791
		private RectTransform _pageParentRectTransform;

		// Token: 0x04000318 RID: 792
		private const float _illustrationWidthHeightRatio = 0.7048387f;
	}
}
