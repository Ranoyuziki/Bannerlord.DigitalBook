using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000009 RID: 9
	[RequireComponent(typeof(CanvasGroup))]
	public class CarouselControls : MonoBehaviour
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000027D7 File Offset: 0x000009D7
		private void Awake()
		{
			this._canvasGroup = base.GetComponent<CanvasGroup>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027E5 File Offset: 0x000009E5
		private void Start()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027E7 File Offset: 0x000009E7
		private void OnDestroy()
		{
			this._onItemSelected = null;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027F0 File Offset: 0x000009F0
		public void Initialize(int count, Action<int> onItemSelected, bool isInteractable)
		{
			if (!this._isInitialized)
			{
				this._canvasGroup.interactable = isInteractable;
				this._canvasGroup.blocksRaycasts = isInteractable;
				this._onItemSelected = onItemSelected;
				for (int i = 0; i < count; i++)
				{
					CarouselControlItem carouselControlItem = Object.Instantiate<CarouselControlItem>(this._itemPrefab, base.transform);
					carouselControlItem.Initialize(i, new Action<int>(this.OnItemSelected));
					this._items.Add(carouselControlItem);
				}
				this._isInitialized = true;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002868 File Offset: 0x00000A68
		public void RefreshSelectedIndex(int index)
		{
			if (this._selectedItem != null)
			{
				this._selectedItem.IsSelected = false;
			}
			this._selectedItem = Enumerable.FirstOrDefault<CarouselControlItem>(this._items, (CarouselControlItem x) => x.Index == index);
			if (this._selectedItem != null)
			{
				this._selectedItem.IsSelected = true;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028D3 File Offset: 0x00000AD3
		private void OnItemSelected(int index)
		{
			Action<int> onItemSelected = this._onItemSelected;
			if (onItemSelected == null)
			{
				return;
			}
			onItemSelected.Invoke(index);
		}

		// Token: 0x04000028 RID: 40
		[SerializeField]
		private CarouselControlItem _itemPrefab;

		// Token: 0x04000029 RID: 41
		private CarouselControlItem _selectedItem;

		// Token: 0x0400002A RID: 42
		private List<CarouselControlItem> _items = new List<CarouselControlItem>();

		// Token: 0x0400002B RID: 43
		private Action<int> _onItemSelected;

		// Token: 0x0400002C RID: 44
		private bool _isInitialized;

		// Token: 0x0400002D RID: 45
		private CanvasGroup _canvasGroup;
	}
}
