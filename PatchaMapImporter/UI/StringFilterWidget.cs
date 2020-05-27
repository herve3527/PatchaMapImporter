namespace PatchaMapImporter.UI
{
	using Tools;
	using UnityEngine;

	/// <summary>
	/// Represent the string filter
	/// </summary>
	class StringFilterWidget
	{
		/// <summary>
		/// Construct with existing flags
		/// </summary>
		/// <param name="flags"></param>
		public StringFilterWidget(string search)
		{
			var style = new GUIStyle {
				fontSize = 15,
				normal = new GUIStyleState {
					textColor = Color.white
				}
			};

			using (new GUILayout.HorizontalScope(style)) {
				Value = new TextEditWidget("Filter by names : ", search).Value;
				GUILayout.Space(5);
				if (GUILayout.Button("Clear", GUILayout.ExpandWidth(false))) {
					Value = "";
				}
			}

			if (search != Value) {
				Log.Write("StringFilterWidget: has changed ! [" + search + "] => [" + Value + "]");
			}
		}

		/// <summary>
		/// Return current value
		/// </summary>
		public string Value { get; private set; }
	}
}
