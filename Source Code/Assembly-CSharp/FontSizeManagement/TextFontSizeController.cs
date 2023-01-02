using System;
using TMPro;
using UnityEngine;

namespace TaleWorlds.CompanionBook.FontSizeManagement
{
	// Token: 0x0200005E RID: 94
	[RequireComponent(typeof(TMP_Text))]
	public class TextFontSizeController : MonoBehaviour
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000B0D1 File Offset: 0x000092D1
		private void Awake()
		{
			this._text = base.GetComponent<TMP_Text>();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B0DF File Offset: 0x000092DF
		private void Start()
		{
			this.Refresh();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B0E7 File Offset: 0x000092E7
		private void OnEnable()
		{
			FontSizeManager fontSizeManager = Game.Instance.FontSizeManager;
			fontSizeManager.OnSelectedFontSizeChanged = (Action)Delegate.Combine(fontSizeManager.OnSelectedFontSizeChanged, new Action(this.OnFontSizeChanged));
			this.Refresh();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B11A File Offset: 0x0000931A
		private void OnDisable()
		{
			FontSizeManager fontSizeManager = Game.Instance.FontSizeManager;
			fontSizeManager.OnSelectedFontSizeChanged = (Action)Delegate.Remove(fontSizeManager.OnSelectedFontSizeChanged, new Action(this.OnFontSizeChanged));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B147 File Offset: 0x00009347
		private void Refresh()
		{
			this._text.fontSize = this.GetFontSize(Game.Instance.FontSizeManager.SelectedFontSizeType);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B169 File Offset: 0x00009369
		private void OnFontSizeChanged()
		{
			this.Refresh();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B174 File Offset: 0x00009374
		private float GetFontSize(FontSizeType fontSizeType)
		{
			float result = this._defaultFontSize;
			if (fontSizeType != FontSizeType.Small)
			{
				if (fontSizeType == FontSizeType.Large)
				{
					result = this._largeFontSize;
				}
			}
			else
			{
				result = this._smallFontSize;
			}
			return result;
		}

		// Token: 0x04000256 RID: 598
		[SerializeField]
		private float _smallFontSize;

		// Token: 0x04000257 RID: 599
		[SerializeField]
		private float _defaultFontSize;

		// Token: 0x04000258 RID: 600
		[SerializeField]
		private float _largeFontSize;

		// Token: 0x04000259 RID: 601
		private TMP_Text _text;
	}
}
