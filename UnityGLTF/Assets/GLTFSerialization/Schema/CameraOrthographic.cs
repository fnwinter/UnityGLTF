using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GLTF.Schema
{
	/// <summary>
	/// An orthographic camera containing properties to create an orthographic
	/// projection matrix.
	/// </summary>
	[System.Serializable]
	public class CameraOrthographic : GLTFProperty
	{
		/// <summary>
		/// The floating-point horizontal magnification of the view.
		/// </summary>
		public double xmag;

		/// <summary>
		/// The floating-point vertical magnification of the view.
		/// </summary>
		public double ymag;

		/// <summary>
		/// The floating-point distance to the far clipping plane.
		/// </summary>
		public double zfar;

		/// <summary>
		/// The floating-point distance to the near clipping plane.
		/// </summary>
		public double znear;

		public CameraOrthographic()
		{
		}

		public CameraOrthographic(CameraOrthographic cameraOrthographic) : base(cameraOrthographic)
		{
			xmag = cameraOrthographic.xmag;
			ymag = cameraOrthographic.ymag;
			zfar = cameraOrthographic.zfar;
			znear = cameraOrthographic.znear;
		}

		public static CameraOrthographic Deserialize(GLTFRoot root, JsonReader reader)
		{
			var cameraOrthographic = new CameraOrthographic();

			if (reader.Read() && reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Orthographic camera must be an object.");
			}

			while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
			{
				var curProp = reader.Value.ToString();

				switch (curProp)
				{
					case "xmag":
						cameraOrthographic.xmag = reader.ReadAsDouble().Value;
						break;
					case "ymag":
						cameraOrthographic.ymag = reader.ReadAsDouble().Value;
						break;
					case "zfar":
						cameraOrthographic.zfar = reader.ReadAsDouble().Value;
						break;
					case "znear":
						cameraOrthographic.znear = reader.ReadAsDouble().Value;
						break;
					default:
						cameraOrthographic.DefaultPropertyDeserializer(root, reader);
						break;
				}
			}

			return cameraOrthographic;
		}

		public override void Serialize(JsonWriter writer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("xmag");
			writer.WriteValue(xmag);

			writer.WritePropertyName("ymag");
			writer.WriteValue(ymag);

			writer.WritePropertyName("zfar");
			writer.WriteValue(zfar);

			writer.WritePropertyName("znear");
			writer.WriteValue(znear);

			base.Serialize(writer);

			writer.WriteEndObject();
		}
	}
}
