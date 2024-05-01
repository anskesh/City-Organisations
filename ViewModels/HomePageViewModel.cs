using CityOrganisations.Models;
using CityOrganisations.Services.DataBase;
using Prism.Events;
using Prism.Services.Dialogs;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;
using System;

namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BaseEditableViewModel<BranchModel>
    {
        public HomePageViewModel(IDatabaseService<BranchModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) : 
            base(databaseService, dialogService, eventAggregator) {}
        
        protected override BranchModel CreateItemCopy(BranchModel item)
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnSaveAdditionalCheck()
        {
            throw new System.NotImplementedException();
        }

        public void HandleMouseRightButton(MouseButtonEventArgs e, Canvas mapCanvas)
        {
            Point mousePosition = e.GetPosition(mapCanvas);

            ContextMenu contextMenu = new ContextMenu();

            MenuItem addItem = new MenuItem { Header = "Добавить" };
            addItem.Click += (s, args) =>
            {
                AddPointToCanvas(mousePosition, mapCanvas);
            };

            contextMenu.Items.Add(addItem);

            contextMenu.IsOpen = true;

            contextMenu.PlacementTarget = mapCanvas;
            contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }


        public void AddPointToCanvas(Point position, Canvas mapCanvas)
        {
            Image pointImage = new Image
            {
                Source = new BitmapImage(new Uri(@"C:\fork\City-Organisations\Images\Point.png")),
                Width = 20,
                Height = 20
            };

            Canvas.SetLeft(pointImage, position.X - pointImage.Width / 2);
            Canvas.SetTop(pointImage, position.Y - pointImage.Height / 2);

            mapCanvas.Children.Add(pointImage);
        }
    }
}