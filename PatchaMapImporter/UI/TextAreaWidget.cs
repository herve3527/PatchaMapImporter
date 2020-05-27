namespace PatchaMapImporter.UI
{
	using UnityEngine;

	/// <summary>
	/// Label + Textarea 
	/// </summary>
	class TextAreaWidget
	{
		/// <summary>
		/// Construct with existing flags
		/// </summary>
		/// <param name="flags"></param>
		public TextAreaWidget(string label, string data)
		{
			using (new GUILayout.HorizontalScope()) {
				GUILayout.Label(label, GUILayout.ExpandWidth(false));
				Value = GUILayout.TextArea(data, GUILayout.Height(120));
			}
		}

		/// <summary>
		/// Return current value
		/// </summary>
		public string Value { get; private set; }
	}
}