using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WPF_OPENCV_Morphology_2025.Models;

namespace WPF_OPENCV_Morphology_2025.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public MainViewModel()
        {
            ItemsSourceImage = Enum.GetValues(typeof(eImage)).Cast<eImage>().ToList();
            ItemsSourceMode = Enum.GetValues(typeof(MorphTypes)).Cast<MorphTypes>().ToList();
            ItemsSourceShape = Enum.GetValues(typeof(MorphShapes)).Cast<MorphShapes>().ToList();
            iteration = 1;
            size = new Size(3, 3);
            mophology = new ClassMorphology();
            CommandOpenFolder = new RelayCommand(() => { OpenFolder(); });
            CommandRunProgram = new RelayCommand(() => { RunProgram(); });
        }
        #region Defines
        public enum eImage { InputImage, OutputImage}
        #endregion

        #region Commands
        public ICommand  CommandOpenFolder { get; set; }
        public ICommand  CommandRunProgram { get; set; }
        #endregion

        #region Propeties
        public List<MorphTypes> ItemsSourceMode { get; set; }
        public List<MorphShapes> ItemsSourceShape { get; set; }
        public List<eImage> ItemsSourceImage { get; set; }

        public int SizeX
        {
            get { return size.Width; }
            set { size.Width = value; OnPropertyChanged(); }
        }

        public int SizeY
        {
            get { return size.Height; }
            set { size.Height = value; OnPropertyChanged(); }
        }

        public int Iteration
        {
            get { return iteration; }
            set { iteration = value; OnPropertyChanged(); }
        }

        public MorphShapes SelectedItemShape
        {
            get { return selectedItemShape; }
            set { selectedItemShape = value; OnPropertyChanged(); }
        }
        

        public MorphTypes SelectedItemMode
        {
            get { return selectedItemMode; }
            set { selectedItemMode = value; OnPropertyChanged(); }
        }

        public eImage SelectedItemImage
        {
            get { return selectedItemImage; }
            set { selectedItemImage = value;
                ShowImage(selectedItemImage);
                OnPropertyChanged(); }
        }

        public ImageSource SourceImageMain { get; set; }

        #endregion

        #region Methods
        void OpenFolder()
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PNG|*.png| JPG| *.jpg| BMP|*.bmp| All|*.*";
            if (dialog.ShowDialog()==true)
            {
                mophology.InputImage = new Mat(dialog.FileName, ImreadModes.Grayscale);
                ShowImage(mophology.InputImage);
            }
        }
        void RunProgram() 
        {
            if (mophology.InputImage == null) return;
            mophology.OutputImage = mophology.InputImage.Clone();
            Mat element = Cv2.GetStructuringElement(SelectedItemShape, size);

            mophology.Run(element, selectedItemMode, iteration);
            SelectedItemImage = eImage.OutputImage;
        }
        void ShowImage(Mat img)
        {
            if (img == null) return;
            App.Current.Dispatcher.Invoke(() => {
                SourceImageMain = img.ToWriteableBitmap();
                OnPropertyChanged(nameof(SourceImageMain));
            });
        }
        void ShowImage(eImage eImage)
        {
            switch (eImage)
            {
                case eImage.InputImage:
                    ShowImage(mophology.InputImage);
                    break;
                case eImage.OutputImage:
                    ShowImage(mophology.OutputImage);
                    break;
            }
        }
        #endregion

        #region Feilds
        private eImage selectedItemImage;
        private MorphShapes selectedItemShape;
        private MorphTypes selectedItemMode;
        private Size size;
        private int iteration;
        private ClassMorphology mophology;
        #endregion
    }
}
