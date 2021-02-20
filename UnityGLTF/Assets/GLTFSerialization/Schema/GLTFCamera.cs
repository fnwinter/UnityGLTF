using Newtonsoft.Json;
using UnityEngine;

namespace GLTF.Schema
{
	/// <summary>
	/// A camera's projection.  A node can reference a camera to apply a transform
	/// to place the camera in the scene
	/// </summary>
	[SerializeField]
	public class GLTFCamera : GLTFChildOfRootProperty
	{
		public string type;

		/// <summary>
		/// An orthographic camera containing properties to create an orthographic
		/// projection matrix.
		/// </summary>
		public CameraOrthographic orthographic;

		/// <summary>
		/// A perspective camera containing properties to create a perspective
		/// projection matrix.
		/// </summary>
		public CameraPerspective perspective;

		/// <summary>
		/// Specifies if the camera uses a perspective or orthographic projection.
		/// Based on this, either the camera's `perspective` or `orthographic` property
		/// will be defined.
		/// </summary>
		public CameraType Type;

		public GLTFCamera()
		{
		}

		public GLTFCamera(GLTFCamera camera, GLTFRoot gltfRoot) : base(camera, gltfRoot)
		{
			if (camera == null) return;

			if (camera.orthographic != null)
			{
				orthographic = new CameraOrthographic(camera.orthographic);
			}

			if (camera.perspective != null)
			{
				perspective = new CameraPerspective(camera.perspective);
			}

			Type = camera.Type;
		}

		public static GLTFCamera Deserialize(GLTFRoot root, JsonReader reader)
		{
			var camera = new GLTFCamera();

			while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
			{
				var curProp = reader.Value.ToString();

				switch (curProp)
				{
					case "orthographic":
						camera.orthographic = CameraOrthographic.Deserialize(root, reader);
						break;
					case "perspective":
						camera.perspective = CameraPerspective.Deserialize(root, reader);
						break;
					default:
						camera.DefaultPropertyDeserializer(root, reader);
						break;
				}
			}

			return camera;
		}

		public override void Serialize(JsonWriter writer)
		{
			writer.WriteStartObject();

			if (orthographic != null)
			{
				writer.WritePropertyName("orthographic");
				orthographic.Serialize(writer);
			}

			if (perspective != null)
			{
				writer.WritePropertyName("perspective");
				perspective.Serialize(writer);
			}

			writer.WritePropertyName("type");
			writer.WriteValue(type);

			base.Serialize(writer);

			writer.WriteEndObject();
		}
	}

	public enum CameraType
	{
		perspective,
		orthographic
	}
}
