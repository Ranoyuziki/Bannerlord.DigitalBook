using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.ResourceSystem.BookResources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000074 RID: 116
	public class ChapterSelectionPopup : MonoBehaviour
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0000E664 File Offset: 0x0000C864
		private void Awake()
		{
			Book bookResources = Game.Instance.ResourceProvider.BookResources;
			List<ChapterSelectionItem> list = new List<ChapterSelectionItem>();
			if (this._chapterItemsList != null)
			{
				foreach (Chapter chapter in bookResources.Chapters)
				{
					ChapterSelectionItem component = Object.Instantiate<GameObject>(this._chapterItemPrefab, this._chapterItemsList.transform).GetComponent<ChapterSelectionItem>();
					component.Initialize(chapter);
					list.Add(component);
				}
			}
			this._chapterItems = list;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E704 File Offset: 0x0000C904
		private void OnEnable()
		{
			this._closeButton.onClick.AddListener(new UnityAction(this.ExecuteClose));
			foreach (ChapterSelectionItem chapterSelectionItem in this._chapterItems)
			{
				chapterSelectionItem.OnSelect = (Action<ChapterSelectionItem>)Delegate.Combine(chapterSelectionItem.OnSelect, new Action<ChapterSelectionItem>(this.OnSelectItem));
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E788 File Offset: 0x0000C988
		private void OnDisable()
		{
			this._closeButton.onClick.RemoveListener(new UnityAction(this.ExecuteClose));
			foreach (ChapterSelectionItem chapterSelectionItem in this._chapterItems)
			{
				chapterSelectionItem.OnSelect = (Action<ChapterSelectionItem>)Delegate.Remove(chapterSelectionItem.OnSelect, new Action<ChapterSelectionItem>(this.OnSelectItem));
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000E80C File Offset: 0x0000CA0C
		public void OnHide()
		{
			EventSystem.current.SetSelectedGameObject(null);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public void OnShow(Chapter chapter)
		{
			this.OnChapterChanged(chapter);
			if (this._selectedItem != null && this._selectedItem.Button != null)
			{
				EventSystem.current.SetSelectedGameObject(this._selectedItem.Button.gameObject);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E86C File Offset: 0x0000CA6C
		public void OnChapterChanged(Chapter chapter)
		{
			if (this._selectedItem != null)
			{
				this._selectedItem.IsSelected = false;
			}
			this._selectedItem = Enumerable.FirstOrDefault<ChapterSelectionItem>(this._chapterItems, (ChapterSelectionItem x) => x.Chapter == chapter);
			if (this._selectedItem != null)
			{
				this._selectedItem.IsSelected = true;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E8D7 File Offset: 0x0000CAD7
		private void OnSelectItem(ChapterSelectionItem item)
		{
			Action<Chapter> onSelectChapter = this.OnSelectChapter;
			if (onSelectChapter == null)
			{
				return;
			}
			onSelectChapter.Invoke(item.Chapter);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E8EF File Offset: 0x0000CAEF
		private void ExecuteClose()
		{
			Action onClose = this.OnClose;
			if (onClose == null)
			{
				return;
			}
			onClose.Invoke();
		}

		// Token: 0x0400030F RID: 783
		public Action<Chapter> OnSelectChapter;

		// Token: 0x04000310 RID: 784
		public Action OnClose;

		// Token: 0x04000311 RID: 785
		[SerializeField]
		private GameObject _chapterItemsList;

		// Token: 0x04000312 RID: 786
		[SerializeField]
		private GameObject _chapterItemPrefab;

		// Token: 0x04000313 RID: 787
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04000314 RID: 788
		private IReadOnlyList<ChapterSelectionItem> _chapterItems;

		// Token: 0x04000315 RID: 789
		private ChapterSelectionItem _selectedItem;
	}
}
