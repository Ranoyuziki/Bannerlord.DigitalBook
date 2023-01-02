using System;
using System.Linq;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000058 RID: 88
	public class MapScreen : MonoBehaviour
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00009B9B File Offset: 0x00007D9B
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00009BB2 File Offset: 0x00007DB2
		private float _currentScale
		{
			get
			{
				return this._mapImage.rectTransform.localScale.x;
			}
			set
			{
				this._mapImage.rectTransform.localScale = new Vector3(value, value, 1f);
				this.OnMapScaleChange();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00009BD6 File Offset: 0x00007DD6
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00009BE8 File Offset: 0x00007DE8
		private Vector3 _currentPosition
		{
			get
			{
				return this._mapImage.rectTransform.localPosition;
			}
			set
			{
				this._mapImage.rectTransform.localPosition = value;
				this.OnMapPositionChange();
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009C04 File Offset: 0x00007E04
		private void Awake()
		{
			this._mapImage.sprite = Game.Instance.ResourceProvider.MapResources.MapImage;
			this._mapFrame.sprite = Game.Instance.ResourceProvider.MapResources.MapFrame;
			float width = this._mapImage.rectTransform.rect.width;
			float height = this._mapImage.rectTransform.rect.height;
			this._initialImageTransformSize = new Vector2(width, height);
			this._panBindingBrush.IsDisabled = true;
			this._zoomOutBindingBrush.IsDisabled = true;
			this._screenManager = Game.Instance.ScreenManager;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009CB8 File Offset: 0x00007EB8
		private void OnEnable()
		{
			Game.Instance.StickNavigationManager.IsEnabled = false;
			ScreenManager screenManager = this._screenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Combine(screenManager.OnCurrentResolutionChanged, new Action(this.OnResolutionChanged));
			FactionMapPieces mapPieces = this._mapPieces;
			mapPieces.OnToggleMapFaction = (Action<MapFactionType, bool>)Delegate.Combine(mapPieces.OnToggleMapFaction, new Action<MapFactionType, bool>(this.OnToggleFaction));
			FrameButtonsController frameButtons = this._frameButtons;
			frameButtons.OnToggleFaction = (Action<MapFactionType, bool>)Delegate.Combine(frameButtons.OnToggleFaction, new Action<MapFactionType, bool>(this.OnToggleFaction));
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			ToggleBordersButton toggleBordersButton = this._toggleBordersButton;
			toggleBordersButton.OnToggle = (Action)Delegate.Combine(toggleBordersButton.OnToggle, new Action(this.ExecuteToggleBorders));
			MapInputEventProvider mapInputEventProvider = this._mapInputEventProvider;
			mapInputEventProvider.OnDrag = (Action<Vector2>)Delegate.Combine(mapInputEventProvider.OnDrag, new Action<Vector2>(this.OnDragMap));
			MapInputEventProvider mapInputEventProvider2 = this._mapInputEventProvider;
			mapInputEventProvider2.OnScroll = (Action<float>)Delegate.Combine(mapInputEventProvider2.OnScroll, new Action<float>(this.OnScrollMap));
			MapInputEventProvider mapInputEventProvider3 = this._mapInputEventProvider;
			mapInputEventProvider3.OnPointerDown = (Action)Delegate.Combine(mapInputEventProvider3.OnPointerDown, new Action(this.OnMapPointerDown));
			this.ResumeNavigationOnEnable();
			this.RefreshImage();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009E18 File Offset: 0x00008018
		private void OnDisable()
		{
			Game.Instance.StickNavigationManager.IsEnabled = true;
			ScreenManager screenManager = this._screenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Remove(screenManager.OnCurrentResolutionChanged, new Action(this.OnResolutionChanged));
			FactionMapPieces mapPieces = this._mapPieces;
			mapPieces.OnToggleMapFaction = (Action<MapFactionType, bool>)Delegate.Remove(mapPieces.OnToggleMapFaction, new Action<MapFactionType, bool>(this.OnToggleFaction));
			FrameButtonsController frameButtons = this._frameButtons;
			frameButtons.OnToggleFaction = (Action<MapFactionType, bool>)Delegate.Remove(frameButtons.OnToggleFaction, new Action<MapFactionType, bool>(this.OnToggleFaction));
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Remove(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			ToggleBordersButton toggleBordersButton = this._toggleBordersButton;
			toggleBordersButton.OnToggle = (Action)Delegate.Remove(toggleBordersButton.OnToggle, new Action(this.ExecuteToggleBorders));
			MapInputEventProvider mapInputEventProvider = this._mapInputEventProvider;
			mapInputEventProvider.OnDrag = (Action<Vector2>)Delegate.Remove(mapInputEventProvider.OnDrag, new Action<Vector2>(this.OnDragMap));
			MapInputEventProvider mapInputEventProvider2 = this._mapInputEventProvider;
			mapInputEventProvider2.OnScroll = (Action<float>)Delegate.Remove(mapInputEventProvider2.OnScroll, new Action<float>(this.OnScrollMap));
			MapInputEventProvider mapInputEventProvider3 = this._mapInputEventProvider;
			mapInputEventProvider3.OnPointerDown = (Action)Delegate.Remove(mapInputEventProvider3.OnPointerDown, new Action(this.OnMapPointerDown));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00009F6C File Offset: 0x0000816C
		private void Start()
		{
			this.RefreshImage();
			PlayerActions playerActions = Game.Instance.PlayerActions;
			this._zoomAction = playerActions.MapActions.Zoom;
			this._panAction = playerActions.MapActions.Pan;
			this._toggleBordersAction = playerActions.MapActions.ToggleBorders;
			this._selectPreviousFactionAction = playerActions.MapActions.SelectPreviousFactionAction;
			this._selectNextFactionAction = playerActions.MapActions.SelectNextFactionAction;
			this._navigateAction = playerActions.UI.Navigate;
			this._cancelAction = playerActions.UI.Cancel;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A018 File Offset: 0x00008218
		private void Update()
		{
			this.HandleZoom();
			this.HandlePan();
			this.CheckBorder();
			this.HandleFrameAndMask();
			this.MoveMapFactionPopup();
			this.HandleToggleBorders();
			this.HandleNavigationBegin();
			this.HandleNavigationAutoPan();
			this.ProcessAutoPan();
			if (this._frameButtons.isActiveAndEnabled)
			{
				if (this._selectPreviousFactionAction.WasPressedThisFrame())
				{
					this._frameButtons.SelectPrevious();
				}
				else if (this._selectNextFactionAction.WasPressedThisFrame())
				{
					this._frameButtons.SelectNext();
				}
			}
			bool isActiveAndEnabled = this._mapFactionPopupController.isActiveAndEnabled;
			if (isActiveAndEnabled)
			{
				if (this._cancelAction.WasPressedThisFrame())
				{
					this.OnToggleFaction(MapFactionType.None, false);
				}
			}
			else
			{
				this._backToMenuButton.CheckActions();
			}
			this._backToMenuButton.IsBindingBlocked = isActiveAndEnabled;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A0D7 File Offset: 0x000082D7
		private void OnMapPointerDown()
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A0DC File Offset: 0x000082DC
		private void OnDragMap(Vector2 delta)
		{
			if (Mathf.Abs(delta.x) > Mathf.Epsilon || Mathf.Abs(delta.y) > Mathf.Epsilon)
			{
				this.OnManualControl();
			}
			Vector3 currentPosition = this._currentPosition + new Vector3(delta.x, delta.y, 0f) * 0.25f * this._currentScale;
			this._currentPosition = currentPosition;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A151 File Offset: 0x00008351
		private void OnScrollMap(float scroll)
		{
			this.Zoom(scroll);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A15C File Offset: 0x0000835C
		private void HandleZoom()
		{
			float zoomValue = this._zoomAction.ReadValue<float>();
			this.Zoom(zoomValue);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A17C File Offset: 0x0000837C
		private void Zoom(float zoomValue)
		{
			if (Mathf.Abs(zoomValue) > Mathf.Epsilon)
			{
				this.OnManualControl();
			}
			int num = (zoomValue > Mathf.Epsilon) ? 1 : ((zoomValue < -Mathf.Epsilon) ? -1 : 0);
			float num2 = (float)num * (Game.Instance.IsCurrentSchemeGamepad ? 0.1f : 0.75f);
			this._zoomSpeed = Mathf.Clamp(this._zoomSpeed + num2, -1.25f, 1.25f);
			float num3 = 1f;
			float num4 = this._minScale + this._minScale * 0.05f;
			float num5 = this._maxScale - this._maxScale * 0.05f;
			if (this._currentScale < num4 && this._zoomSpeed < 0f)
			{
				num3 = (this._currentScale - this._minScale) / (num4 - this._minScale);
			}
			else if (this._currentScale > num5 && this._zoomSpeed > 0f)
			{
				num3 = (this._maxScale - this._currentScale) / (this._maxScale - num5);
			}
			if (Game.Instance.IsCurrentSchemeGamepad)
			{
				num3 = num3 * 0.9f + 0.1f;
			}
			float num6 = this._zoomSpeed * num3 * this._currentScale * Time.deltaTime;
			float currentScale = this._currentScale;
			this._currentScale = Mathf.Clamp(this._currentScale + num6, this._minScale, this._maxScale);
			if (!Game.Instance.IsCurrentSchemeGamepad && Mathf.Abs(this._currentScale - currentScale) > Mathf.Epsilon)
			{
				if (num != 0)
				{
					ScreenManager screenManager = Game.Instance.ScreenManager;
					this._zoomLastMousePosition = (Mouse.current.position.ReadValue() - screenManager.CurrentResolution / 2f) / screenManager.ResolutionScale;
				}
				Vector2 vector = this._zoomLastMousePosition - new Vector2(this._currentPosition.x, this._currentPosition.y);
				Vector2 vector2 = vector / currentScale * this._currentScale - vector;
				this._currentPosition -= new Vector3(vector2.x, vector2.y, 0f);
			}
			this._zoomSpeed = Mathf.MoveTowards(this._zoomSpeed, 0f, 3f * Time.deltaTime);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A3D0 File Offset: 0x000085D0
		private void HandlePan()
		{
			Vector2 vector = this._panAction.ReadValue<Vector2>();
			if (Mathf.Abs(vector.x) > Mathf.Epsilon || Mathf.Abs(vector.y) > Mathf.Epsilon)
			{
				this.OnManualControl();
			}
			this._normalizedPanSpeed = Vector2.MoveTowards(this._normalizedPanSpeed, vector, 4f * Time.deltaTime);
			Vector2 vector2 = this._normalizedPanSpeed * 300f;
			Vector3 currentPosition = this._currentPosition - new Vector3(vector2.x, vector2.y, 0f) * Time.deltaTime * this._currentScale;
			this._currentPosition = currentPosition;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A480 File Offset: 0x00008680
		private void CheckBorder()
		{
			Vector2 vector = this._initialImageTransformSize * this._currentScale * 0.5f;
			Vector3 currentPosition = this._currentPosition;
			float num = currentPosition.x + vector.x;
			float num2 = currentPosition.x - vector.x;
			float num3 = currentPosition.y + vector.y;
			float num4 = currentPosition.y - vector.y;
			if (num < this._border.x)
			{
				currentPosition..ctor(this._border.x - vector.x, currentPosition.y, currentPosition.z);
			}
			else if (num2 > -this._border.x)
			{
				currentPosition..ctor(-this._border.x + vector.x, currentPosition.y, currentPosition.z);
			}
			if (num3 < this._border.y)
			{
				currentPosition..ctor(currentPosition.x, this._border.y - vector.y, currentPosition.z);
			}
			else if (num4 > -this._border.y)
			{
				currentPosition..ctor(currentPosition.x, -this._border.y + vector.y, currentPosition.z);
			}
			this._currentPosition = currentPosition;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A5C4 File Offset: 0x000087C4
		private void MoveMapFactionPopup()
		{
			if (this._mapFactionPopupController.isActiveAndEnabled && this._selectedFaction != null)
			{
				Vector2 vector = this._selectedFaction.Position * this._currentScale + this._selectedFaction.PopupOffset;
				this._mapFactionPopupController.RectTransform.localPosition = this._currentPosition + new Vector3(vector.x, vector.y, 0f);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A640 File Offset: 0x00008840
		private void HandleFrameAndMask()
		{
			Vector3 vector = this._currentPosition * 0.05f;
			this._mapFrame.rectTransform.localPosition = vector;
			float num = (this._currentScale - this._minScale) * 0.05f + 1f;
			this._mapFrame.rectTransform.localScale = new Vector3(num, num, num);
			float num2 = (num - 1f) * this._mapMask.rectTransform.rect.width * 0.5f;
			float num3 = (num - 1f) * this._mapMask.rectTransform.rect.height * 0.5f;
			Vector4 vector2;
			vector2..ctor(-num2, -num3, -num2, -num3);
			vector2 += new Vector4(vector.x, vector.y, -vector.x, -vector.y);
			this._mapMask.padding = vector2;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A735 File Offset: 0x00008935
		private void HandleToggleBorders()
		{
			if (this._toggleBordersAction.WasPressedThisFrame())
			{
				this.ExecuteToggleBorders();
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A74C File Offset: 0x0000894C
		private void HandleNavigationBegin()
		{
			if (Game.Instance.IsCurrentSchemeGamepad && EventSystem.current.currentSelectedGameObject == null && this._mapPieces.gameObject.activeSelf)
			{
				Vector2 vector = this._navigateAction.ReadValue<Vector2>();
				if (Mathf.Abs(vector.x) > Mathf.Epsilon || Mathf.Abs(vector.y) > Mathf.Epsilon)
				{
					EventSystem.current.SetSelectedGameObject(this._navigationBeginFaction);
				}
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A7CC File Offset: 0x000089CC
		private void HandleNavigationAutoPan()
		{
			if (Game.Instance.IsCurrentSchemeGamepad && EventSystem.current.currentSelectedGameObject != this._lastSelectedNavigationObject && this._mapPieces.gameObject.activeSelf)
			{
				this._lastSelectedNavigationObject = EventSystem.current.currentSelectedGameObject;
				IFactionMapPieceItem factionMapPieceItem = Enumerable.FirstOrDefault<IFactionMapPieceItem>(this._mapPieces.Pieces, (IFactionMapPieceItem x) => x.GameObject == this._lastSelectedNavigationObject);
				if (factionMapPieceItem != null)
				{
					this.BeginAutoPan(factionMapPieceItem.Position);
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A84C File Offset: 0x00008A4C
		private void ResumeNavigationOnEnable()
		{
			GameObject gameObject = (this._selectedFaction != null) ? this._selectedFaction.GameObject : null;
			EventSystem.current.SetSelectedGameObject(gameObject);
			this._lastSelectedNavigationObject = gameObject;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A884 File Offset: 0x00008A84
		private void ExecuteToggleBorders()
		{
			EventSystem.current.SetSelectedGameObject(null);
			this._lastSelectedNavigationObject = null;
			bool activeSelf = this._mapPieces.gameObject.activeSelf;
			if (activeSelf)
			{
				this.OnToggleFaction(MapFactionType.None, false);
			}
			this._mapPieces.gameObject.SetActive(!activeSelf);
			this._frameButtons.gameObject.SetActive(!activeSelf);
			this._toggleBordersButton.OnBordersVisibilityChanged(!activeSelf);
			Notification notification = Game.Instance.Notification;
			LocalizedString localizedString = LocalizedText.CreateLocalizedString("ui_notification_political_map");
			localizedString.Add("COLOR", new StringVariable
			{
				Value = (activeSelf ? notification.OffColorCode : notification.OnColorCode)
			});
			localizedString.Add("STATUS", LocalizedText.CreateLocalizedString("ui_notification_" + (activeSelf ? "off" : "on")));
			notification.Display(localizedString);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A964 File Offset: 0x00008B64
		private void RefreshImage()
		{
			if (Vector2.Distance(this._lastCheckedResolution, this._screenManager.CurrentResolution) > Mathf.Epsilon)
			{
				this._lastCheckedResolution = this._screenManager.CurrentResolution;
				float num = this._initialImageTransformSize.x * this._screenManager.ResolutionScale;
				int height = this._mapImage.sprite.texture.height;
				float num2 = this._initialImageTransformSize.y * this._screenManager.ResolutionScale;
				this._minScale = Mathf.Min(this._screenManager.CurrentResolution.x / num, 1f);
				this._maxScale = Mathf.Max((float)height / num2, 1f);
				this._border = this._minScale * this._initialImageTransformSize * 0.5f;
				this._currentScale = this._minScale;
				this._currentPosition = Vector3.zero;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000AA58 File Offset: 0x00008C58
		private void OnToggleFaction(MapFactionType selectedFactionType, bool isFromFrameButtons = false)
		{
			if (selectedFactionType == MapFactionType.None || (this._selectedFaction != null && this._selectedFaction.Faction == selectedFactionType))
			{
				this._selectedFaction = null;
			}
			else
			{
				this._selectedFaction = Enumerable.FirstOrDefault<IFactionMapPieceItem>(this._mapPieces.Pieces, (IFactionMapPieceItem x) => x.Faction == selectedFactionType);
			}
			bool flag = this._selectedFaction != null;
			this._mapFactionPopupController.gameObject.SetActive(flag);
			this._borderGlow.gameObject.SetActive(flag);
			this._mapPieces.OnSelectedFactionChanged(flag ? this._selectedFaction.Faction : MapFactionType.None, this._borderGlow);
			this._frameButtons.OnSelectedFactionChanged(flag ? this._selectedFaction.Faction : MapFactionType.None);
			if (flag)
			{
				this._mapFactionPopupController.Refresh(this._selectedFaction);
				this.BeginAutoPan(this._selectedFaction.Position);
			}
			if (selectedFactionType != MapFactionType.None)
			{
				Game.Instance.AudioManager.PlayAudioClip(isFromFrameButtons ? this._frameFactionSelectionAudioClip : this._mapFactionSelectionAudioClip);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000AB84 File Offset: 0x00008D84
		private void BeginAutoPan(Vector2 targetPosition)
		{
			this._normalizedPanSpeed = Vector2.zero;
			this._zoomSpeed = 0f;
			this._autoPanAnimation = new AnimationProgress(this._autoPanAnimationDuration, AnimationProgressType.EaseOutCubic);
			this._normalizedPanSpeed = Vector2.zero;
			this._autoPanBeginPosition = this._currentPosition;
			this._autoPanEndPosition = (this._currentScale - this._minScale) * targetPosition * -1f;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		private void ProcessAutoPan()
		{
			if (this._autoPanAnimation != null)
			{
				this._autoPanAnimation.Progress(Time.deltaTime);
				this._currentPosition = Vector2.Lerp(this._autoPanBeginPosition, this._autoPanEndPosition, this._autoPanAnimation.Progression);
				if (this._autoPanAnimation.IsFinished)
				{
					this.EndAutoPan();
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000AC57 File Offset: 0x00008E57
		private void EndAutoPan()
		{
			this._autoPanAnimation = null;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000AC60 File Offset: 0x00008E60
		private void OnManualControl()
		{
			this.EndAutoPan();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AC68 File Offset: 0x00008E68
		private void OnMapScaleChange()
		{
			bool flag = this._currentScale < this._maxScale - Mathf.Epsilon;
			bool flag2 = this._currentScale > this._minScale + Mathf.Epsilon;
			this._panBindingBrush.IsDisabled = !flag2;
			this._zoomInBindingBrush.IsDisabled = !flag;
			this._zoomOutBindingBrush.IsDisabled = !flag2;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000ACCC File Offset: 0x00008ECC
		private void OnMapPositionChange()
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000ACCE File Offset: 0x00008ECE
		private void OnResolutionChanged()
		{
			this.RefreshImage();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000ACD6 File Offset: 0x00008ED6
		private void OnControlSchemeChanged()
		{
			EventSystem.current.SetSelectedGameObject(null);
			this._lastSelectedNavigationObject = null;
		}

		// Token: 0x04000215 RID: 533
		[SerializeField]
		private Image _mapImage;

		// Token: 0x04000216 RID: 534
		[SerializeField]
		private FactionMapPieces _mapPieces;

		// Token: 0x04000217 RID: 535
		[SerializeField]
		private MapFactionPopupController _mapFactionPopupController;

		// Token: 0x04000218 RID: 536
		[SerializeField]
		private GameObject _navigationBeginFaction;

		// Token: 0x04000219 RID: 537
		[SerializeField]
		private BackToMenuButton _backToMenuButton;

		// Token: 0x0400021A RID: 538
		[SerializeField]
		private ToggleBordersButton _toggleBordersButton;

		// Token: 0x0400021B RID: 539
		[SerializeField]
		private RectMask2D _mapMask;

		// Token: 0x0400021C RID: 540
		[SerializeField]
		private Image _mapFrame;

		// Token: 0x0400021D RID: 541
		[SerializeField]
		private MapInputEventProvider _mapInputEventProvider;

		// Token: 0x0400021E RID: 542
		[SerializeField]
		private Brush _panBindingBrush;

		// Token: 0x0400021F RID: 543
		[SerializeField]
		private Brush _zoomInBindingBrush;

		// Token: 0x04000220 RID: 544
		[SerializeField]
		private Brush _zoomOutBindingBrush;

		// Token: 0x04000221 RID: 545
		[SerializeField]
		private Image _borderGlow;

		// Token: 0x04000222 RID: 546
		[SerializeField]
		private FrameButtonsController _frameButtons;

		// Token: 0x04000223 RID: 547
		[SerializeField]
		private AudioClip _mapFactionSelectionAudioClip;

		// Token: 0x04000224 RID: 548
		[SerializeField]
		private AudioClip _frameFactionSelectionAudioClip;

		// Token: 0x04000225 RID: 549
		private float _minScale;

		// Token: 0x04000226 RID: 550
		private float _maxScale;

		// Token: 0x04000227 RID: 551
		private InputAction _zoomAction;

		// Token: 0x04000228 RID: 552
		private InputAction _panAction;

		// Token: 0x04000229 RID: 553
		private InputAction _toggleBordersAction;

		// Token: 0x0400022A RID: 554
		private InputAction _selectPreviousFactionAction;

		// Token: 0x0400022B RID: 555
		private InputAction _selectNextFactionAction;

		// Token: 0x0400022C RID: 556
		private InputAction _navigateAction;

		// Token: 0x0400022D RID: 557
		private InputAction _cancelAction;

		// Token: 0x0400022E RID: 558
		private Vector2 _border;

		// Token: 0x0400022F RID: 559
		private Vector2 _initialImageTransformSize;

		// Token: 0x04000230 RID: 560
		private ScreenManager _screenManager;

		// Token: 0x04000231 RID: 561
		private const float _mapFrameScaleMultiplier = 0.05f;

		// Token: 0x04000232 RID: 562
		private const float _mapFramePositionMultiplier = 0.05f;

		// Token: 0x04000233 RID: 563
		private Vector2 _normalizedPanSpeed;

		// Token: 0x04000234 RID: 564
		private const float _panNormalizedAcceleration = 4f;

		// Token: 0x04000235 RID: 565
		private const float _panSpeedMultiplier = 300f;

		// Token: 0x04000236 RID: 566
		private const float _dragPanMultiplier = 0.25f;

		// Token: 0x04000237 RID: 567
		private Vector2 _zoomLastMousePosition;

		// Token: 0x04000238 RID: 568
		private float _zoomSpeed;

		// Token: 0x04000239 RID: 569
		private const float _maxZoomSpeed = 1.25f;

		// Token: 0x0400023A RID: 570
		private const float _zoomDeceleration = 3f;

		// Token: 0x0400023B RID: 571
		private const float _zoomFrictionScaleRatio = 0.05f;

		// Token: 0x0400023C RID: 572
		private const float _gamepadZoomMultiplier = 0.1f;

		// Token: 0x0400023D RID: 573
		private const float _keyboardMouseZoomMultiplier = 0.75f;

		// Token: 0x0400023E RID: 574
		private Vector2 _autoPanBeginPosition;

		// Token: 0x0400023F RID: 575
		private Vector2 _autoPanEndPosition;

		// Token: 0x04000240 RID: 576
		private AnimationProgress _autoPanAnimation;

		// Token: 0x04000241 RID: 577
		private float _autoPanAnimationDuration = 1f;

		// Token: 0x04000242 RID: 578
		private IFactionMapPieceItem _selectedFaction;

		// Token: 0x04000243 RID: 579
		private GameObject _lastSelectedNavigationObject;

		// Token: 0x04000244 RID: 580
		private Vector2 _lastCheckedResolution = Vector2.zero;
	}
}
