﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace Onyx3D
{

    public enum CubemapFace
    {
        Right,
        Left,
        Top,
        Bottom,
        Back,
        Front
    };

    public class Cubemap : IDisposable
    {
        IntPtr data;

        private int mId;

		public int Id { get { return mId; } }

		public Cubemap()
		{
			GL.GenTextures(1, out mId);
			GL.BindTexture(TextureTarget.TextureCubeMap, mId);

			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
		}

		public void SetTexture(CubemapFace face, Texture t)
		{
            if (data == IntPtr.Zero)
                data = Marshal.AllocHGlobal(32 * t.Width * t.Height);

			GL.GetTextureImage(t.Id, 0, PixelFormat.Rgba, PixelType.UnsignedByte, 32 * t.Width * t.Height, data);
			GL.BindTexture(TextureTarget.TextureCubeMap, mId);
			GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + (int)face, 0, PixelInternalFormat.Rgba, t.Width, t.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
		}

		public void GenerateMipmaps()
		{
			GL.GenerateMipmap(GenerateMipmapTarget.TextureCubeMap);
		}

		public void Dispose()
		{
			GL.DeleteTexture(mId);
		}

    }

    

}
