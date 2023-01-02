using System;
using System.Collections.Generic;
using TaleWorlds.CompanionBook.ResourceSystem.SoundtrackResources;
using UnityEngine;

namespace TaleWorlds.CompanionBook.Soundtracks
{
	// Token: 0x02000024 RID: 36
	public class Soundtrack : ISoundtrack
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000057D4 File Offset: 0x000039D4
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000057DC File Offset: 0x000039DC
		public bool IsPlaying
		{
			get
			{
				return this._isPlaying;
			}
			set
			{
				if (value != this._isPlaying)
				{
					this._isPlaying = value;
					Action<bool> onIsPlayingChanged = this.OnIsPlayingChanged;
					if (onIsPlayingChanged == null)
					{
						return;
					}
					onIsPlayingChanged.Invoke(value);
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000057FF File Offset: 0x000039FF
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00005807 File Offset: 0x00003A07
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
					Action<bool> onIsSelectedChanged = this.OnIsSelectedChanged;
					if (onIsSelectedChanged == null)
					{
						return;
					}
					onIsSelectedChanged.Invoke(value);
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000582A File Offset: 0x00003A2A
		public AudioClip Clip { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005832 File Offset: 0x00003A32
		public string Name { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000583A File Offset: 0x00003A3A
		public string DurationText { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005842 File Offset: 0x00003A42
		public string InfoTextID { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000584A File Offset: 0x00003A4A
		public IReadOnlyDictionary<string, string> InfoTextVariables { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005852 File Offset: 0x00003A52
		public string ID { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000585A File Offset: 0x00003A5A
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005862 File Offset: 0x00003A62
		public Action<bool> OnIsPlayingChanged { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000586B File Offset: 0x00003A6B
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00005873 File Offset: 0x00003A73
		public Action<bool> OnIsSelectedChanged { get; set; }

		// Token: 0x0600013C RID: 316 RVA: 0x0000587C File Offset: 0x00003A7C
		public Soundtrack(SoundtrackClip clip)
		{
			this.ID = clip.ID;
			this.Name = clip.Name;
			this.Clip = clip.Clip;
			this.DurationText = clip.DurationText;
			this.InfoTextID = clip.InfoTextID;
			this.InfoTextVariables = clip.InfoTextVariables;
		}

		// Token: 0x040000EA RID: 234
		private bool _isPlaying;

		// Token: 0x040000EB RID: 235
		private bool _isSelected;
	}
}
