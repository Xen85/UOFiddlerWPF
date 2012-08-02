#region License Header

/***************************************************************************
 *   Copyright (c) 2011 OpenUO Software Team.
 *   All Right Reserved.
 *
 *   $Id: $:
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 ***************************************************************************/

#endregion

using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenUO.Ultima.Adapters;

namespace OpenUO.Ultima.PresentationFramework.Adapters
{
    internal class TexmapImageSourceAdapter : StorageAdapterBase, ITexmapStorageAdapter<ImageSource>
    {
        private FileIndex _fileIndex;

        #region ITexmapStorageAdapter<ImageSource> Members

        public override void Initialize()
        {
            base.Initialize();

            InstallLocation install = Install;

            _fileIndex = install.CreateFileIndex("texidx.mul", "texmaps.mul");
        }

        public unsafe ImageSource GetTexmap(int index)
        {
            int length, extra;
            Stream stream = _fileIndex.Seek(index, out length, out extra);

            if (stream == null)
                return null;

            int size = extra == 0 ? 64 : 128;

            var bin = new BinaryReader(stream);
            var bmp = new WriteableBitmap(size, size, 96, 96, PixelFormats.Bgr555, null);
            bmp.Lock();

            var line = (ushort*) bmp.BackBuffer;
            int delta = bmp.BackBufferStride >> 1;

            for (int y = 0; y < size; ++y, line += delta)
            {
                ushort* cur = line;
                ushort* end = cur + size;

                while (cur < end)
                    *cur++ = (ushort) (bin.ReadUInt16() ^ 0x8000);
            }

            bmp.AddDirtyRect(new Int32Rect(0, 0, size, size));
            bmp.Unlock();

            return bmp;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_fileIndex != null)
            {
                _fileIndex.Close();
                _fileIndex = null;
            }
        }
    }
}