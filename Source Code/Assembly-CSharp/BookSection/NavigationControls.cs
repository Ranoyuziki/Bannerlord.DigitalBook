using System;
using TaleWorlds.CompanionBook.BrushSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000076 RID: 118
	public class NavigationControls : MonoBehaviour
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000E969 File Offset: 0x0000CB69
		private void Awake()
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E96C File Offset: 0x0000CB6C
		private void OnEnable()
		{
			this._nextPageButton.onClick.AddListener(new UnityAction(this.ExecuteOpenNextPage));
			this._previousPageButton.onClick.AddListener(new UnityAction(this.ExecuteOpenPreviousPage));
			this._toggleChapterSelectionButton.onClick.AddListener(new UnityAction(this.ExecuteToggleChapterSelection));
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		private void OnDisable()
		{
			this._nextPageButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenNextPage));
			this._previousPageButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenPreviousPage));
			this._toggleChapterSelectionButton.onClick.RemoveListener(new UnityAction(this.ExecuteToggleChapterSelection));
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000EA31 File Offset: 0x0000CC31
		public void OnPageChanged(bool isFirstPage, bool isLastPage)
		{
			Brush.SetButtonEnabled(this._previousPageButton, !isFirstPage);
			Brush.SetButtonEnabled(this._nextPageButton, !isLastPage);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000EA51 File Offset: 0x0000CC51
		private void ExecuteOpenNextPage()
		{
			Action onOpenNextPage = this.OnOpenNextPage;
			if (onOpenNextPage == null)
			{
				return;
			}
			onOpenNextPage.Invoke();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000EA63 File Offset: 0x0000CC63
		private void ExecuteOpenPreviousPage()
		{
			Action onOpenPreviousPage = this.OnOpenPreviousPage;
			if (onOpenPreviousPage == null)
			{
				return;
			}
			onOpenPreviousPage.Invoke();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000EA75 File Offset: 0x0000CC75
		private void ExecuteToggleChapterSelection()
		{
			Action onToggleChapterSelection = this.OnToggleChapterSelection;
			if (onToggleChapterSelection == null)
			{
				return;
			}
			onToggleChapterSelection.Invoke();
		}

		// Token: 0x04000319 RID: 793
		public Action OnOpenNextPage;

		// Token: 0x0400031A RID: 794
		public Action OnOpenPreviousPage;

		// Token: 0x0400031B RID: 795
		public Action OnToggleChapterSelection;

		// Token: 0x0400031C RID: 796
		[SerializeField]
		private CanvasGroup _nextPageButtonParent;

		// Token: 0x0400031D RID: 797
		[SerializeField]
		private CanvasGroup _previousPageButtonParent;

		// Token: 0x0400031E RID: 798
		[SerializeField]
		private Button _nextPageButton;

		// Token: 0x0400031F RID: 799
		[SerializeField]
		private Button _previousPageButton;

		// Token: 0x04000320 RID: 800
		[SerializeField]
		private Button _toggleChapterSelectionButton;

		// Token: 0x04000321 RID: 801
		[SerializeField]
		private Sprite _chapterSelectionEnabledSprite;

		// Token: 0x04000322 RID: 802
		[SerializeField]
		private Sprite _chapterSelectionDisabledSprite;
	}
}
