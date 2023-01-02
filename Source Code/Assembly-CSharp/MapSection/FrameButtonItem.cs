using System;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000052 RID: 82
	[RequireComponent(typeof(Brush))]
	[RequireComponent(typeof(Button))]
	public class FrameButtonItem : MonoBehaviour
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009353 File Offset: 0x00007553
		public MapFactionType Faction
		{
			get
			{
				return this._faction;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000935B File Offset: 0x0000755B
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00009363 File Offset: 0x00007563
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

		// Token: 0x06000292 RID: 658 RVA: 0x0000937C File Offset: 0x0000757C
		private void Awake()
		{
			this._button = base.GetComponent<Button>();
			this._brush = base.GetComponent<Brush>();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009396 File Offset: 0x00007596
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteToggleSelect));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000093B4 File Offset: 0x000075B4
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteToggleSelect));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000093D2 File Offset: 0x000075D2
		private void OnIsSelectedChanged(bool isSelected)
		{
			this._brush.IsSelected = isSelected;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000093E0 File Offset: 0x000075E0
		private void ExecuteToggleSelect()
		{
			Action<FrameButtonItem> onToggle = this.OnToggle;
			if (onToggle == null)
			{
				return;
			}
			onToggle.Invoke(this);
		}

		// Token: 0x040001FA RID: 506
		public Action<FrameButtonItem> OnToggle;

		// Token: 0x040001FB RID: 507
		[SerializeField]
		private MapFactionType _faction;

		// Token: 0x040001FC RID: 508
		private Button _button;

		// Token: 0x040001FD RID: 509
		private Brush _brush;

		// Token: 0x040001FE RID: 510
		private bool _isSelected;
	}
}
