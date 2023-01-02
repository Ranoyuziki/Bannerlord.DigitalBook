using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook.FontSizeManagement
{
	// Token: 0x0200005B RID: 91
	public class FontSizeManager : MonoBehaviour
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000AF67 File Offset: 0x00009167
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000AF6F File Offset: 0x0000916F
		public FontSizeType SelectedFontSizeType { get; private set; }

		// Token: 0x060002F0 RID: 752 RVA: 0x0000AF78 File Offset: 0x00009178
		private void Awake()
		{
			this.SelectedFontSizeType = FontSizeType.Default;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000AF81 File Offset: 0x00009181
		public void ChangeFontSize(FontSizeType fontSizeType)
		{
			if (this.SelectedFontSizeType != fontSizeType)
			{
				this.SelectedFontSizeType = fontSizeType;
				Action onSelectedFontSizeChanged = this.OnSelectedFontSizeChanged;
				if (onSelectedFontSizeChanged == null)
				{
					return;
				}
				onSelectedFontSizeChanged.Invoke();
			}
		}

		// Token: 0x0400024D RID: 589
		public Action OnSelectedFontSizeChanged;
	}
}
