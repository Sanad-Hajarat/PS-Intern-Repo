using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SanadDiP
{
    /// <CREDITS>
    /// Sanad Hajarat
    /// Data Science Intern
    /// ProgressSoft Corporation
    /// </CREDITS>

    public class BitmapFilter
    {
        public static Bitmap GrayConvert8(Bitmap b)
        {
            if (b.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                return b;
            }
            BitmapData b1 = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            
            Bitmap newB = new Bitmap(b.Width,b.Height,PixelFormat.Format8bppIndexed);
            BitmapData b2 = newB.LockBits(new Rectangle(0, 0, newB.Width, newB.Height),ImageLockMode.ReadWrite,PixelFormat.Format8bppIndexed);
            
            int stride = b1.Stride;
            int stride2 = b2.Stride;

            ColorPalette palette = newB.Palette;
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            newB.Palette = palette;
            
            unsafe
            {
                byte* ptr = (byte*) b1.Scan0;
                byte* ptr2 = (byte*) b2.Scan0;

                int offset = stride - b.Width * 3;
                int offset2 = stride2 - newB.Width;
                
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        ptr2[0] = (byte)(.299 * ptr[2] + .587 * ptr[1] + .114 * ptr[0]);
                        
                        ptr += 3;
                        ptr2++;
                    }
                    ptr += offset;
                    ptr2 += offset2;
                }
            }

            b.UnlockBits(b1);
            newB.UnlockBits(b2);

            return newB;
        }
        public static bool GrayScale(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;
                
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        p[0] = p[1] = p[2] = (byte)(.299 * p[2] + .587 * p[1] + .114 * p[0]);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }
        public static bool ApplyStaticThresh(Bitmap b, int t, bool inverse)
        {
            if (t > 255 || t < 0)
                return false;

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;

            byte white = 255;
            byte black = 0;
            if (inverse) { white = 0; black = 255; }
            
            unsafe
            {
                byte* p = (byte*)bmData.Scan0;

                int nWidth = b.Width * 3;
                int nOffset = stride - nWidth;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        if (p[0] >= t) { p[0] = white; } else { p[0] = black; }
                        ++p; 
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool ApplyMeanThresh(Bitmap b, bool inverse)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;

            byte white = 255;
            byte black = 0;
            if (inverse) { white = 0; black = 255; }

            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0;

                int nWidth = b.Width * 3;
                int nOffset = stride - nWidth;

                int sum = 0;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        sum += p[0];
                        ++p;

                    }
                    p += nOffset;
                }
    
                int t = sum / (nWidth * b.Height); //set threshold
                p = (byte*)bmData.Scan0;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {

                        if (p[0] >= t) { p[0] = white; } else { p[0] = black; }
                        ++p;

                    }
                    p += nOffset;
                }
            }
            b.UnlockBits(bmData);

            return true;
        }
        public static Bitmap ConcatImages(Bitmap img1, Bitmap img2, bool horizontal)
        {
            Bitmap concatted;

            BitmapData bmData1 = img1.LockBits(new Rectangle(0, 0, img1.Width, img1.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride1 = bmData1.Stride;
            
            BitmapData bmData2 = img2.LockBits(new Rectangle(0, 0, img2.Width, img2.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride2 = bmData2.Stride;

            if (horizontal)
            {
                concatted = new Bitmap(img1.Width + img2.Width, img1.Height > img2.Height ? img1.Height : img2.Height);
                BitmapData bmData0 = concatted.LockBits(new Rectangle(0, 0, concatted.Width, concatted.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int stride0 = bmData0.Stride;

                unsafe
                {
                    byte* p0 = (byte*)bmData0.Scan0;
                    byte* p1 = (byte*)bmData1.Scan0;
                    byte* p2 = (byte*)bmData2.Scan0;

                    int nWidth0 = concatted.Width * 3;
                    int nOffset0 = stride0 - nWidth0;
                    int nWidth1 = img1.Width * 3;
                    int nOffset1 = stride1 - nWidth1;
                    int nWidth2 = img2.Width * 3;
                    int nOffset2 = stride2 - nWidth2;

                    for (int y = 0; y < concatted.Height; ++y)
                    {
                        for (int x = 0; x < img1.Width; ++x)
                        {
                            p0[0] = p1[0];
                            p0[1] = p1[1];
                            p0[2] = p1[2];
                            p0 += 3;
                            p1 += 3;
                        }
                        p1 += nOffset1;

                        for (int x = 0; x < img2.Width; ++x)
                        {
                            p0[0] = p2[0];
                            p0[1] = p2[1];
                            p0[2] = p2[2];
                            p0 += 3;
                            p2 += 3;
                        }
                        p0 += nOffset0;
                        p2 += nOffset2;
                    }
                }
                concatted.UnlockBits(bmData0);
            }
            else
            {
                concatted = new Bitmap(img1.Width > img2.Width ? img1.Width : img2.Width, img1.Height + img2.Height);
                BitmapData bmData0 = concatted.LockBits(new Rectangle(0, 0, concatted.Width, concatted.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int stride0 = bmData0.Stride;

                unsafe
                {
                    byte* p0 = (byte*)bmData0.Scan0;
                    byte* p1 = (byte*)bmData1.Scan0;
                    byte* p2 = (byte*)bmData2.Scan0;

                    int nWidth0 = concatted.Width * 3;
                    int nOffset0 = stride0 - nWidth0;
                    int nWidth1 = img1.Width * 3;
                    int nOffset1 = stride1 - nWidth1;
                    int nWidth2 = img2.Width * 3;
                    int nOffset2 = stride2 - nWidth2;

                    for (int y = 0; y < img1.Height; ++y)
                    {
                        for (int x = 0; x < concatted.Width; ++x)
                        {
                            p0[0] = p1[0];
                            p0[1] = p1[1];
                            p0[2] = p1[2];
                            p0 += 3;
                            p1 += 3;
                        }
                        p1 += nOffset1;
                        p0 += nOffset0;
                    }
                    for (int y = 0; y < img2.Height; ++y)
                    {
                        for (int x = 0; x < concatted.Width; ++x)
                        {
                            p0[0] = p2[0];
                            p0[1] = p2[1];
                            p0[2] = p2[2];
                            p0 += 3;
                            p2 += 3;
                        }
                        p0 += nOffset0;
                        p2 += nOffset2;
                    }
                }
                concatted.UnlockBits(bmData0);
            }

            return concatted;
        }

        public static Bitmap Resize(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap bTemp = (Bitmap)b.Clone();
            b = new Bitmap(nWidth, nHeight, bTemp.PixelFormat);

            double nXFactor = (double)bTemp.Width / (double)nWidth;
            double nYFactor = (double)bTemp.Height / (double)nHeight;
          
            // BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            // int stride = bmData.Stride;
            //
            // unsafe
            // {
            //   byte* p = (byte*)bmData.Scan0;
            //
            //   int bwidth = b.Width * 3;
            //   int nOffset = stride - nWidth;
            //
            //   for (int y = 0; y < b.Height; ++y)
            //   {
            //       for (int x = 0; x < bwidth; ++x)
            //       {
            //           p[0] = bTemp.GetPixel((int)(Math.Floor(x * nXFactor)), (int)(Math.Floor(y * nYFactor)));
            //           ++p;
            //       }
            //
            //       p += nOffset;
            //   }
            // }
            //
            // b.UnlockBits(bmData);
            
            for (int x = 0; x < b.Width; ++x)
                for (int y = 0; y < b.Height; ++y)
                    b.SetPixel(x, y, bTemp.GetPixel((int)(Math.Floor(x * nXFactor)), (int)(Math.Floor(y * nYFactor))));

            return b;
        }
    }
}
