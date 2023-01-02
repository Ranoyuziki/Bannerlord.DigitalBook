using System;
using TaleWorlds.CompanionBook.AudioSystem;
using TaleWorlds.CompanionBook.FontSizeManagement;
using TaleWorlds.CompanionBook.Options;
using TaleWorlds.CompanionBook.ResourceSystem;
using TaleWorlds.CompanionBook.ScreenSystem;
using TaleWorlds.CompanionBook.Soundtracks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200000D RID: 13
	[RequireComponent(typeof(AudioManager))]
	[RequireComponent(typeof(ResourceProvider))]
	[RequireComponent(typeof(StickNavigationManager))]
	[RequireComponent(typeof(FontSizeManager))]
	[RequireComponent(typeof(SoundtrackPlayer))]
	[RequireComponent(typeof(ScreenManager))]
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(CursorManager))]
	public class Game : MonoBehaviour
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A4A File Offset: 0x00000C4A
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002A51 File Offset: 0x00000C51
		public static Game Instance { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A59 File Offset: 0x00000C59
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002A61 File Offset: 0x00000C61
		public int ActiveBookSessionChapter { get; set; } = -1;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A6A File Offset: 0x00000C6A
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002A72 File Offset: 0x00000C72
		public AudioManager AudioManager { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002A7B File Offset: 0x00000C7B
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002A83 File Offset: 0x00000C83
		public ResourceProvider ResourceProvider { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002A8C File Offset: 0x00000C8C
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002A94 File Offset: 0x00000C94
		public SoundtrackPlayer SoundtrackPlayer { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A9D File Offset: 0x00000C9D
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public ScreenManager ScreenManager { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002AAE File Offset: 0x00000CAE
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public FontSizeManager FontSizeManager { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002ABF File Offset: 0x00000CBF
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public StickNavigationManager StickNavigationManager { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002AD0 File Offset: 0x00000CD0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public CursorManager CursorManager { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002AE1 File Offset: 0x00000CE1
		public Notification Notification
		{
			get
			{
				return this._notification;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002AE9 File Offset: 0x00000CE9
		public PlayerActions PlayerActions
		{
			get
			{
				return this._playerActions;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public PlayerInput PlayerInput
		{
			get
			{
				return this._playerInput;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002AF9 File Offset: 0x00000CF9
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002B01 File Offset: 0x00000D01
		public OptionsManager OptionsManager { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002B0C File Offset: 0x00000D0C
		public bool IsCurrentSchemeGamepad
		{
			get
			{
				return this._playerInput != null && this._playerInput.currentControlScheme == this._playerActions.GamepadScheme.name;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B4C File Offset: 0x00000D4C
		private void OnEnable()
		{
			InputUser.onChange += new Action<InputUser, InputUserChange, InputDevice>(this.OnInputUserChange);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B5F File Offset: 0x00000D5F
		private void OnDisable()
		{
			InputUser.onChange -= new Action<InputUser, InputUserChange, InputDevice>(this.OnInputUserChange);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B72 File Offset: 0x00000D72
		private void Awake()
		{
			if (Game.Instance == null)
			{
				Game.Instance = this;
				this.Initialize();
				Object.DontDestroyOnLoad(base.gameObject);
				return;
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public void ChangeBinding(InputAction action, int index, string path)
		{
			if (action != null && !string.IsNullOrEmpty(path) && index >= 0 && index < action.bindings.Count)
			{
				InputBinding inputBinding = action.bindings[index];
				if (inputBinding.effectivePath != path)
				{
					inputBinding.overridePath = path;
					InputActionRebindingExtensions.ApplyBindingOverride(action, index, inputBinding);
					Action<Guid> onActionBindingChanged = this.OnActionBindingChanged;
					if (onActionBindingChanged == null)
					{
						return;
					}
					onActionBindingChanged.Invoke(action.id);
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C16 File Offset: 0x00000E16
		public void Pause()
		{
			if (!this._isPaused)
			{
				this._isPaused = true;
				Action<bool> onGamePauseStateChanged = this.OnGamePauseStateChanged;
				if (onGamePauseStateChanged == null)
				{
					return;
				}
				onGamePauseStateChanged.Invoke(true);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C38 File Offset: 0x00000E38
		public void UnPause()
		{
			if (this._isPaused)
			{
				this._isPaused = false;
				Action<bool> onGamePauseStateChanged = this.OnGamePauseStateChanged;
				if (onGamePauseStateChanged == null)
				{
					return;
				}
				onGamePauseStateChanged.Invoke(false);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C5C File Offset: 0x00000E5C
		private void Initialize()
		{
			this.ResourceProvider = base.GetComponent<ResourceProvider>();
			this.AudioManager = base.GetComponent<AudioManager>();
			this.ScreenManager = base.GetComponent<ScreenManager>();
			this.SoundtrackPlayer = base.GetComponent<SoundtrackPlayer>();
			this.FontSizeManager = base.GetComponent<FontSizeManager>();
			this.StickNavigationManager = base.GetComponent<StickNavigationManager>();
			this.CursorManager = base.GetComponent<CursorManager>();
			this.OptionsManager = new OptionsManager();
			this._playerActions = new PlayerActions();
			this._playerInput = base.GetComponent<PlayerInput>();
			this._playerInput.actions = this._playerActions.asset;
			this._playerInput.SwitchCurrentActionMap("UI");
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002D05 File Offset: 0x00000F05
		private void OnInputUserChange(InputUser inputUser, InputUserChange inputUserChange, InputDevice inputDevice)
		{
			if (inputUserChange == 11)
			{
				Action onControlSchemeChanged = this.OnControlSchemeChanged;
				if (onControlSchemeChanged == null)
				{
					return;
				}
				onControlSchemeChanged.Invoke();
			}
		}

		// Token: 0x0400003A RID: 58
		public Action OnControlSchemeChanged;

		// Token: 0x0400003B RID: 59
		public Action<Guid> OnActionBindingChanged;

		// Token: 0x0400003C RID: 60
		public Action<bool> OnGamePauseStateChanged;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		private Notification _notification;

		// Token: 0x0400003E RID: 62
		private PlayerActions _playerActions;

		// Token: 0x0400003F RID: 63
		private PlayerInput _playerInput;

		// Token: 0x04000040 RID: 64
		private bool _isPaused;
	}
}
