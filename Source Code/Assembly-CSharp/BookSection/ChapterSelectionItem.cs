using System;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.ResourceSystem.BookResources;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000073 RID: 115
	public class ChapterSelectionItem : MonoBehaviour
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public Chapter Chapter { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000E4D5 File Offset: 0x0000C6D5
		public Button Button
		{
			get
			{
				return this._button;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000E4DD File Offset: 0x0000C6DD
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000E4E5 File Offset: 0x0000C6E5
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000E4FD File Offset: 0x0000C6FD
		private void OnEnable()
		{
			if (this._button != null)
			{
				this._button.onClick.AddListener(new UnityAction(this.ExecuteSelect));
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E529 File Offset: 0x0000C729
		private void OnDisable()
		{
			if (this._button != null)
			{
				this._button.onClick.RemoveListener(new UnityAction(this.ExecuteSelect));
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E558 File Offset: 0x0000C758
		public void Initialize(Chapter chapter)
		{
			this.Chapter = chapter;
			this._title.SetReference(string.Format("travels_in_calradia_chapter_{0}_title", chapter.Index));
			if (this._pageNumber != null)
			{
				this._pageNumber.text = chapter.FirstPageNumber.ToString();
			}
			this.Refresh();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		private void Refresh()
		{
			this.ChangeImageVisibility(this._leftArrow, this.IsSelected);
			this.ChangeImageVisibility(this._rightArrow, this.IsSelected);
			if (this._brush != null)
			{
				this._brush.IsSelected = this.IsSelected;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000E60C File Offset: 0x0000C80C
		private void ChangeImageVisibility(Image image, bool isVisible)
		{
			if (image != null)
			{
				Color color = image.color;
				color.a = (isVisible ? 1f : 0f);
				image.color = color;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000E646 File Offset: 0x0000C846
		private void ExecuteSelect()
		{
			Action<ChapterSelectionItem> onSelect = this.OnSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect.Invoke(this);
		}

		// Token: 0x04000307 RID: 775
		public Action<ChapterSelectionItem> OnSelect;

		// Token: 0x04000308 RID: 776
		[SerializeField]
		private LocalizedText _title;

		// Token: 0x04000309 RID: 777
		[SerializeField]
		private TMP_Text _pageNumber;

		// Token: 0x0400030A RID: 778
		[SerializeField]
		private Image _leftArrow;

		// Token: 0x0400030B RID: 779
		[SerializeField]
		private Image _rightArrow;

		// Token: 0x0400030C RID: 780
		[SerializeField]
		private Button _button;

		// Token: 0x0400030D RID: 781
		[SerializeField]
		private Brush _brush;

		// Token: 0x0400030E RID: 782
		private bool _isSelected;
	}
}
