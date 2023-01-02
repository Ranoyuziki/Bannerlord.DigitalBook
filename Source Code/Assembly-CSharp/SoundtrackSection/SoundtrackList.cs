using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.Soundtracks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TaleWorlds.CompanionBook.SoundtrackSection
{
	// Token: 0x0200001E RID: 30
	public class SoundtrackList : MonoBehaviour
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004F5F File Offset: 0x0000315F
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00004F67 File Offset: 0x00003167
		public ISoundtrack HoveredItem
		{
			get
			{
				return this._hoveredItem;
			}
			set
			{
				if (value != this._hoveredItem)
				{
					this._hoveredItem = value;
					Action onHoveredItemChanged = this.OnHoveredItemChanged;
					if (onHoveredItemChanged == null)
					{
						return;
					}
					onHoveredItemChanged.Invoke();
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004F8C File Offset: 0x0000318C
		private void Start()
		{
			SoundtrackPlayer soundtrackPlayer = Game.Instance.SoundtrackPlayer;
			IPlaylist playlist = soundtrackPlayer.Playlist;
			this.Refresh((playlist != null) ? playlist.Soundtracks : null);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004FBC File Offset: 0x000031BC
		private void OnEnable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			IPlaylist playlist = Game.Instance.SoundtrackPlayer.Playlist;
			this.Refresh((playlist != null) ? playlist.Soundtracks : null);
			if (Game.Instance.IsCurrentSchemeGamepad)
			{
				GameObject selectedNavigationObject = EventSystem.current.currentSelectedGameObject;
				SoundtrackListItem soundtrackListItem = Enumerable.FirstOrDefault<SoundtrackListItem>(this._items, (SoundtrackListItem x) => x.Brush.gameObject == selectedNavigationObject);
				this.HoveredItem = ((soundtrackListItem != null) ? soundtrackListItem.Soundtrack : null);
				return;
			}
			this.HoveredItem = null;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000506A File Offset: 0x0000326A
		private void OnDisable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Remove(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			this.DestroyAllChildren();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005098 File Offset: 0x00003298
		private void Refresh(IEnumerable<ISoundtrack> soundtracks)
		{
			this.DestroyAllChildren();
			if (soundtracks != null)
			{
				foreach (ISoundtrack soundtrack in soundtracks)
				{
					SoundtrackListItem component = Object.Instantiate<GameObject>(this._soundtrackItemPrefab, base.transform).GetComponent<SoundtrackListItem>();
					component.Initialize(soundtrack);
					SoundtrackListItem soundtrackListItem = component;
					soundtrackListItem.OnHoverChanged = (Action<ISoundtrack, bool>)Delegate.Combine(soundtrackListItem.OnHoverChanged, new Action<ISoundtrack, bool>(this.OnItemHoverChanged));
					this._items.Add(component);
				}
				this.RefreshNavigation();
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005134 File Offset: 0x00003334
		private void DestroyAllChildren()
		{
			this._items.ForEach(delegate(SoundtrackListItem x)
			{
				x.OnHoverChanged = (Action<ISoundtrack, bool>)Delegate.Remove(x.OnHoverChanged, new Action<ISoundtrack, bool>(this.OnItemHoverChanged));
			});
			this._items.Clear();
			foreach (object obj in base.transform)
			{
				Object.Destroy(((Transform)obj).gameObject);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000051B4 File Offset: 0x000033B4
		private void OnItemHoverChanged(ISoundtrack soundtrack, bool isHovered)
		{
			if (isHovered)
			{
				this.HoveredItem = soundtrack;
				return;
			}
			if (this.HoveredItem == soundtrack)
			{
				this.HoveredItem = null;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000051D1 File Offset: 0x000033D1
		private void OnControlSchemeChanged()
		{
			this.RefreshNavigation();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000051DC File Offset: 0x000033DC
		private void RefreshNavigation()
		{
			if (Game.Instance.IsCurrentSchemeGamepad && this._items.Count > 0)
			{
				SoundtrackListItem soundtrackListItem = Enumerable.FirstOrDefault<SoundtrackListItem>(this._items, (SoundtrackListItem x) => x.Soundtrack.IsSelected);
				SoundtrackListItem soundtrackListItem2 = (soundtrackListItem != null) ? soundtrackListItem : this._items[0];
				EventSystem.current.SetSelectedGameObject(soundtrackListItem2.Brush.gameObject);
			}
		}

		// Token: 0x040000CF RID: 207
		public Action OnHoveredItemChanged;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		private GameObject _soundtrackItemPrefab;

		// Token: 0x040000D1 RID: 209
		private ISoundtrack _hoveredItem;

		// Token: 0x040000D2 RID: 210
		private List<SoundtrackListItem> _items = new List<SoundtrackListItem>();
	}
}
