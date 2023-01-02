using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000018 RID: 24
	public class PlayerActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003F82 File Offset: 0x00002182
		public InputActionAsset asset { get; }

		// Token: 0x060000B9 RID: 185 RVA: 0x00003F8C File Offset: 0x0000218C
		public PlayerActions()
		{
			this.asset = InputActionAsset.FromJson("{\n    \"name\": \"PlayerActions\",\n    \"maps\": [\n        {\n            \"name\": \"UI\",\n            \"id\": \"e503b670-e604-4010-ba8c-26b15d1d9d4c\",\n            \"actions\": [\n                {\n                    \"name\": \"Navigate\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"c40171bd-3151-4da2-ab3b-089b2b91ffe7\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Submit\",\n                    \"type\": \"Button\",\n                    \"id\": \"eb986d6e-9c85-4e4a-a7db-3def0943cb73\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"f41f3d9b-0236-4a45-a79c-189d75b876df\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Point\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"40e11fc4-6800-431c-8a08-0b9289a6666f\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Click\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"2ec9116f-e0ff-4241-844b-0fddbc0e4047\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"ScrollWheel\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"32db3f40-c593-4f36-847e-6e7069c1c15e\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"MiddleClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"19f4c20f-ab48-42e9-8f0e-ad49ea095b33\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"RightClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"59cc95a4-f455-4ba6-bc6f-ebb5ac7b70e8\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"StickNavigation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"c0a5266f-1a23-4b1d-bffc-50328e4e79fa\",\n                    \"expectedControlType\": \"Stick\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"NextSoundtrackAction\",\n                    \"type\": \"Button\",\n                    \"id\": \"d1254dd1-e1d0-4a56-b591-3ad55a16c94a\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"PreviousSoundtrackAction\",\n                    \"type\": \"Button\",\n                    \"id\": \"c0097b8b-c281-4c6e-9b78-d8f808006e7a\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ToggleSoundtrackAction\",\n                    \"type\": \"Button\",\n                    \"id\": \"102dcf6c-e00f-4a57-801a-57c52c6f984d\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ChangeFontSize\",\n                    \"type\": \"Button\",\n                    \"id\": \"a4266fb7-74fe-4d40-bbfd-bc449193b584\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"GamepadScroll\",\n                    \"type\": \"Value\",\n                    \"id\": \"a5df855a-ac18-4088-b024-00f419851b21\",\n                    \"expectedControlType\": \"Axis\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"CustomAnyButtonAction\",\n                    \"type\": \"Button\",\n                    \"id\": \"431f89aa-6d8d-412f-82df-4e9f8a22e7b6\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"09983d39-bda0-4c7b-b38b-248b06799229\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1deeffab-9009-46af-a29b-164630f3262a\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e5d8e445-d8bc-40cb-871e-0cd0548c68de\",\n                    \"path\": \"<Keyboard>/enter\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"47dbce02-5341-4b5f-a5f8-0e927744a2c4\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"40e2b874-aec0-4ad2-8c01-463328d378ff\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fec4a0ac-4202-4bad-a3a6-5f9ebe99d8e8\",\n                    \"path\": \"<Mouse>/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;KeyboardMouse\",\n                    \"action\": \"Point\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e358dfaf-6624-4c6d-84dd-039bf06f5f8f\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;KeyboardMouse\",\n                    \"action\": \"Click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8864f373-2679-4069-ae2f-c9c07100f94c\",\n                    \"path\": \"<Mouse>/scroll\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;KeyboardMouse\",\n                    \"action\": \"ScrollWheel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"64ba0214-6e47-4221-9b8c-595aa8d59988\",\n                    \"path\": \"<Mouse>/middleButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;KeyboardMouse\",\n                    \"action\": \"MiddleClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f01be31c-fb1d-4950-b292-29b76e4b3eef\",\n                    \"path\": \"<Mouse>/rightButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;KeyboardMouse\",\n                    \"action\": \"RightClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d7f17b3f-1367-4748-bfce-040d7be4aebe\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"StickNavigation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"6757832b-39c4-497b-b42a-5c65ec05ec2c\",\n                    \"path\": \"<Keyboard>/l\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"NextSoundtrackAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f658d9eb-ceeb-40a0-ba37-83a7820dced7\",\n                    \"path\": \"<Gamepad>/rightStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"NextSoundtrackAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"076d623a-d690-4cc4-8fb2-7b0c05754d6a\",\n                    \"path\": \"<Keyboard>/j\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"PreviousSoundtrackAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b7b01a38-8263-4cb6-ae7c-5e4cdb6570b3\",\n                    \"path\": \"<Gamepad>/rightStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"PreviousSoundtrackAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"413d2fa7-ccfe-4e34-813d-fdbde0e61875\",\n                    \"path\": \"<Keyboard>/k\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"ToggleSoundtrackAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b72397b1-abf5-400c-9722-bfb94bc6fb03\",\n                    \"path\": \"<Gamepad>/rightStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"ToggleSoundtrackAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2dfbb5ec-2bd1-49db-80a5-10e7e2492523\",\n                    \"path\": \"<Keyboard>/tab\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"ChangeFontSize\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4483d83b-3095-45d0-9415-e5f279c74b86\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"ChangeFontSize\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"1D Axis\",\n                    \"id\": \"ec9aea72-440f-4800-893f-14e25a234e3d\",\n                    \"path\": \"1DAxis\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"GamepadScroll\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"negative\",\n                    \"id\": \"25d570e0-75c8-46d5-aa29-36f25eb3320f\",\n                    \"path\": \"<Gamepad>/rightStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"GamepadScroll\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"positive\",\n                    \"id\": \"d73164a1-7e09-4342-8951-bec0303d79fd\",\n                    \"path\": \"<Gamepad>/rightStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"GamepadScroll\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"498a58dc-c5cf-45a9-b0d5-7b57940c8dac\",\n                    \"path\": \"<Keyboard>/anyKey\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3f97a884-1c8d-4b89-b51c-207f2e40dae9\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8ce542b4-ac02-4417-858b-f8fd42e64051\",\n                    \"path\": \"<Mouse>/rightButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"KeyboardMouse\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fd1dbf38-3f25-468a-aba2-78aa8719da66\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7ebbcecc-7227-4771-a787-24d024bd15e9\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c53d7410-7567-4d18-b11f-d316e861fa8a\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1079ac0f-d32f-4228-bf1c-f5246f8764a5\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8482acdf-c79a-40c9-9fba-8bb5ae4f7c99\",\n                    \"path\": \"<Gamepad>/dpad/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5124f0ed-a969-48d0-aa39-a5101571d29a\",\n                    \"path\": \"<Gamepad>/dpad/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"329fd528-c2fd-4163-a1dd-ff231553d6db\",\n                    \"path\": \"<Gamepad>/dpad/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"CustomAnyButtonAction\",\n                    \"isComposite\": false,\n                    \"isPartO[...string is too long...]");
			this.m_UI = this.asset.FindActionMap("UI", true);
			this.m_UI_Navigate = this.m_UI.FindAction("Navigate", true);
			this.m_UI_Submit = this.m_UI.FindAction("Submit", true);
			this.m_UI_Cancel = this.m_UI.FindAction("Cancel", true);
			this.m_UI_Point = this.m_UI.FindAction("Point", true);
			this.m_UI_Click = this.m_UI.FindAction("Click", true);
			this.m_UI_ScrollWheel = this.m_UI.FindAction("ScrollWheel", true);
			this.m_UI_MiddleClick = this.m_UI.FindAction("MiddleClick", true);
			this.m_UI_RightClick = this.m_UI.FindAction("RightClick", true);
			this.m_UI_StickNavigation = this.m_UI.FindAction("StickNavigation", true);
			this.m_UI_NextSoundtrackAction = this.m_UI.FindAction("NextSoundtrackAction", true);
			this.m_UI_PreviousSoundtrackAction = this.m_UI.FindAction("PreviousSoundtrackAction", true);
			this.m_UI_ToggleSoundtrackAction = this.m_UI.FindAction("ToggleSoundtrackAction", true);
			this.m_UI_ChangeFontSize = this.m_UI.FindAction("ChangeFontSize", true);
			this.m_UI_GamepadScroll = this.m_UI.FindAction("GamepadScroll", true);
			this.m_UI_CustomAnyButtonAction = this.m_UI.FindAction("CustomAnyButtonAction", true);
			this.m_MapActions = this.asset.FindActionMap("MapActions", true);
			this.m_MapActions_Zoom = this.m_MapActions.FindAction("Zoom", true);
			this.m_MapActions_Pan = this.m_MapActions.FindAction("Pan", true);
			this.m_MapActions_ToggleBorders = this.m_MapActions.FindAction("ToggleBorders", true);
			this.m_MapActions_SelectPreviousFactionAction = this.m_MapActions.FindAction("SelectPreviousFactionAction", true);
			this.m_MapActions_SelectNextFactionAction = this.m_MapActions.FindAction("SelectNextFactionAction", true);
			this.m_BookActions = this.asset.FindActionMap("BookActions", true);
			this.m_BookActions_PreviousPage = this.m_BookActions.FindAction("PreviousPage", true);
			this.m_BookActions_NextPage = this.m_BookActions.FindAction("NextPage", true);
			this.m_BookActions_ToggleVoiceOverAction = this.m_BookActions.FindAction("ToggleVoiceOverAction", true);
			this.m_BookActions_ToggleChapterSelection = this.m_BookActions.FindAction("ToggleChapterSelection", true);
			this.m_BookActions_ToggleUIAction = this.m_BookActions.FindAction("ToggleUIAction", true);
			this.m_BookActions_ToggleAutoTurnAction = this.m_BookActions.FindAction("ToggleAutoTurnAction", true);
			this.m_ConceptArtActions = this.asset.FindActionMap("ConceptArtActions", true);
			this.m_ConceptArtActions_NextFaction = this.m_ConceptArtActions.FindAction("NextFaction", true);
			this.m_ConceptArtActions_PreviousFaction = this.m_ConceptArtActions.FindAction("PreviousFaction", true);
			this.m_ConceptArtActions_NextArtwork = this.m_ConceptArtActions.FindAction("NextArtwork", true);
			this.m_ConceptArtActions_PreviousArtwork = this.m_ConceptArtActions.FindAction("PreviousArtwork", true);
			this.m_CreditsActions = this.asset.FindActionMap("CreditsActions", true);
			this.m_CreditsActions_PreviousStoryItemAction = this.m_CreditsActions.FindAction("PreviousStoryItemAction", true);
			this.m_CreditsActions_NextStoryItemAction = this.m_CreditsActions.FindAction("NextStoryItemAction", true);
			this.m_MenuActions = this.asset.FindActionMap("MenuActions", true);
			this.m_MenuActions_OpenOptionsAction = this.m_MenuActions.FindAction("OpenOptionsAction", true);
			this.m_MenuActions_BookRestartAction = this.m_MenuActions.FindAction("BookRestartAction", true);
			this.m_MenuActions_OptionsConfirmAction = this.m_MenuActions.FindAction("OptionsConfirmAction", true);
			this.m_MenuActions_OptionsResetAction = this.m_MenuActions.FindAction("OptionsResetAction", true);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004383 File Offset: 0x00002583
		public void Dispose()
		{
			Object.Destroy(this.asset);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004390 File Offset: 0x00002590
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000439D File Offset: 0x0000259D
		public InputBinding? bindingMask
		{
			get
			{
				return this.asset.bindingMask;
			}
			set
			{
				this.asset.bindingMask = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000043AB File Offset: 0x000025AB
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000043B8 File Offset: 0x000025B8
		public ReadOnlyArray<InputDevice>? devices
		{
			get
			{
				return this.asset.devices;
			}
			set
			{
				this.asset.devices = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000043C6 File Offset: 0x000025C6
		public ReadOnlyArray<InputControlScheme> controlSchemes
		{
			get
			{
				return this.asset.controlSchemes;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000043D3 File Offset: 0x000025D3
		public bool Contains(InputAction action)
		{
			return this.asset.Contains(action);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000043E1 File Offset: 0x000025E1
		public IEnumerator<InputAction> GetEnumerator()
		{
			return this.asset.GetEnumerator();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043EE File Offset: 0x000025EE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000043F6 File Offset: 0x000025F6
		public void Enable()
		{
			this.asset.Enable();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004403 File Offset: 0x00002603
		public void Disable()
		{
			this.asset.Disable();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004410 File Offset: 0x00002610
		public IEnumerable<InputBinding> bindings
		{
			get
			{
				return this.asset.bindings;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000441D File Offset: 0x0000261D
		public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			return this.asset.FindAction(actionNameOrId, throwIfNotFound);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000442C File Offset: 0x0000262C
		public int FindBinding(InputBinding bindingMask, out InputAction action)
		{
			return this.asset.FindBinding(bindingMask, ref action);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000443B File Offset: 0x0000263B
		public PlayerActions.UIActions UI
		{
			get
			{
				return new PlayerActions.UIActions(this);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004443 File Offset: 0x00002643
		public PlayerActions.MapActionsActions MapActions
		{
			get
			{
				return new PlayerActions.MapActionsActions(this);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000444B File Offset: 0x0000264B
		public PlayerActions.BookActionsActions BookActions
		{
			get
			{
				return new PlayerActions.BookActionsActions(this);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004453 File Offset: 0x00002653
		public PlayerActions.ConceptArtActionsActions ConceptArtActions
		{
			get
			{
				return new PlayerActions.ConceptArtActionsActions(this);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000445B File Offset: 0x0000265B
		public PlayerActions.CreditsActionsActions CreditsActions
		{
			get
			{
				return new PlayerActions.CreditsActionsActions(this);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004463 File Offset: 0x00002663
		public PlayerActions.MenuActionsActions MenuActions
		{
			get
			{
				return new PlayerActions.MenuActionsActions(this);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000446C File Offset: 0x0000266C
		public InputControlScheme KeyboardMouseScheme
		{
			get
			{
				if (this.m_KeyboardMouseSchemeIndex == -1)
				{
					this.m_KeyboardMouseSchemeIndex = this.asset.FindControlSchemeIndex("KeyboardMouse");
				}
				return this.asset.controlSchemes[this.m_KeyboardMouseSchemeIndex];
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000044B4 File Offset: 0x000026B4
		public InputControlScheme GamepadScheme
		{
			get
			{
				if (this.m_GamepadSchemeIndex == -1)
				{
					this.m_GamepadSchemeIndex = this.asset.FindControlSchemeIndex("Gamepad");
				}
				return this.asset.controlSchemes[this.m_GamepadSchemeIndex];
			}
		}

		// Token: 0x04000075 RID: 117
		private readonly InputActionMap m_UI;

		// Token: 0x04000076 RID: 118
		private PlayerActions.IUIActions m_UIActionsCallbackInterface;

		// Token: 0x04000077 RID: 119
		private readonly InputAction m_UI_Navigate;

		// Token: 0x04000078 RID: 120
		private readonly InputAction m_UI_Submit;

		// Token: 0x04000079 RID: 121
		private readonly InputAction m_UI_Cancel;

		// Token: 0x0400007A RID: 122
		private readonly InputAction m_UI_Point;

		// Token: 0x0400007B RID: 123
		private readonly InputAction m_UI_Click;

		// Token: 0x0400007C RID: 124
		private readonly InputAction m_UI_ScrollWheel;

		// Token: 0x0400007D RID: 125
		private readonly InputAction m_UI_MiddleClick;

		// Token: 0x0400007E RID: 126
		private readonly InputAction m_UI_RightClick;

		// Token: 0x0400007F RID: 127
		private readonly InputAction m_UI_StickNavigation;

		// Token: 0x04000080 RID: 128
		private readonly InputAction m_UI_NextSoundtrackAction;

		// Token: 0x04000081 RID: 129
		private readonly InputAction m_UI_PreviousSoundtrackAction;

		// Token: 0x04000082 RID: 130
		private readonly InputAction m_UI_ToggleSoundtrackAction;

		// Token: 0x04000083 RID: 131
		private readonly InputAction m_UI_ChangeFontSize;

		// Token: 0x04000084 RID: 132
		private readonly InputAction m_UI_GamepadScroll;

		// Token: 0x04000085 RID: 133
		private readonly InputAction m_UI_CustomAnyButtonAction;

		// Token: 0x04000086 RID: 134
		private readonly InputActionMap m_MapActions;

		// Token: 0x04000087 RID: 135
		private PlayerActions.IMapActionsActions m_MapActionsActionsCallbackInterface;

		// Token: 0x04000088 RID: 136
		private readonly InputAction m_MapActions_Zoom;

		// Token: 0x04000089 RID: 137
		private readonly InputAction m_MapActions_Pan;

		// Token: 0x0400008A RID: 138
		private readonly InputAction m_MapActions_ToggleBorders;

		// Token: 0x0400008B RID: 139
		private readonly InputAction m_MapActions_SelectPreviousFactionAction;

		// Token: 0x0400008C RID: 140
		private readonly InputAction m_MapActions_SelectNextFactionAction;

		// Token: 0x0400008D RID: 141
		private readonly InputActionMap m_BookActions;

		// Token: 0x0400008E RID: 142
		private PlayerActions.IBookActionsActions m_BookActionsActionsCallbackInterface;

		// Token: 0x0400008F RID: 143
		private readonly InputAction m_BookActions_PreviousPage;

		// Token: 0x04000090 RID: 144
		private readonly InputAction m_BookActions_NextPage;

		// Token: 0x04000091 RID: 145
		private readonly InputAction m_BookActions_ToggleVoiceOverAction;

		// Token: 0x04000092 RID: 146
		private readonly InputAction m_BookActions_ToggleChapterSelection;

		// Token: 0x04000093 RID: 147
		private readonly InputAction m_BookActions_ToggleUIAction;

		// Token: 0x04000094 RID: 148
		private readonly InputAction m_BookActions_ToggleAutoTurnAction;

		// Token: 0x04000095 RID: 149
		private readonly InputActionMap m_ConceptArtActions;

		// Token: 0x04000096 RID: 150
		private PlayerActions.IConceptArtActionsActions m_ConceptArtActionsActionsCallbackInterface;

		// Token: 0x04000097 RID: 151
		private readonly InputAction m_ConceptArtActions_NextFaction;

		// Token: 0x04000098 RID: 152
		private readonly InputAction m_ConceptArtActions_PreviousFaction;

		// Token: 0x04000099 RID: 153
		private readonly InputAction m_ConceptArtActions_NextArtwork;

		// Token: 0x0400009A RID: 154
		private readonly InputAction m_ConceptArtActions_PreviousArtwork;

		// Token: 0x0400009B RID: 155
		private readonly InputActionMap m_CreditsActions;

		// Token: 0x0400009C RID: 156
		private PlayerActions.ICreditsActionsActions m_CreditsActionsActionsCallbackInterface;

		// Token: 0x0400009D RID: 157
		private readonly InputAction m_CreditsActions_PreviousStoryItemAction;

		// Token: 0x0400009E RID: 158
		private readonly InputAction m_CreditsActions_NextStoryItemAction;

		// Token: 0x0400009F RID: 159
		private readonly InputActionMap m_MenuActions;

		// Token: 0x040000A0 RID: 160
		private PlayerActions.IMenuActionsActions m_MenuActionsActionsCallbackInterface;

		// Token: 0x040000A1 RID: 161
		private readonly InputAction m_MenuActions_OpenOptionsAction;

		// Token: 0x040000A2 RID: 162
		private readonly InputAction m_MenuActions_BookRestartAction;

		// Token: 0x040000A3 RID: 163
		private readonly InputAction m_MenuActions_OptionsConfirmAction;

		// Token: 0x040000A4 RID: 164
		private readonly InputAction m_MenuActions_OptionsResetAction;

		// Token: 0x040000A5 RID: 165
		private int m_KeyboardMouseSchemeIndex = -1;

		// Token: 0x040000A6 RID: 166
		private int m_GamepadSchemeIndex = -1;

		// Token: 0x02000084 RID: 132
		public struct UIActions
		{
			// Token: 0x0600040F RID: 1039 RVA: 0x0000F7A5 File Offset: 0x0000D9A5
			public UIActions(PlayerActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000F7AE File Offset: 0x0000D9AE
			public InputAction Navigate
			{
				get
				{
					return this.m_Wrapper.m_UI_Navigate;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000F7BB File Offset: 0x0000D9BB
			public InputAction Submit
			{
				get
				{
					return this.m_Wrapper.m_UI_Submit;
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
			public InputAction Cancel
			{
				get
				{
					return this.m_Wrapper.m_UI_Cancel;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000F7D5 File Offset: 0x0000D9D5
			public InputAction Point
			{
				get
				{
					return this.m_Wrapper.m_UI_Point;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000F7E2 File Offset: 0x0000D9E2
			public InputAction Click
			{
				get
				{
					return this.m_Wrapper.m_UI_Click;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000F7EF File Offset: 0x0000D9EF
			public InputAction ScrollWheel
			{
				get
				{
					return this.m_Wrapper.m_UI_ScrollWheel;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000F7FC File Offset: 0x0000D9FC
			public InputAction MiddleClick
			{
				get
				{
					return this.m_Wrapper.m_UI_MiddleClick;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000F809 File Offset: 0x0000DA09
			public InputAction RightClick
			{
				get
				{
					return this.m_Wrapper.m_UI_RightClick;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000F816 File Offset: 0x0000DA16
			public InputAction StickNavigation
			{
				get
				{
					return this.m_Wrapper.m_UI_StickNavigation;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000F823 File Offset: 0x0000DA23
			public InputAction NextSoundtrackAction
			{
				get
				{
					return this.m_Wrapper.m_UI_NextSoundtrackAction;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000F830 File Offset: 0x0000DA30
			public InputAction PreviousSoundtrackAction
			{
				get
				{
					return this.m_Wrapper.m_UI_PreviousSoundtrackAction;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000F83D File Offset: 0x0000DA3D
			public InputAction ToggleSoundtrackAction
			{
				get
				{
					return this.m_Wrapper.m_UI_ToggleSoundtrackAction;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000F84A File Offset: 0x0000DA4A
			public InputAction ChangeFontSize
			{
				get
				{
					return this.m_Wrapper.m_UI_ChangeFontSize;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000F857 File Offset: 0x0000DA57
			public InputAction GamepadScroll
			{
				get
				{
					return this.m_Wrapper.m_UI_GamepadScroll;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000F864 File Offset: 0x0000DA64
			public InputAction CustomAnyButtonAction
			{
				get
				{
					return this.m_Wrapper.m_UI_CustomAnyButtonAction;
				}
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000F871 File Offset: 0x0000DA71
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_UI;
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x0000F87E File Offset: 0x0000DA7E
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000F88B File Offset: 0x0000DA8B
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000F898 File Offset: 0x0000DA98
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0000F8A5 File Offset: 0x0000DAA5
			public static implicit operator InputActionMap(PlayerActions.UIActions set)
			{
				return set.Get();
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
			public void SetCallbacks(PlayerActions.IUIActions instance)
			{
				if (this.m_Wrapper.m_UIActionsCallbackInterface != null)
				{
					this.Navigate.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate);
					this.Navigate.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate);
					this.Navigate.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate);
					this.Submit.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit);
					this.Submit.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit);
					this.Submit.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit);
					this.Cancel.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel);
					this.Cancel.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel);
					this.Cancel.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel);
					this.Point.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint);
					this.Point.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint);
					this.Point.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint);
					this.Click.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnClick);
					this.Click.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnClick);
					this.Click.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnClick);
					this.ScrollWheel.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel);
					this.ScrollWheel.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel);
					this.ScrollWheel.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel);
					this.MiddleClick.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick);
					this.MiddleClick.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick);
					this.MiddleClick.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick);
					this.RightClick.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick);
					this.RightClick.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick);
					this.RightClick.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick);
					this.StickNavigation.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnStickNavigation);
					this.StickNavigation.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnStickNavigation);
					this.StickNavigation.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnStickNavigation);
					this.NextSoundtrackAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnNextSoundtrackAction);
					this.NextSoundtrackAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnNextSoundtrackAction);
					this.NextSoundtrackAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnNextSoundtrackAction);
					this.PreviousSoundtrackAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnPreviousSoundtrackAction);
					this.PreviousSoundtrackAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnPreviousSoundtrackAction);
					this.PreviousSoundtrackAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnPreviousSoundtrackAction);
					this.ToggleSoundtrackAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnToggleSoundtrackAction);
					this.ToggleSoundtrackAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnToggleSoundtrackAction);
					this.ToggleSoundtrackAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnToggleSoundtrackAction);
					this.ChangeFontSize.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnChangeFontSize);
					this.ChangeFontSize.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnChangeFontSize);
					this.ChangeFontSize.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnChangeFontSize);
					this.GamepadScroll.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnGamepadScroll);
					this.GamepadScroll.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnGamepadScroll);
					this.GamepadScroll.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnGamepadScroll);
					this.CustomAnyButtonAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnCustomAnyButtonAction);
					this.CustomAnyButtonAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnCustomAnyButtonAction);
					this.CustomAnyButtonAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_UIActionsCallbackInterface.OnCustomAnyButtonAction);
				}
				this.m_Wrapper.m_UIActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.Navigate.started += new Action<InputAction.CallbackContext>(instance.OnNavigate);
					this.Navigate.performed += new Action<InputAction.CallbackContext>(instance.OnNavigate);
					this.Navigate.canceled += new Action<InputAction.CallbackContext>(instance.OnNavigate);
					this.Submit.started += new Action<InputAction.CallbackContext>(instance.OnSubmit);
					this.Submit.performed += new Action<InputAction.CallbackContext>(instance.OnSubmit);
					this.Submit.canceled += new Action<InputAction.CallbackContext>(instance.OnSubmit);
					this.Cancel.started += new Action<InputAction.CallbackContext>(instance.OnCancel);
					this.Cancel.performed += new Action<InputAction.CallbackContext>(instance.OnCancel);
					this.Cancel.canceled += new Action<InputAction.CallbackContext>(instance.OnCancel);
					this.Point.started += new Action<InputAction.CallbackContext>(instance.OnPoint);
					this.Point.performed += new Action<InputAction.CallbackContext>(instance.OnPoint);
					this.Point.canceled += new Action<InputAction.CallbackContext>(instance.OnPoint);
					this.Click.started += new Action<InputAction.CallbackContext>(instance.OnClick);
					this.Click.performed += new Action<InputAction.CallbackContext>(instance.OnClick);
					this.Click.canceled += new Action<InputAction.CallbackContext>(instance.OnClick);
					this.ScrollWheel.started += new Action<InputAction.CallbackContext>(instance.OnScrollWheel);
					this.ScrollWheel.performed += new Action<InputAction.CallbackContext>(instance.OnScrollWheel);
					this.ScrollWheel.canceled += new Action<InputAction.CallbackContext>(instance.OnScrollWheel);
					this.MiddleClick.started += new Action<InputAction.CallbackContext>(instance.OnMiddleClick);
					this.MiddleClick.performed += new Action<InputAction.CallbackContext>(instance.OnMiddleClick);
					this.MiddleClick.canceled += new Action<InputAction.CallbackContext>(instance.OnMiddleClick);
					this.RightClick.started += new Action<InputAction.CallbackContext>(instance.OnRightClick);
					this.RightClick.performed += new Action<InputAction.CallbackContext>(instance.OnRightClick);
					this.RightClick.canceled += new Action<InputAction.CallbackContext>(instance.OnRightClick);
					this.StickNavigation.started += new Action<InputAction.CallbackContext>(instance.OnStickNavigation);
					this.StickNavigation.performed += new Action<InputAction.CallbackContext>(instance.OnStickNavigation);
					this.StickNavigation.canceled += new Action<InputAction.CallbackContext>(instance.OnStickNavigation);
					this.NextSoundtrackAction.started += new Action<InputAction.CallbackContext>(instance.OnNextSoundtrackAction);
					this.NextSoundtrackAction.performed += new Action<InputAction.CallbackContext>(instance.OnNextSoundtrackAction);
					this.NextSoundtrackAction.canceled += new Action<InputAction.CallbackContext>(instance.OnNextSoundtrackAction);
					this.PreviousSoundtrackAction.started += new Action<InputAction.CallbackContext>(instance.OnPreviousSoundtrackAction);
					this.PreviousSoundtrackAction.performed += new Action<InputAction.CallbackContext>(instance.OnPreviousSoundtrackAction);
					this.PreviousSoundtrackAction.canceled += new Action<InputAction.CallbackContext>(instance.OnPreviousSoundtrackAction);
					this.ToggleSoundtrackAction.started += new Action<InputAction.CallbackContext>(instance.OnToggleSoundtrackAction);
					this.ToggleSoundtrackAction.performed += new Action<InputAction.CallbackContext>(instance.OnToggleSoundtrackAction);
					this.ToggleSoundtrackAction.canceled += new Action<InputAction.CallbackContext>(instance.OnToggleSoundtrackAction);
					this.ChangeFontSize.started += new Action<InputAction.CallbackContext>(instance.OnChangeFontSize);
					this.ChangeFontSize.performed += new Action<InputAction.CallbackContext>(instance.OnChangeFontSize);
					this.ChangeFontSize.canceled += new Action<InputAction.CallbackContext>(instance.OnChangeFontSize);
					this.GamepadScroll.started += new Action<InputAction.CallbackContext>(instance.OnGamepadScroll);
					this.GamepadScroll.performed += new Action<InputAction.CallbackContext>(instance.OnGamepadScroll);
					this.GamepadScroll.canceled += new Action<InputAction.CallbackContext>(instance.OnGamepadScroll);
					this.CustomAnyButtonAction.started += new Action<InputAction.CallbackContext>(instance.OnCustomAnyButtonAction);
					this.CustomAnyButtonAction.performed += new Action<InputAction.CallbackContext>(instance.OnCustomAnyButtonAction);
					this.CustomAnyButtonAction.canceled += new Action<InputAction.CallbackContext>(instance.OnCustomAnyButtonAction);
				}
			}

			// Token: 0x0400036E RID: 878
			private PlayerActions m_Wrapper;
		}

		// Token: 0x02000085 RID: 133
		public struct MapActionsActions
		{
			// Token: 0x06000425 RID: 1061 RVA: 0x00010311 File Offset: 0x0000E511
			public MapActionsActions(PlayerActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001031A File Offset: 0x0000E51A
			public InputAction Zoom
			{
				get
				{
					return this.m_Wrapper.m_MapActions_Zoom;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x00010327 File Offset: 0x0000E527
			public InputAction Pan
			{
				get
				{
					return this.m_Wrapper.m_MapActions_Pan;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000428 RID: 1064 RVA: 0x00010334 File Offset: 0x0000E534
			public InputAction ToggleBorders
			{
				get
				{
					return this.m_Wrapper.m_MapActions_ToggleBorders;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000429 RID: 1065 RVA: 0x00010341 File Offset: 0x0000E541
			public InputAction SelectPreviousFactionAction
			{
				get
				{
					return this.m_Wrapper.m_MapActions_SelectPreviousFactionAction;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001034E File Offset: 0x0000E54E
			public InputAction SelectNextFactionAction
			{
				get
				{
					return this.m_Wrapper.m_MapActions_SelectNextFactionAction;
				}
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x0001035B File Offset: 0x0000E55B
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_MapActions;
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x00010368 File Offset: 0x0000E568
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00010375 File Offset: 0x0000E575
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x00010382 File Offset: 0x0000E582
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0001038F File Offset: 0x0000E58F
			public static implicit operator InputActionMap(PlayerActions.MapActionsActions set)
			{
				return set.Get();
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x00010398 File Offset: 0x0000E598
			public void SetCallbacks(PlayerActions.IMapActionsActions instance)
			{
				if (this.m_Wrapper.m_MapActionsActionsCallbackInterface != null)
				{
					this.Zoom.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnZoom);
					this.Zoom.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnZoom);
					this.Zoom.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnZoom);
					this.Pan.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnPan);
					this.Pan.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnPan);
					this.Pan.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnPan);
					this.ToggleBorders.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnToggleBorders);
					this.ToggleBorders.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnToggleBorders);
					this.ToggleBorders.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnToggleBorders);
					this.SelectPreviousFactionAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnSelectPreviousFactionAction);
					this.SelectPreviousFactionAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnSelectPreviousFactionAction);
					this.SelectPreviousFactionAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnSelectPreviousFactionAction);
					this.SelectNextFactionAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnSelectNextFactionAction);
					this.SelectNextFactionAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnSelectNextFactionAction);
					this.SelectNextFactionAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MapActionsActionsCallbackInterface.OnSelectNextFactionAction);
				}
				this.m_Wrapper.m_MapActionsActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.Zoom.started += new Action<InputAction.CallbackContext>(instance.OnZoom);
					this.Zoom.performed += new Action<InputAction.CallbackContext>(instance.OnZoom);
					this.Zoom.canceled += new Action<InputAction.CallbackContext>(instance.OnZoom);
					this.Pan.started += new Action<InputAction.CallbackContext>(instance.OnPan);
					this.Pan.performed += new Action<InputAction.CallbackContext>(instance.OnPan);
					this.Pan.canceled += new Action<InputAction.CallbackContext>(instance.OnPan);
					this.ToggleBorders.started += new Action<InputAction.CallbackContext>(instance.OnToggleBorders);
					this.ToggleBorders.performed += new Action<InputAction.CallbackContext>(instance.OnToggleBorders);
					this.ToggleBorders.canceled += new Action<InputAction.CallbackContext>(instance.OnToggleBorders);
					this.SelectPreviousFactionAction.started += new Action<InputAction.CallbackContext>(instance.OnSelectPreviousFactionAction);
					this.SelectPreviousFactionAction.performed += new Action<InputAction.CallbackContext>(instance.OnSelectPreviousFactionAction);
					this.SelectPreviousFactionAction.canceled += new Action<InputAction.CallbackContext>(instance.OnSelectPreviousFactionAction);
					this.SelectNextFactionAction.started += new Action<InputAction.CallbackContext>(instance.OnSelectNextFactionAction);
					this.SelectNextFactionAction.performed += new Action<InputAction.CallbackContext>(instance.OnSelectNextFactionAction);
					this.SelectNextFactionAction.canceled += new Action<InputAction.CallbackContext>(instance.OnSelectNextFactionAction);
				}
			}

			// Token: 0x0400036F RID: 879
			private PlayerActions m_Wrapper;
		}

		// Token: 0x02000086 RID: 134
		public struct BookActionsActions
		{
			// Token: 0x06000431 RID: 1073 RVA: 0x0001072D File Offset: 0x0000E92D
			public BookActionsActions(PlayerActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x00010736 File Offset: 0x0000E936
			public InputAction PreviousPage
			{
				get
				{
					return this.m_Wrapper.m_BookActions_PreviousPage;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x00010743 File Offset: 0x0000E943
			public InputAction NextPage
			{
				get
				{
					return this.m_Wrapper.m_BookActions_NextPage;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x00010750 File Offset: 0x0000E950
			public InputAction ToggleVoiceOverAction
			{
				get
				{
					return this.m_Wrapper.m_BookActions_ToggleVoiceOverAction;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001075D File Offset: 0x0000E95D
			public InputAction ToggleChapterSelection
			{
				get
				{
					return this.m_Wrapper.m_BookActions_ToggleChapterSelection;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001076A File Offset: 0x0000E96A
			public InputAction ToggleUIAction
			{
				get
				{
					return this.m_Wrapper.m_BookActions_ToggleUIAction;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x00010777 File Offset: 0x0000E977
			public InputAction ToggleAutoTurnAction
			{
				get
				{
					return this.m_Wrapper.m_BookActions_ToggleAutoTurnAction;
				}
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x00010784 File Offset: 0x0000E984
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_BookActions;
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x00010791 File Offset: 0x0000E991
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x0001079E File Offset: 0x0000E99E
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600043B RID: 1083 RVA: 0x000107AB File Offset: 0x0000E9AB
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x000107B8 File Offset: 0x0000E9B8
			public static implicit operator InputActionMap(PlayerActions.BookActionsActions set)
			{
				return set.Get();
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x000107C4 File Offset: 0x0000E9C4
			public void SetCallbacks(PlayerActions.IBookActionsActions instance)
			{
				if (this.m_Wrapper.m_BookActionsActionsCallbackInterface != null)
				{
					this.PreviousPage.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnPreviousPage);
					this.PreviousPage.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnPreviousPage);
					this.PreviousPage.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnPreviousPage);
					this.NextPage.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnNextPage);
					this.NextPage.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnNextPage);
					this.NextPage.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnNextPage);
					this.ToggleVoiceOverAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleVoiceOverAction);
					this.ToggleVoiceOverAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleVoiceOverAction);
					this.ToggleVoiceOverAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleVoiceOverAction);
					this.ToggleChapterSelection.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleChapterSelection);
					this.ToggleChapterSelection.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleChapterSelection);
					this.ToggleChapterSelection.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleChapterSelection);
					this.ToggleUIAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleUIAction);
					this.ToggleUIAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleUIAction);
					this.ToggleUIAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleUIAction);
					this.ToggleAutoTurnAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleAutoTurnAction);
					this.ToggleAutoTurnAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleAutoTurnAction);
					this.ToggleAutoTurnAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_BookActionsActionsCallbackInterface.OnToggleAutoTurnAction);
				}
				this.m_Wrapper.m_BookActionsActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.PreviousPage.started += new Action<InputAction.CallbackContext>(instance.OnPreviousPage);
					this.PreviousPage.performed += new Action<InputAction.CallbackContext>(instance.OnPreviousPage);
					this.PreviousPage.canceled += new Action<InputAction.CallbackContext>(instance.OnPreviousPage);
					this.NextPage.started += new Action<InputAction.CallbackContext>(instance.OnNextPage);
					this.NextPage.performed += new Action<InputAction.CallbackContext>(instance.OnNextPage);
					this.NextPage.canceled += new Action<InputAction.CallbackContext>(instance.OnNextPage);
					this.ToggleVoiceOverAction.started += new Action<InputAction.CallbackContext>(instance.OnToggleVoiceOverAction);
					this.ToggleVoiceOverAction.performed += new Action<InputAction.CallbackContext>(instance.OnToggleVoiceOverAction);
					this.ToggleVoiceOverAction.canceled += new Action<InputAction.CallbackContext>(instance.OnToggleVoiceOverAction);
					this.ToggleChapterSelection.started += new Action<InputAction.CallbackContext>(instance.OnToggleChapterSelection);
					this.ToggleChapterSelection.performed += new Action<InputAction.CallbackContext>(instance.OnToggleChapterSelection);
					this.ToggleChapterSelection.canceled += new Action<InputAction.CallbackContext>(instance.OnToggleChapterSelection);
					this.ToggleUIAction.started += new Action<InputAction.CallbackContext>(instance.OnToggleUIAction);
					this.ToggleUIAction.performed += new Action<InputAction.CallbackContext>(instance.OnToggleUIAction);
					this.ToggleUIAction.canceled += new Action<InputAction.CallbackContext>(instance.OnToggleUIAction);
					this.ToggleAutoTurnAction.started += new Action<InputAction.CallbackContext>(instance.OnToggleAutoTurnAction);
					this.ToggleAutoTurnAction.performed += new Action<InputAction.CallbackContext>(instance.OnToggleAutoTurnAction);
					this.ToggleAutoTurnAction.canceled += new Action<InputAction.CallbackContext>(instance.OnToggleAutoTurnAction);
				}
			}

			// Token: 0x04000370 RID: 880
			private PlayerActions m_Wrapper;
		}

		// Token: 0x02000087 RID: 135
		public struct ConceptArtActionsActions
		{
			// Token: 0x0600043E RID: 1086 RVA: 0x00010C07 File Offset: 0x0000EE07
			public ConceptArtActionsActions(PlayerActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x00010C10 File Offset: 0x0000EE10
			public InputAction NextFaction
			{
				get
				{
					return this.m_Wrapper.m_ConceptArtActions_NextFaction;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x00010C1D File Offset: 0x0000EE1D
			public InputAction PreviousFaction
			{
				get
				{
					return this.m_Wrapper.m_ConceptArtActions_PreviousFaction;
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000441 RID: 1089 RVA: 0x00010C2A File Offset: 0x0000EE2A
			public InputAction NextArtwork
			{
				get
				{
					return this.m_Wrapper.m_ConceptArtActions_NextArtwork;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x00010C37 File Offset: 0x0000EE37
			public InputAction PreviousArtwork
			{
				get
				{
					return this.m_Wrapper.m_ConceptArtActions_PreviousArtwork;
				}
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x00010C44 File Offset: 0x0000EE44
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_ConceptArtActions;
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x00010C51 File Offset: 0x0000EE51
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x00010C5E File Offset: 0x0000EE5E
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x00010C6B File Offset: 0x0000EE6B
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x00010C78 File Offset: 0x0000EE78
			public static implicit operator InputActionMap(PlayerActions.ConceptArtActionsActions set)
			{
				return set.Get();
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x00010C84 File Offset: 0x0000EE84
			public void SetCallbacks(PlayerActions.IConceptArtActionsActions instance)
			{
				if (this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface != null)
				{
					this.NextFaction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnNextFaction);
					this.NextFaction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnNextFaction);
					this.NextFaction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnNextFaction);
					this.PreviousFaction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnPreviousFaction);
					this.PreviousFaction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnPreviousFaction);
					this.PreviousFaction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnPreviousFaction);
					this.NextArtwork.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnNextArtwork);
					this.NextArtwork.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnNextArtwork);
					this.NextArtwork.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnNextArtwork);
					this.PreviousArtwork.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnPreviousArtwork);
					this.PreviousArtwork.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnPreviousArtwork);
					this.PreviousArtwork.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface.OnPreviousArtwork);
				}
				this.m_Wrapper.m_ConceptArtActionsActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.NextFaction.started += new Action<InputAction.CallbackContext>(instance.OnNextFaction);
					this.NextFaction.performed += new Action<InputAction.CallbackContext>(instance.OnNextFaction);
					this.NextFaction.canceled += new Action<InputAction.CallbackContext>(instance.OnNextFaction);
					this.PreviousFaction.started += new Action<InputAction.CallbackContext>(instance.OnPreviousFaction);
					this.PreviousFaction.performed += new Action<InputAction.CallbackContext>(instance.OnPreviousFaction);
					this.PreviousFaction.canceled += new Action<InputAction.CallbackContext>(instance.OnPreviousFaction);
					this.NextArtwork.started += new Action<InputAction.CallbackContext>(instance.OnNextArtwork);
					this.NextArtwork.performed += new Action<InputAction.CallbackContext>(instance.OnNextArtwork);
					this.NextArtwork.canceled += new Action<InputAction.CallbackContext>(instance.OnNextArtwork);
					this.PreviousArtwork.started += new Action<InputAction.CallbackContext>(instance.OnPreviousArtwork);
					this.PreviousArtwork.performed += new Action<InputAction.CallbackContext>(instance.OnPreviousArtwork);
					this.PreviousArtwork.canceled += new Action<InputAction.CallbackContext>(instance.OnPreviousArtwork);
				}
			}

			// Token: 0x04000371 RID: 881
			private PlayerActions m_Wrapper;
		}

		// Token: 0x02000088 RID: 136
		public struct CreditsActionsActions
		{
			// Token: 0x06000449 RID: 1097 RVA: 0x00010F6B File Offset: 0x0000F16B
			public CreditsActionsActions(PlayerActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x00010F74 File Offset: 0x0000F174
			public InputAction PreviousStoryItemAction
			{
				get
				{
					return this.m_Wrapper.m_CreditsActions_PreviousStoryItemAction;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x00010F81 File Offset: 0x0000F181
			public InputAction NextStoryItemAction
			{
				get
				{
					return this.m_Wrapper.m_CreditsActions_NextStoryItemAction;
				}
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x00010F8E File Offset: 0x0000F18E
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_CreditsActions;
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00010F9B File Offset: 0x0000F19B
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x00010FA8 File Offset: 0x0000F1A8
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x00010FB5 File Offset: 0x0000F1B5
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x00010FC2 File Offset: 0x0000F1C2
			public static implicit operator InputActionMap(PlayerActions.CreditsActionsActions set)
			{
				return set.Get();
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x00010FCC File Offset: 0x0000F1CC
			public void SetCallbacks(PlayerActions.ICreditsActionsActions instance)
			{
				if (this.m_Wrapper.m_CreditsActionsActionsCallbackInterface != null)
				{
					this.PreviousStoryItemAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_CreditsActionsActionsCallbackInterface.OnPreviousStoryItemAction);
					this.PreviousStoryItemAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_CreditsActionsActionsCallbackInterface.OnPreviousStoryItemAction);
					this.PreviousStoryItemAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_CreditsActionsActionsCallbackInterface.OnPreviousStoryItemAction);
					this.NextStoryItemAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_CreditsActionsActionsCallbackInterface.OnNextStoryItemAction);
					this.NextStoryItemAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_CreditsActionsActionsCallbackInterface.OnNextStoryItemAction);
					this.NextStoryItemAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_CreditsActionsActionsCallbackInterface.OnNextStoryItemAction);
				}
				this.m_Wrapper.m_CreditsActionsActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.PreviousStoryItemAction.started += new Action<InputAction.CallbackContext>(instance.OnPreviousStoryItemAction);
					this.PreviousStoryItemAction.performed += new Action<InputAction.CallbackContext>(instance.OnPreviousStoryItemAction);
					this.PreviousStoryItemAction.canceled += new Action<InputAction.CallbackContext>(instance.OnPreviousStoryItemAction);
					this.NextStoryItemAction.started += new Action<InputAction.CallbackContext>(instance.OnNextStoryItemAction);
					this.NextStoryItemAction.performed += new Action<InputAction.CallbackContext>(instance.OnNextStoryItemAction);
					this.NextStoryItemAction.canceled += new Action<InputAction.CallbackContext>(instance.OnNextStoryItemAction);
				}
			}

			// Token: 0x04000372 RID: 882
			private PlayerActions m_Wrapper;
		}

		// Token: 0x02000089 RID: 137
		public struct MenuActionsActions
		{
			// Token: 0x06000452 RID: 1106 RVA: 0x00011157 File Offset: 0x0000F357
			public MenuActionsActions(PlayerActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000453 RID: 1107 RVA: 0x00011160 File Offset: 0x0000F360
			public InputAction OpenOptionsAction
			{
				get
				{
					return this.m_Wrapper.m_MenuActions_OpenOptionsAction;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001116D File Offset: 0x0000F36D
			public InputAction BookRestartAction
			{
				get
				{
					return this.m_Wrapper.m_MenuActions_BookRestartAction;
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001117A File Offset: 0x0000F37A
			public InputAction OptionsConfirmAction
			{
				get
				{
					return this.m_Wrapper.m_MenuActions_OptionsConfirmAction;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000456 RID: 1110 RVA: 0x00011187 File Offset: 0x0000F387
			public InputAction OptionsResetAction
			{
				get
				{
					return this.m_Wrapper.m_MenuActions_OptionsResetAction;
				}
			}

			// Token: 0x06000457 RID: 1111 RVA: 0x00011194 File Offset: 0x0000F394
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_MenuActions;
			}

			// Token: 0x06000458 RID: 1112 RVA: 0x000111A1 File Offset: 0x0000F3A1
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x06000459 RID: 1113 RVA: 0x000111AE File Offset: 0x0000F3AE
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x0600045A RID: 1114 RVA: 0x000111BB File Offset: 0x0000F3BB
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x000111C8 File Offset: 0x0000F3C8
			public static implicit operator InputActionMap(PlayerActions.MenuActionsActions set)
			{
				return set.Get();
			}

			// Token: 0x0600045C RID: 1116 RVA: 0x000111D4 File Offset: 0x0000F3D4
			public void SetCallbacks(PlayerActions.IMenuActionsActions instance)
			{
				if (this.m_Wrapper.m_MenuActionsActionsCallbackInterface != null)
				{
					this.OpenOptionsAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOpenOptionsAction);
					this.OpenOptionsAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOpenOptionsAction);
					this.OpenOptionsAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOpenOptionsAction);
					this.BookRestartAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnBookRestartAction);
					this.BookRestartAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnBookRestartAction);
					this.BookRestartAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnBookRestartAction);
					this.OptionsConfirmAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOptionsConfirmAction);
					this.OptionsConfirmAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOptionsConfirmAction);
					this.OptionsConfirmAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOptionsConfirmAction);
					this.OptionsResetAction.started -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOptionsResetAction);
					this.OptionsResetAction.performed -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOptionsResetAction);
					this.OptionsResetAction.canceled -= new Action<InputAction.CallbackContext>(this.m_Wrapper.m_MenuActionsActionsCallbackInterface.OnOptionsResetAction);
				}
				this.m_Wrapper.m_MenuActionsActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.OpenOptionsAction.started += new Action<InputAction.CallbackContext>(instance.OnOpenOptionsAction);
					this.OpenOptionsAction.performed += new Action<InputAction.CallbackContext>(instance.OnOpenOptionsAction);
					this.OpenOptionsAction.canceled += new Action<InputAction.CallbackContext>(instance.OnOpenOptionsAction);
					this.BookRestartAction.started += new Action<InputAction.CallbackContext>(instance.OnBookRestartAction);
					this.BookRestartAction.performed += new Action<InputAction.CallbackContext>(instance.OnBookRestartAction);
					this.BookRestartAction.canceled += new Action<InputAction.CallbackContext>(instance.OnBookRestartAction);
					this.OptionsConfirmAction.started += new Action<InputAction.CallbackContext>(instance.OnOptionsConfirmAction);
					this.OptionsConfirmAction.performed += new Action<InputAction.CallbackContext>(instance.OnOptionsConfirmAction);
					this.OptionsConfirmAction.canceled += new Action<InputAction.CallbackContext>(instance.OnOptionsConfirmAction);
					this.OptionsResetAction.started += new Action<InputAction.CallbackContext>(instance.OnOptionsResetAction);
					this.OptionsResetAction.performed += new Action<InputAction.CallbackContext>(instance.OnOptionsResetAction);
					this.OptionsResetAction.canceled += new Action<InputAction.CallbackContext>(instance.OnOptionsResetAction);
				}
			}

			// Token: 0x04000373 RID: 883
			private PlayerActions m_Wrapper;
		}

		// Token: 0x0200008A RID: 138
		public interface IUIActions
		{
			// Token: 0x0600045D RID: 1117
			void OnNavigate(InputAction.CallbackContext context);

			// Token: 0x0600045E RID: 1118
			void OnSubmit(InputAction.CallbackContext context);

			// Token: 0x0600045F RID: 1119
			void OnCancel(InputAction.CallbackContext context);

			// Token: 0x06000460 RID: 1120
			void OnPoint(InputAction.CallbackContext context);

			// Token: 0x06000461 RID: 1121
			void OnClick(InputAction.CallbackContext context);

			// Token: 0x06000462 RID: 1122
			void OnScrollWheel(InputAction.CallbackContext context);

			// Token: 0x06000463 RID: 1123
			void OnMiddleClick(InputAction.CallbackContext context);

			// Token: 0x06000464 RID: 1124
			void OnRightClick(InputAction.CallbackContext context);

			// Token: 0x06000465 RID: 1125
			void OnStickNavigation(InputAction.CallbackContext context);

			// Token: 0x06000466 RID: 1126
			void OnNextSoundtrackAction(InputAction.CallbackContext context);

			// Token: 0x06000467 RID: 1127
			void OnPreviousSoundtrackAction(InputAction.CallbackContext context);

			// Token: 0x06000468 RID: 1128
			void OnToggleSoundtrackAction(InputAction.CallbackContext context);

			// Token: 0x06000469 RID: 1129
			void OnChangeFontSize(InputAction.CallbackContext context);

			// Token: 0x0600046A RID: 1130
			void OnGamepadScroll(InputAction.CallbackContext context);

			// Token: 0x0600046B RID: 1131
			void OnCustomAnyButtonAction(InputAction.CallbackContext context);
		}

		// Token: 0x0200008B RID: 139
		public interface IMapActionsActions
		{
			// Token: 0x0600046C RID: 1132
			void OnZoom(InputAction.CallbackContext context);

			// Token: 0x0600046D RID: 1133
			void OnPan(InputAction.CallbackContext context);

			// Token: 0x0600046E RID: 1134
			void OnToggleBorders(InputAction.CallbackContext context);

			// Token: 0x0600046F RID: 1135
			void OnSelectPreviousFactionAction(InputAction.CallbackContext context);

			// Token: 0x06000470 RID: 1136
			void OnSelectNextFactionAction(InputAction.CallbackContext context);
		}

		// Token: 0x0200008C RID: 140
		public interface IBookActionsActions
		{
			// Token: 0x06000471 RID: 1137
			void OnPreviousPage(InputAction.CallbackContext context);

			// Token: 0x06000472 RID: 1138
			void OnNextPage(InputAction.CallbackContext context);

			// Token: 0x06000473 RID: 1139
			void OnToggleVoiceOverAction(InputAction.CallbackContext context);

			// Token: 0x06000474 RID: 1140
			void OnToggleChapterSelection(InputAction.CallbackContext context);

			// Token: 0x06000475 RID: 1141
			void OnToggleUIAction(InputAction.CallbackContext context);

			// Token: 0x06000476 RID: 1142
			void OnToggleAutoTurnAction(InputAction.CallbackContext context);
		}

		// Token: 0x0200008D RID: 141
		public interface IConceptArtActionsActions
		{
			// Token: 0x06000477 RID: 1143
			void OnNextFaction(InputAction.CallbackContext context);

			// Token: 0x06000478 RID: 1144
			void OnPreviousFaction(InputAction.CallbackContext context);

			// Token: 0x06000479 RID: 1145
			void OnNextArtwork(InputAction.CallbackContext context);

			// Token: 0x0600047A RID: 1146
			void OnPreviousArtwork(InputAction.CallbackContext context);
		}

		// Token: 0x0200008E RID: 142
		public interface ICreditsActionsActions
		{
			// Token: 0x0600047B RID: 1147
			void OnPreviousStoryItemAction(InputAction.CallbackContext context);

			// Token: 0x0600047C RID: 1148
			void OnNextStoryItemAction(InputAction.CallbackContext context);
		}

		// Token: 0x0200008F RID: 143
		public interface IMenuActionsActions
		{
			// Token: 0x0600047D RID: 1149
			void OnOpenOptionsAction(InputAction.CallbackContext context);

			// Token: 0x0600047E RID: 1150
			void OnBookRestartAction(InputAction.CallbackContext context);

			// Token: 0x0600047F RID: 1151
			void OnOptionsConfirmAction(InputAction.CallbackContext context);

			// Token: 0x06000480 RID: 1152
			void OnOptionsResetAction(InputAction.CallbackContext context);
		}
	}
}
