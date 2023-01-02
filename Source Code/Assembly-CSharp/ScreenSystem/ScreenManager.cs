using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.ScreenSystem
{
	// Token: 0x02000027 RID: 39
	public class ScreenManager : MonoBehaviour
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005ED7 File Offset: 0x000040D7
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005EDF File Offset: 0x000040DF
		public Vector2 ReferenceResolution { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005EE8 File Offset: 0x000040E8
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005EF0 File Offset: 0x000040F0
		public Vector2 CurrentResolution { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005EF9 File Offset: 0x000040F9
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005F01 File Offset: 0x00004101
		public float ResolutionScale { get; private set; }

		// Token: 0x0600015D RID: 349 RVA: 0x00005F0C File Offset: 0x0000410C
		private void Awake()
		{
			HashSet<ScreenType> hashSet = new HashSet<ScreenType>();
			foreach (ScreenComponent screenComponent in this._screens)
			{
				if (!hashSet.Contains(screenComponent.ScreenType))
				{
					hashSet.Add(screenComponent.ScreenType);
				}
			}
			foreach (ScreenType screenType in (ScreenType[])Enum.GetValues(typeof(ScreenType)))
			{
			}
			this.ReferenceResolution = this._canvasScaler.referenceResolution;
			this.UpdateResolution();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005FBC File Offset: 0x000041BC
		private void Start()
		{
			this.SwitchToScreen(ScreenType.Loading, Array.Empty<object>());
			ScreenComponent screenComponent = Enumerable.FirstOrDefault<ScreenComponent>(this._screens, (ScreenComponent x) => x.ScreenType == this._currentScreenType);
			LoadingScreen loadingScreen;
			if (screenComponent != null && screenComponent.TryGetComponent<LoadingScreen>(ref loadingScreen))
			{
				loadingScreen.SetScreenToOpen(ScreenType.MainMenu);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006008 File Offset: 0x00004208
		private void Update()
		{
			if (Mathf.Abs(this.CurrentResolution.x - (float)Screen.width) > Mathf.Epsilon || Mathf.Abs(this.CurrentResolution.y - (float)Screen.height) > Mathf.Epsilon)
			{
				this.UpdateResolution();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006058 File Offset: 0x00004258
		public void SwitchToScreen(ScreenType screenType, params object[] args)
		{
			ScreenComponent screenComponent = Enumerable.FirstOrDefault<ScreenComponent>(this._screens, (ScreenComponent x) => x.ScreenType == this._currentScreenType);
			if (screenComponent != null)
			{
				screenComponent.GameObject.SetActive(false);
			}
			this._currentScreenType = screenType;
			ScreenComponent screenComponent2 = Enumerable.FirstOrDefault<ScreenComponent>(this._screens, (ScreenComponent x) => x.ScreenType == screenType);
			if (screenComponent2 != null)
			{
				screenComponent2.OpeningArgs = args;
				screenComponent2.GameObject.SetActive(true);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000060E8 File Offset: 0x000042E8
		public void SetSafeArea(float ratio)
		{
			Vector3 localScale = this._mainRectTransform.localScale;
			localScale..ctor(ratio, ratio, localScale.z);
			this._mainRectTransform.localScale = localScale;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000611C File Offset: 0x0000431C
		private void UpdateResolution()
		{
			this.CurrentResolution = new Vector2((float)Screen.width, (float)Screen.height);
			this.ResolutionScale = this.CurrentResolution.y / this.ReferenceResolution.y;
			Action onCurrentResolutionChanged = this.OnCurrentResolutionChanged;
			if (onCurrentResolutionChanged == null)
			{
				return;
			}
			onCurrentResolutionChanged.Invoke();
		}

		// Token: 0x04000100 RID: 256
		public Action OnCurrentResolutionChanged;

		// Token: 0x04000101 RID: 257
		[SerializeField]
		private List<ScreenComponent> _screens;

		// Token: 0x04000102 RID: 258
		[SerializeField]
		private CanvasScaler _canvasScaler;

		// Token: 0x04000103 RID: 259
		[SerializeField]
		private RectTransform _mainRectTransform;

		// Token: 0x04000104 RID: 260
		private ScreenType _currentScreenType = ScreenType.None;
	}
}
