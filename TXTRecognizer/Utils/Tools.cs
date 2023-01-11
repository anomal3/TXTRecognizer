using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Docnet.Core;
using Docnet.Core.Converters;
using Docnet.Core.Models;
using Emgu.CV;
using Xunit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TXTRecognizer.Utils
{
    
    [Collection("Example collection")]
    public partial class Tools
    {
        private readonly Fixture _fixture;
        public Tools(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ConvertPageToSimpleImage(string inpath, string outpath)
        {
            using var docReader = _fixture.DocNet.GetDocReader(inpath, new PageDimensions(1080, 1920));

            using var pageReader = docReader.GetPageReader(0);

            var rawBytes = pageReader.GetImage();

            var width = pageReader.GetPageWidth();
            var height = pageReader.GetPageHeight();

            var characters = pageReader.GetCharacters();

            using var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            AddBytes(bmp, rawBytes);
            //DrawRectangles(bmp, characters);

            using var stream = new MemoryStream();

            bmp.Save(stream, ImageFormat.Png);

            //TODO : Сделать через FileStream
            File.WriteAllBytes(outpath, stream.ToArray());

            stream.Close();
            stream.Dispose();

        }
        public static void CopyStream(Stream input, Stream output)
        {
            input.CopyTo(output);
        }


        private static void AddBytes(Bitmap bmp, byte[] rawBytes)
        {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            var bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);
            var pNative = bmpData.Scan0;

            Marshal.Copy(rawBytes, 0, pNative, rawBytes.Length);
            bmp.UnlockBits(bmpData);
        }

        /// <summary>
        /// Обводит буквы красным квадратом
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="characters"></param>
        private static void DrawRectangles(Bitmap bmp, IEnumerable<Character> characters)
        {
            var pen = new Pen(Color.Red);

            using var graphics = Graphics.FromImage(bmp);

            foreach (var c in characters)
            {
                var rect = new Rectangle(c.Box.Left, c.Box.Top, c.Box.Right - c.Box.Left, c.Box.Bottom - c.Box.Top);
                graphics.DrawRectangle(pen, rect);
            }
        }

        /// <summary>
        /// Экспорт текста в файл
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="TextboxAsSave"></param>
        public static void ExportFile(string ext, Control TextboxAsSave)
        {
            System.Windows.Forms.TextBox tbTextRec = (System.Windows.Forms.TextBox)TextboxAsSave;
            if (string.IsNullOrEmpty(tbTextRec.Text) || string.IsNullOrWhiteSpace(tbTextRec.Text))
            {
                MessageBox.Show("Нечего сохранять. Текстовое поле не заполнено", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = $"{ext.ToUpper()} File|*.{ext.ToLower()}"
            };

            if (sfd.ShowDialog() == DialogResult.Cancel) return;

            string fileName = sfd.FileName;

            File.WriteAllText(fileName, tbTextRec.Text);
            MessageBox.Show($"Файл успешно сохранён по пути\r{fileName}", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Поиск итемов по загловку меню
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in GetItems(dropDownItem))
                        yield return subItem;
                }
                yield return dropDownItem;
            }
        }

        /// <summary>
        /// Поиск котролов по типу
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static List<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type).ToList();
        }
    }

    public class Fixture : IDisposable
    {
        public IDocLib DocNet { get; }

        public Fixture()
        {
            DocNet = DocLib.Instance;
        }

        public void Dispose()
        {
            DocNet.Dispose();
        }
    }

    [CollectionDefinition("Example collection")]
    public class ExampleCollection : ICollectionFixture<Fixture> { }
}
