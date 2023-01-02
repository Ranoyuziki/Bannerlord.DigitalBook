using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.ScreenSystem
{
	// Token: 0x02000026 RID: 38
	[RequireComponent(typeof(CanvasGroup))]
	public class ScreenComponent : MonoBehaviour
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005CA0 File Offset: 0x00003EA0
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public object[] OpeningArgs { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005CB1 File Offset: 0x00003EB1
		public GameObject GameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005CB9 File Offset: 0x00003EB9
		public ScreenType ScreenType
		{
			get
			{
				return this._screenType;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005CC1 File Offset: 0x00003EC1
		private void Awake()
		{
			this._canvasGroup = base.GetComponent<CanvasGroup>();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005CD0 File Offset: 0x00003ED0
		private void OnEnable()
		{
			this._animation = new AnimationProgress(this._transitionDuration, AnimationProgressType.ExpoEaseOut);
			this._canvasGroup.alpha = this._animation.Progression;
			InputActionMap inputActionMap = Game.Instance.PlayerInput.actions.FindActionMap(this._inputActionMapName, false);
			if (inputActionMap != null)
			{
				inputActionMap.Enable();
			}
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			this.RefreshNavigation();
			Game.Instance.AudioManager.PlayAudioClip(this._clip);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005D6C File Offset: 0x00003F6C
		private void OnDisable()
		{
			this._animation = null;
			this._canvasGroup.alpha = 0f;
			Game instance = Game.Instance;
			if (instance != null)
			{
				if (instance.PlayerInput != null)
				{
					InputActionMap inputActionMap = instance.PlayerInput.actions.FindActionMap(this._inputActionMapName, false);
					if (inputActionMap != null)
					{
						inputActionMap.Disable();
					}
				}
				Game game = instance;
				game.OnControlSchemeChanged = (Action)Delegate.Remove(game.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
				this._lastSelectedObject = ((this._useAutoNavigation && instance.IsCurrentSchemeGamepad) ? EventSystem.current.currentSelectedGameObject : null);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005E14 File Offset: 0x00004014
		private void Update()
		{
			if (this._animation != null)
			{
				this._animation.Progress(Time.deltaTime);
				this._canvasGroup.alpha = this._animation.Progression;
				if (this._animation.IsFinished)
				{
					this._animation = null;
				}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005E63 File Offset: 0x00004063
		private void OnControlSchemeChanged()
		{
			this.RefreshNavigation();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005E6C File Offset: 0x0000406C
		private void RefreshNavigation()
		{
			if (Game.Instance.IsCurrentSchemeGamepad && this._useAutoNavigation)
			{
				if (this._lastSelectedObject != null)
				{
					EventSystem.current.SetSelectedGameObject(this._lastSelectedObject);
					return;
				}
				if (this._defaultSelectedObject != null)
				{
					EventSystem.current.SetSelectedGameObject(this._defaultSelectedObject.gameObject);
				}
			}
		}

		// Token: 0x040000F4 RID: 244
		[SerializeField]
		private ScreenType _screenType;

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		private bool _useAutoNavigation;

		// Token: 0x040000F6 RID: 246
		[SerializeField]
		private Selectable _defaultSelectedObject;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		private string _inputActionMapName;

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		private AudioClip _clip;

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		private float _transitionDuration;

		// Token: 0x040000FA RID: 250
		private CanvasGroup _canvasGroup;

		// Token: 0x040000FB RID: 251
		private AnimationProgress _animation;

		// Token: 0x040000FC RID: 252
		private GameObject _lastSelectedObject;
	}
}
