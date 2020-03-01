using FFImageLoading;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.IO;
using Xamarin.Forms;

namespace FFImageCleanCache.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private byte[] imgArray;

        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public DelegateCommand ImgBtn1Command { get; set; }
        public DelegateCommand ImgBtn2Command { get; set; }
        public DelegateCommand ImgBtnStrmCommand { get; set; }
        public DelegateCommand ImgBtnRsrcCommand { get; set; }

        public MainViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Clear Cache";

            ImageSource = "super_do_povo";

            ImgBtn1Command = new DelegateCommand(ImgBtn1Clicked);
            ImgBtn2Command = new DelegateCommand(ImgBtn2Clicked);
            ImgBtnStrmCommand = new DelegateCommand(ImgBtnStrmClicked);
            ImgBtnRsrcCommand = new DelegateCommand(ImgBtnRsrcClicked);
        }

        private void ImgBtn1Clicked()
        {
            ImageSource = "http://s2.glbimg.com/7-hknclnun_oevh51dxuplLsLdg=/e.glbimg.com/og/ed/f/original/2015/09/28/supermercado.jpg";
        }

        private void ImgBtn2Clicked()
        {
            ImageSource = "https://i.ytimg.com/vi/2x9c-DPJqus/maxresdefault.jpg";
        }

        private void ImgBtnStrmClicked()
        {
            ImageSource = "super_do_povo";
        }

        private async void ImgBtnRsrcClicked()
        {
            MediaFile file = null;

            var option = await _pageDialogService.DisplayActionSheetAsync(null, "Cancelar", null, new[] { "Galeria", "Câmera" });

            if (option.Equals("Câmera"))
            {
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

                if (cameraStatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await _pageDialogService.DisplayAlertAsync("Permissão Câmera", "Super Barato precisa de acesso à câmera", "OK", "Cancelar");
                    }

                    var cameraRequest = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

                    cameraStatus = cameraRequest[Permission.Camera];
                }

                if (cameraStatus == PermissionStatus.Granted)
                {
                    file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        AllowCropping = true,
                        SaveToAlbum = false,
                        PhotoSize = PhotoSize.Medium,
                        DefaultCamera = CameraDevice.Front
                    });
                }
            }

            if (option.Equals("Galeria"))
            {
                var photoStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

                if (photoStatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos))
                    {
                        await _pageDialogService.DisplayAlertAsync("Permissão Galeria", "Super Barato precisa de acesso à galeria", "OK", "Cancelar");
                    }

                    var cameraRequest = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);

                    photoStatus = cameraRequest[Permission.Photos];
                }

                if (photoStatus == PermissionStatus.Granted)
                {
                    file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium
                    });
                }
            }

            if (file == null)
                return;

            await CachedImage.InvalidateCache(ImageSource, CacheType.All, false);

            ImageSource = ImageSource.FromFile(file.Path);

            var stream = file.GetStreamWithImageRotatedForExternalStorage();

            imgArray = stream.ToByteArray();


            // The code bellow causes the error.

            //ImageSource = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStreamWithImageRotatedForExternalStorage();
            //    file.Dispose();

            //    imgArray = stream.ToByteArray();

            //    return stream;
            //});
        }
    }
}
