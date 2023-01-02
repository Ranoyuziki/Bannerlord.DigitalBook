using System;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x0200004F RID: 79
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(Brush))]
	public class FactionMapPieceItem : MonoBehaviour, IFactionMapPieceItem
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00008FCF File Offset: 0x000071CF
		public GameObject GameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00008FD7 File Offset: 0x000071D7
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00008FDF File Offset: 0x000071DF
		public Vector2 Position { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00008FE8 File Offset: 0x000071E8
		public Vector2 PopupOffset
		{
			get
			{
				return this._popupOffset;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00008FF0 File Offset: 0x000071F0
		public MapFactionType Faction
		{
			get
			{
				return this._faction;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00008FF8 File Offset: 0x000071F8
		public FactionMapPieceItemPopupLayout Layout
		{
			get
			{
				return this._layout;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00009000 File Offset: 0x00007200
		public Sprite Banner
		{
			get
			{
				return this._banner;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00009008 File Offset: 0x00007208
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00009010 File Offset: 0x00007210
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

		// Token: 0x0600027F RID: 639 RVA: 0x0000902C File Offset: 0x0000722C
		private void Awake()
		{
			this._button = base.GetComponent<Button>();
			this._image = base.GetComponent<Image>();
			this._brush = base.GetComponent<Brush>();
			MapFaction mapFaction;
			if (Game.Instance.ResourceProvider.MapResources.Factions.TryGetValue(this._faction, ref mapFaction))
			{
				this._image.sprite = mapFaction.Border;
				this._glowSprite = mapFaction.Glow;
			}
			Vector3 localPosition = base.GetComponent<RectTransform>().localPosition;
			this.Position = new Vector2(localPosition.x, localPosition.y);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000090C0 File Offset: 0x000072C0
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteToggleSelect));
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000090DE File Offset: 0x000072DE
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteToggleSelect));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000090FC File Offset: 0x000072FC
		public void RefreshGlow(Image glowImage)
		{
			glowImage.sprite = this._glowSprite;
			glowImage.SetNativeSize();
			Vector3 localPosition = glowImage.rectTransform.localPosition;
			localPosition.x = this.Position.x;
			localPosition.y = this.Position.y;
			glowImage.rectTransform.localPosition = localPosition;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009157 File Offset: 0x00007357
		private void ExecuteToggleSelect()
		{
			Action<MapFactionType> onToggleSelected = this.OnToggleSelected;
			if (onToggleSelected == null)
			{
				return;
			}
			onToggleSelected.Invoke(this.Faction);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000916F File Offset: 0x0000736F
		private void OnIsSelectedChanged(bool value)
		{
			this._brush.IsSelected = value;
		}

		// Token: 0x040001E9 RID: 489
		[SerializeField]
		private Sprite _banner;

		// Token: 0x040001EA RID: 490
		public Action<MapFactionType> OnToggleSelected;

		// Token: 0x040001EB RID: 491
		[SerializeField]
		private MapFactionType _faction;

		// Token: 0x040001EC RID: 492
		[SerializeField]
		private FactionMapPieceItemPopupLayout _layout;

		// Token: 0x040001ED RID: 493
		[SerializeField]
		private Vector2 _popupOffset;

		// Token: 0x040001EE RID: 494
		private Sprite _glowSprite;

		// Token: 0x040001EF RID: 495
		private Button _button;

		// Token: 0x040001F0 RID: 496
		private Image _image;

		// Token: 0x040001F1 RID: 497
		private Brush _brush;

		// Token: 0x040001F2 RID: 498
		private bool _isSelected;
	}
}
