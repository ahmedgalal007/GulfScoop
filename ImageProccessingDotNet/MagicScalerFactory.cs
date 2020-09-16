using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace ImageProccessingDotNet
{
    public static class MagicScalerFactory
    {
        public static void ProccessImag(string inPath, string outPath ,CropBox cropBox)
        {
            const int quality = 75;

            var settings = new ProcessImageSettings()
            {
                Crop = new Rectangle(new Point(cropBox.x,cropBox.y), new Size(cropBox.width,cropBox.height)),
                Width = cropBox.width,
                Height = cropBox.height,
                ResizeMode = CropScaleMode.Max,
                SaveFormat = FileFormat.Jpeg,
                JpegQuality = quality,
                JpegSubsampleMode = ChromaSubsampleMode.Subsample420
            };

            //using (var output = new FileStream(OutputPath(path, outputDirectory, MagicScaler), FileMode.Create))
            using (var output = new FileStream(outPath, FileMode.Create))
            {
                MagicImageProcessor.ProcessImage(inPath, output, settings);
                output.Close();
            }
        }
    }
}
