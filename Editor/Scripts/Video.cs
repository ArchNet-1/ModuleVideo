using TMPro;
using UnityEngine;

namespace ArchNet.Module.Video
{
	/// <summary>
	/// [MODULE] - [ARCH NET] - [VIDEO]
	/// author : LOUIS PAKEL
	/// </summary>
	public class Video : MonoBehaviour
	{
		#region Serialized Fields

		[Header("Dropdowns")]
		// Main Music Audio Source
		[SerializeField, Tooltip("Resolution Dropdown")]
		private TMP_Dropdown _resolutionDropdown = null;

		// Main Game Sound Audio Source
		[SerializeField, Tooltip("Mode Dropdown")]
		private TMP_Dropdown _modeDropdown = null;

		// Music Slider
		[SerializeField, Tooltip("Quality Dropdown")]
		private TMP_Dropdown _qualityDropdown = null;

		// Game Sound Slider"
		[SerializeField, Tooltip("Anti Aliasing Dropdown")]
		private TMP_Dropdown _antiAliasingDropdown = null;

		#endregion

		#region Private Fields

		// Video Resolution
		private Resolution _videoResolution;

		// Video FullScreen Mode
		private FullScreenMode _VideoMode;

		#endregion

		#region Private Methods

		private void Start()
		{
			// Set Video Resolution Value
			_videoResolution = Screen.currentResolution;

			// Set Video Mode Value
			_VideoMode = Screen.fullScreenMode;

			// OPTIONAL
			// Force set dropdown component
			ForceSetModule();

			// Check if the module is available
			ModuleAvailable();

			// Init Drop Down
			InitDropDowns();
		}

		/// <summary>
		/// Description : Initiate all values
		/// </summary>
		private void ForceSetModule()
		{
			// force set shadown level dropdown
			if (null == _qualityDropdown)
			{
				_qualityDropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
			}

			// force set shadow quality dropdown
			if (null == _resolutionDropdown)
			{
				_resolutionDropdown = transform.GetChild(1).GetComponent<TMP_Dropdown>();
			}

			// force set shadown level dropdown
			if (null == _modeDropdown)
			{
				_modeDropdown = transform.GetChild(2).GetComponent<TMP_Dropdown>();
			}

			// force set shadow quality dropdown
			if (null == _antiAliasingDropdown)
			{
				_antiAliasingDropdown = transform.GetChild(3).GetComponent<TMP_Dropdown>();
			}
		}

		/// <summary>
		/// Description : Check if the module is available for runtime
		/// </summary>
		private void ModuleAvailable()
		{
			// Shadow level dropdown and shadown quality dropdown must not be empty
			if (null == _antiAliasingDropdown
				|| null == _modeDropdown
				|| null == _qualityDropdown
				|| null == _resolutionDropdown)
			{
				Debug.Log(Constants.ERROR_411);
			}
		}

		/// <summary>
		/// Description : Initiate all drop down
		/// </summary>
		private void InitDropDowns()
		{
			// Set video Resolution drop down
			switch (_videoResolution.height)
			{
				case 1920:
					_resolutionDropdown.value = 0;
					break;
				case 1600:
					_resolutionDropdown.value = 1;
					break;
				case 1280:
					_resolutionDropdown.value = 2;
					break;
				case 1024:
					_resolutionDropdown.value = 3;
					break;
				case 640:
					_resolutionDropdown.value = 4;
					break;
			}

			// Set video mode drop down
			switch (_VideoMode)
			{
				case FullScreenMode.ExclusiveFullScreen:
					_modeDropdown.value = 0;
					break;
				case FullScreenMode.Windowed:
					_modeDropdown.value = 1;
					break;
				case FullScreenMode.FullScreenWindow:
					_modeDropdown.value = 2;
					break;
			}

			// Set Anti aliasing drop down
			switch (QualitySettings.antiAliasing)
			{
				case 2:
					_antiAliasingDropdown.value = 0;
					break;
				case 4:
					_antiAliasingDropdown.value = 1;
					break;
				case 8:
					_antiAliasingDropdown.value = 2;
					break;
			}

			// Set Quality Drop down
			_qualityDropdown.value = QualitySettings.GetQualityLevel();
		}

		private void ValueChanged()
		{
			// SET NEW WINDOW MODE
			switch (_modeDropdown.value)
			{
				case 0:
					_VideoMode = FullScreenMode.ExclusiveFullScreen;
					break;
				case 1:
					_VideoMode = FullScreenMode.Windowed;
					break;
				case 2:
					_VideoMode = FullScreenMode.FullScreenWindow;
					break;
			}

			// SET NEW RESOLUTION
			switch (_resolutionDropdown.value)
			{
				case 0:
					Screen.SetResolution(1920, 1080, _VideoMode, 60);
					break;
				case 1:
					Screen.SetResolution(1600, 900, _VideoMode, 60);
					break;
				case 2:
					Screen.SetResolution(1280, 720, _VideoMode, 60);
					break;
				case 3:
					Screen.SetResolution(1024, 576, _VideoMode, 60);
					break;
				case 4:
					Screen.SetResolution(640, 360, _VideoMode, 60);
					break;
			}

			//SET NEW ANTI ALIASING QUALITY
			switch (_antiAliasingDropdown.value)
			{
				case 0:
					QualitySettings.antiAliasing = 2;
					break;
				case 1:
					QualitySettings.antiAliasing = 4;
					break;
				case 2:
					QualitySettings.antiAliasing = 8;
					break;
			}

			// SET NEW QUALITY LEVEL AND ANTI ALIASING
			QualitySettings.SetQualityLevel(_qualityDropdown.value, true);
		}

		#endregion

		#region Public Methods

		public void Save()
		{
			// UPDATE VIDEO SETTINGS
			ValueChanged();
		}

        #endregion
    }
}