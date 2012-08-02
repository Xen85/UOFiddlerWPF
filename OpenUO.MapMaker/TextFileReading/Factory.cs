using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.LandSets;

namespace OpenUO.MapMaker.TextFileReading
{
    public class Factory
    {
        #region fields
        protected readonly List<string> Strings;
        #endregion

        public static char[] separator = { '\t', ' ' };

        public Color ReadColorFromInt(int number)
        {
            var bytes = BitConverter.GetBytes(number);
            Array.Reverse(bytes);
            return Color.FromArgb(byte.MaxValue, bytes[1], bytes[2], bytes[3]);
        }
        public Color ReadColorFromInt(String number)
        {
            return ColorTranslator.FromHtml(number.Replace("0x","#"));
        }
        #region props
        #endregion
        
        public Factory(string location)
        {
            Strings = File.ReadAllLines(location).ToList();
        }

        public virtual void Read()
        {
            
        }
    }
}
