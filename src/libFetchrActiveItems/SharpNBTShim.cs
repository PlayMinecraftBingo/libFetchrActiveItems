using SharpNBT;
using System;
using System.IO;
using System.IO.Compression;

namespace libFetchrActiveItems
{
	internal static class SharpNBTShim
	{
		// Derived from https://github.com/ForeverZer0/SharpNBT/blob/master/SharpNBT/NbtFile.cs

		internal static CompoundTag ReadFromStream(Stream stream, FormatOptions options, CompressionType compression = CompressionType.AutoDetect)
		{
			using (TagReader reader = new(GetReadStream(stream, compression), options))
			{
				return reader.ReadTag<CompoundTag>();
			}
		}

		private static Stream GetReadStream(Stream stream, CompressionType compression)
		{
			if (compression == CompressionType.AutoDetect)
			{
				byte firstByte = (byte)stream.ReadByte();
				stream.Seek(0, SeekOrigin.Begin);

				compression = firstByte switch
				{
					0x78 => CompressionType.ZLib,
					0x1F => CompressionType.GZip,
					0x08 => CompressionType.None, // ListTag (valid in Bedrock)
					0x0A => CompressionType.None, // CompoundTag
					_ => throw new FormatException("CannotDetectCompression")
				};
			}

			return compression switch
			{
				CompressionType.None => stream,
				CompressionType.GZip => new BufferedStream(new GZipStream(stream, CompressionMode.Decompress, false)),
				CompressionType.ZLib => new BufferedStream(new ZLibStream(stream, CompressionMode.Decompress)),
				_ => throw new ArgumentOutOfRangeException(nameof(compression), compression, null)
			};
		}
	}
}
