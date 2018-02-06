using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet
{
    public class BitmapFont
    {
        public BitmapFont(Bitmap bitmap, int startChar, int charWidth, int charHeight)
        {
            FontMap = bitmap;

            CharacterWidth = charWidth;
            CharacterHeight = charHeight;
            CharactersPerRow = bitmap.Width / CharacterWidth;
            StartCharacter = startChar;
        }

        public Bitmap FontMap { get; private set; }
        public int StartCharacter { get; private set; }
        public int CharacterWidth { get; private set; }
        public int CharacterHeight { get; private set; }
        public int CharactersPerRow { get; private set; }

        public static BitmapFont EightByEight = new BitmapFont(Properties.Resources._8x8, 0, 8, 8);
    }

    public static class BitmapFontRenderer
    {
        public static void RenderBitmapFont(this Graphics g, BitmapFont font, string text, Point position, Color color, int scale = 1)
        {
            text = text.Replace("\r", "");
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            int renderOffsetX = 0;
            int renderOffsetY = 0;

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]{
            new float[] {color.R / 255.0f, 0, 0, 0, 0},
            new float[] {0, color.G / 255.0f, 0, 0, 0},
            new float[] {0, 0, color.B / 255.0f, 0, 0},
            new float[] {0, 0, 0, color.A / 255.0f, 0},
            new float[] {0, 0, 0, 0, 1}});
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix);

            foreach (char c in text)
            {
                int charOffset = c - font.StartCharacter;
                if (c == '\n')
                {
                    renderOffsetX = 0;
                    renderOffsetY += font.CharacterHeight * scale;
                    continue;
                }

                int charSrcX = (charOffset % font.CharactersPerRow) * font.CharacterWidth;
                int charSrcY = (charOffset / font.CharactersPerRow) * font.CharacterHeight;

                g.DrawImage(font.FontMap, new Rectangle(position.X + renderOffsetX, position.Y + renderOffsetY, font.CharacterWidth * scale, font.CharacterHeight * scale), charSrcX, charSrcY, font.CharacterWidth, font.CharacterHeight, GraphicsUnit.Pixel, imgAttributes);

                renderOffsetX += font.CharacterWidth * scale;
            }
        }

        public static int GetStringHeight(BitmapFont font, string text, int scale = 1)
        {
            int c = (text.Count(x => x == '\n') + 1);
            if (text.EndsWith("\n"))
                c--;

            return c * font.CharacterHeight * scale;
        }
    }
}