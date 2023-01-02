using System;
using TaleWorlds.CompanionBook.ResourceSystem;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000012 RID: 18
	public class LoadingScreen : MonoBehaviour
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003981 File Offset: 0x00001B81
		private void Awake()
		{
			this._screenToOpen = ScreenType.None;
			this._resourceProvider = Game.Instance.ResourceProvider;
			this._screenManager = Game.Instance.ScreenManager;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000039AA File Offset: 0x00001BAA
		private void OnDisable()
		{
			this._screenToOpen = ScreenType.None;
			this._loadingBar.value = 0f;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000039C4 File Offset: 0x00001BC4
		private void Update()
		{
			if (this._resourceProvider.IsLoading)
			{
				this._loadingBar.value = Mathf.MoveTowards(this._loadingBar.value, this._resourceProvider.LoadingProgress, Time.deltaTime * 0.65f);
				return;
			}
			if (this._screenToOpen != ScreenType.None && this._screenToOpen != ScreenType.Loading)
			{
				this._screenManager.SwitchToScreen(this._screenToOpen, Array.Empty<object>());
				return;
			}
			this._screenManager.SwitchToScreen(ScreenType.MainMenu, Array.Empty<object>());
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003A4A File Offset: 0x00001C4A
		public void SetScreenToOpen(ScreenType screenToOpen)
		{
			this._screenToOpen = screenToOpen;
		}

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private Slider _loadingBar;

		// Token: 0x0400005B RID: 91
		private const float _loadingBarValueMaxDelta = 0.65f;

		// Token: 0x0400005C RID: 92
		private ScreenType _screenToOpen;

		// Token: 0x0400005D RID: 93
		private ResourceProvider _resourceProvider;

		// Token: 0x0400005E RID: 94
		private ScreenManager _screenManager;
	}
}
