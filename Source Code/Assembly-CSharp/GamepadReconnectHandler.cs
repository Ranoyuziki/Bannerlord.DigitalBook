using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200000E RID: 14
	public class GamepadReconnectHandler : MonoBehaviour
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002D2C File Offset: 0x00000F2C
		private void Update()
		{
			bool flag = Gamepad.current != null;
			if (this._isGamepadConnected && !flag)
			{
				InputSystem.FlushDisconnectedDevices();
			}
			this._isGamepadConnected = flag;
		}

		// Token: 0x04000041 RID: 65
		private bool _isGamepadConnected;
	}
}
