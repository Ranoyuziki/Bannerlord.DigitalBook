using System;
using TaleWorlds.CompanionBook.BrushSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.FontSizeManagement
{
	// Token: 0x0200005C RID: 92
	[RequireComponent(typeof(Brush))]
	[RequireComponent(typeof(Button))]
	public class FontSizeOptionItem : MonoBehaviour
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x0000AFAB File Offset: 0x000091AB
		private void Awake()
		{
			this._button = base.GetComponent<Button>();
			this._brush = base.GetComponent<Brush>();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000AFC5 File Offset: 0x000091C5
		private void Start()
		{
			this.Refresh();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000AFD0 File Offset: 0x000091D0
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteSelect));
			FontSizeManager fontSizeManager = Game.Instance.FontSizeManager;
			fontSizeManager.OnSelectedFontSizeChanged = (Action)Delegate.Combine(fontSizeManager.OnSelectedFontSizeChanged, new Action(this.Refresh));
			this.Refresh();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B02C File Offset: 0x0000922C
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteSelect));
			FontSizeManager fontSizeManager = Game.Instance.FontSizeManager;
			fontSizeManager.OnSelectedFontSizeChanged = (Action)Delegate.Remove(fontSizeManager.OnSelectedFontSizeChanged, new Action(this.Refresh));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B080 File Offset: 0x00009280
		private void Refresh()
		{
			bool isSelected = Game.Instance.FontSizeManager.SelectedFontSizeType == this._fontSizeType;
			this._brush.IsSelected = isSelected;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B0B1 File Offset: 0x000092B1
		private void ExecuteSelect()
		{
			Action<FontSizeType> onSelect = this.OnSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect.Invoke(this._fontSizeType);
		}

		// Token: 0x0400024E RID: 590
		public Action<FontSizeType> OnSelect;

		// Token: 0x0400024F RID: 591
		[SerializeField]
		private FontSizeType _fontSizeType;

		// Token: 0x04000250 RID: 592
		private Button _button;

		// Token: 0x04000251 RID: 593
		private Brush _brush;
	}
}
