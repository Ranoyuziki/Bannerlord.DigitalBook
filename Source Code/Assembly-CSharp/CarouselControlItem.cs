using System;
using TaleWorlds.CompanionBook.BrushSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000008 RID: 8
	[RequireComponent(typeof(Button))]
	public class CarouselControlItem : MonoBehaviour
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002705 File Offset: 0x00000905
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000270D File Offset: 0x0000090D
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002715 File Offset: 0x00000915
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
					this.OnIsSelectedChanged(value);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000272E File Offset: 0x0000092E
		private void Awake()
		{
			this._button = base.GetComponent<Button>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000273C File Offset: 0x0000093C
		private void OnDestroy()
		{
			this._onSelected = null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002745 File Offset: 0x00000945
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteSelect));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002763 File Offset: 0x00000963
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteSelect));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002781 File Offset: 0x00000981
		public void Initialize(int index, Action<int> onSelected)
		{
			this._index = index;
			this._onSelected = onSelected;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002791 File Offset: 0x00000991
		private void ExecuteSelect()
		{
			Action<int> onSelected = this._onSelected;
			if (onSelected == null)
			{
				return;
			}
			onSelected.Invoke(this._index);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027AC File Offset: 0x000009AC
		private void OnIsSelectedChanged(bool isSelected)
		{
			Brush brush;
			if (this._button.TryGetComponent<Brush>(ref brush))
			{
				brush.IsSelected = isSelected;
			}
		}

		// Token: 0x04000024 RID: 36
		private Action<int> _onSelected;

		// Token: 0x04000025 RID: 37
		private int _index;

		// Token: 0x04000026 RID: 38
		private bool _isSelected;

		// Token: 0x04000027 RID: 39
		private Button _button;
	}
}
