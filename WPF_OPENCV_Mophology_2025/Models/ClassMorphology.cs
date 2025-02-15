using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_OPENCV_Morphology_2025.Models
{
    public class ClassMorphology
    {
        public ClassMorphology()
        {

        }
        public Mat InputImage { get; set; }
        public Mat OutputImage { get; set; }
        public void Run(Mat element, MorphTypes type, int iteration = 1)
        {
            switch (type)
            {
                case MorphTypes.Erode:
                    Cv2.Erode(InputImage, OutputImage, element, null, iteration);
                    break;
                case MorphTypes.Dilate:
                    Cv2.Dilate(InputImage, OutputImage, element, null, iteration);
                    break;
                case MorphTypes.Open:
                case MorphTypes.Close:
                case MorphTypes.Gradient:
                case MorphTypes.TopHat:
                case MorphTypes.BlackHat:
                case MorphTypes.HitMiss:
                    Cv2.MorphologyEx(InputImage, OutputImage, type, element, null, iteration);
                    break;
            }
        }
    }
}
