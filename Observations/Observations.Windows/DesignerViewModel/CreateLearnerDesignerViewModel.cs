using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Observations.Entities;
using Observations.ViewModel;
using Observations.WindowsRT.Common;
using Observations.WindowsRT.Common.Converters;
using Observations.WindowsRT.Interfaces;
using Observations.WindowsRT.Messages;
using Observations.WindowsRT.Views;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Observations.WindowsRT.DesignerViewModel
{
    public class CreateLearnerDesignerViewModel : ViewModelBase, IViewModel
    {
        private readonly INavigationService _navigationService;

        private Learner learner;
        public Learner Learner
        {
            get
            {
                if (learner != null)
                    return learner;

                Learner l = new Learner();
                l.ImageLocation = new Uri("ms-appx:./Assets/userplus.png");

                return l;
            }
            set
            {
                if (value == learner)
                    return;
                learner = value;
                RaisePropertyChanged("Learner");
            }
        }

        public CreateLearnerDesignerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnLearnerReceived(LearnerSelectedArgs obj)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            Learner = ((Learner)e.Parameter);
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region RelayCommands
        private RelayCommand takePhoto;
        public RelayCommand TakePhoto
        {
            get
            {
                return takePhoto
                    ?? (takePhoto = new RelayCommand(async
                        () =>
                    {
                        try
                        {
                            CameraCaptureUI dialog = new CameraCaptureUI();
                            Size aspectRatio = new Size(7, 5);
                            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

                            StorageFile file;
                            ImageToByteArrayConverter imageConverter = new ImageToByteArrayConverter();

                            file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
                            if (file != null)
                            {
                                ParseFile pf = new ParseFile(file.Name, await imageConverter.GetByteArray(file));
                                Learner.ImageLocation = new Uri("file://" + file.Path);
                                Learner.Image = pf;
                            }
                            else
                            {
                                //rootPage.NotifyUser("No photo captured.", NotifyType.StatusMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            //rootPage.NotifyUser(ex.Message, NotifyType.ErrorMessage);
                        }
                    }));
            }
        }

        private RelayCommand addImage;

        /// <summary>
        /// Gets the AddImage command property to in turn runs the lambda method to browse and selected an image.
        /// </summary>
        public RelayCommand AddImage
        {
            get
            {
                return addImage
                    ?? (addImage = new RelayCommand(async
                                          () =>
                                          {
                                              FileOpenPicker openPicker = new FileOpenPicker();
                                              openPicker.ViewMode = PickerViewMode.Thumbnail;
                                              openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                                              openPicker.FileTypeFilter.Add(".jpg");
                                              openPicker.FileTypeFilter.Add(".jpeg");
                                              openPicker.FileTypeFilter.Add(".png");

                                              // Launch file open picker and caller app is suspended and may be terminated if required
                                              StorageFile file;
                                              file = await openPicker.PickSingleFileAsync();
                                              if (file != null)
                                              {
                                                  ImageToByteArrayConverter imageConverter = new ImageToByteArrayConverter();
                                                  ParseFile pf = new ParseFile(file.Name, await imageConverter.GetByteArray(file));
                                                  Learner.ImageLocation = new Uri("file://" + file.Path);
                                                  Learner.Image = pf;
                                              }
                                          }));
            }
        }

        private RelayCommand save;

        /// <summary>
        /// Gets the Save property that calls the save methods to store the learner.
        /// </summary>
        public RelayCommand Save
        {
            get
            {
                return save
                    ?? (save = new RelayCommand(async
                                          () =>
                                          {
                                              PupilViewModel pupilViewModel = new PupilViewModel();
                                              await pupilViewModel.SavePupil(Learner);

                                              _navigationService.Navigate(typeof(PupilListView));
                                          }));
            }
        }

        #endregion
    }
}
