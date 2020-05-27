﻿namespace PatchaMapImporter
{
	using Models;
	using System.IO;
	using Tools;
	using UI;
	using UnityEngine;

	/// <summary>
	/// Mod Map Importer
	/// </summary>
	public class PatchaMapImporter : MonoBehaviour
	{
		public static string MapsDirectory = Path.Combine(Application.dataPath, "CustomMaps");

		void Start()
		{
			_mapLoader = new MapLoader(MapsDirectory, this);
			_mapManager = new MapManager(MapsDirectory);
		}

		/// <summary>
		/// On Update
		/// </summary>
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.L) && !_editUiVisible) _mainUiVisible = !_mainUiVisible;
			if (Input.GetKeyDown(KeyCode.E) && _currentMap != null) _editUiVisible = !_editUiVisible;
		}

		/// <summary>
		/// On GUI
		/// </summary>
		void OnGUI()
		{
			if (_mainUiVisible) ShowMainUi();
			if (_editUiVisible) ShowEditUi();
		}


		/// <summary>
		/// Show main UI with filter and map list
		/// </summary>
		/// <returns></returns>
		void ShowMainUi()
		{
			var mainRect = new Rect(Screen.width / 3, 100, Screen.width / 3, Screen.height - 200);

			//fixed positioning

			//simple box
			GUI.Box(mainRect, "");

			//patchamak trail logo
			GUI.Label(new Rect(mainRect.x - 50, mainRect.y - 40, 128, 128), new GUIContent(LogoPatcha));

			//main content
			using (new GUILayout.AreaScope(new Rect(mainRect.x + 25, mainRect.y + 30, mainRect.width - 50, mainRect.height - 40))) {
				using (new GUILayout.VerticalScope()) {
					new TitleWidget("Patcha'Map Importer", 30);

					GUILayout.Space(30);

					using (new DisableAllButtons(_editUiVisible)) {

						//show filter and ordering widget
						var filter = new FilterWidget(_mapManager.CurrentFilter).Value;
						if (filter.ToString() != _mapManager.CurrentFilter.ToString()) //todo : make operator Equals instead of comparing strings
						{
							Log.Write("MI: Applying new filter !");
							_mapManager.Filter(filter);
							_mainUiScrollPos = Vector2.zero;
						}

						GUILayout.Space(10);

						//show map list and wire button actions
						new MapListWidget(ref _mainUiScrollPos, _mapManager.Maps,
							map => {
								//if (_editUiVisible) return; //deactivate if editing

								Log.Write($"MI: Load map '{map.Filename}'");
								_currentMap = map;
								_mapLoader.Load(map);
								_mainUiVisible = false;
							},
							map => {
								//if (_editUiVisible) return; //deactivate if editing

								Log.Write($"MI: Edit map '{map.Filename}'");
								_currentMap = map;
								_editUiVisible = true;
							}
						);

						GUILayout.Space(5);

						//save on bottom (todo:remove it when editmap ui is done)
						using (new GUILayout.HorizontalScope()) {
							GUILayout.FlexibleSpace();

							if (GUILayout.Button("Save")) _mapManager.Save();
							if (GUILayout.Button("Reload")) _mapManager.Load();
							if (GUILayout.Button("Close")) {
								_mainUiVisible = false;
								_editUiVisible = false;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Show property editor UI
		/// </summary>
		void ShowEditUi()
		{
			if (_currentMap != null) {

				if (_currentMapEditInfos == null) {
					_currentMapEditInfos = new Map(_currentMap.Filename, _currentMap.DisplayName, _currentMap.Authors, _currentMap.Description, _currentMap.Rating, _currentMap.Flags);
				}

				var editRect = new Rect((Screen.width / 2) - (Screen.width / 8), 300, Screen.width / 4, 325);
				GUI.Box(editRect, "", new GUIStyle() { normal = new GUIStyleState() { background = TextureHelpers.MakeTexture(2, 2, new Color(.3f, .3f, .3f, 1f)) } });

				var style = new GUIStyle() {
					padding = new RectOffset(20, 25, 25, 25),
					border = new RectOffset(1, 1, 1, 1)
				};

				//map property editor
				using (new GUILayout.AreaScope(editRect, "", style)) {

					new TitleWidget($"Map filename : {_currentMapEditInfos.Filename}", 20);

					GUILayout.Space(20);

					new EditMapWidget(ref _currentMapEditInfos,
						map => {
							Log.Write($"MI: Save '{map.Filename}'");
							_currentMap.CopyEditableInfos(map);
							_mapManager.Save();
							_editUiVisible = false;
							_currentMapEditInfos = null;
						},
						map => {
							Log.Write($"MI: Cancel edit of '{map.Filename}'");
							_editUiVisible = false;
							_currentMapEditInfos = null;
						}
					);
				}

				GUI.Label(new Rect(editRect.x - 25, editRect.y - 20, 64, 64), new GUIContent(LogoPatcha));
			}
		}

		//main ui vars
		private bool _mainUiVisible = false;
		private Vector2 _mainUiScrollPos = Vector2.zero;
		static public Texture2D LogoPatcha = TextureHelpers.MakeTextureFromData(EmbeddedRessourceHelper.ReadResourceFile("PatchaMapImporter.Assets.patcha-128.png"), 128, 128);

		//edit ui vars
		private bool _editUiVisible = false;
		private Map _currentMapEditInfos;

		//--privates-------------------------

		private MapLoader _mapLoader;
		private MapManager _mapManager;
		private Map _currentMap;


	}
}
