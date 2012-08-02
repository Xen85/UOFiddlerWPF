using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using OpenUO.MapMaker.LandSets;

namespace OpenUO.MapMaker
{
    public class BitmapReader 
    {
        public Color[] BitmapColors { get; private set; }

        public BitmapReader(string bitmapLocation, Boolean Altitude)
        {
            try
            {
                Cache(bitmapLocation, Altitude);
            }
            catch(Exception)
            {
            }
        }

        /// <summary>
        /// Just cache the bmp in ram
        /// </summary>
        /// <param name="location"></param>
        private void Cache(string location, bool altitude)
        {
            using (var bitmap = new Bitmap(location))
            {
               
                    BitmapColors = new Color[bitmap.Width*bitmap.Height];
                    BitmapColors.Initialize();
                
                    // Lock the bitmap's bits.  
                    var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    
                    //lock the bitmap bits
                    BitmapData bmpData;
                    
                    bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                    // Get the address of the first line.
                    var ptr = bmpData.Scan0;

                    // Declare an array to hold the bytes of the bitmap.
                    var bytes = bmpData.Stride*bitmap.Height;
                    var rgbValues = new byte[bytes];


                    // Copy the RGB values into the array.
                    Marshal.Copy(ptr, rgbValues, 0, bytes);

                    var stride = bmpData.Stride;
                    
                    for (var coulmn = bmpData.Height - 1; coulmn >= 0; coulmn--)
                    {
                        for (var row = 0; row < bmpData.Width; row++)
                        {
                            BitmapColors[(coulmn * (bmpData.Width - 1)) + row] = Color.FromArgb((rgbValues[(coulmn * stride) + (row * 3) + 2]),
                                                                       rgbValues[(coulmn*stride) + (row*3) + 1],
                                                                       rgbValues[(coulmn*stride) + (row*3)]);
                        }
                    }
            }
        }

    }
}
