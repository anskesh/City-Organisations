using CityOrganisations.Models;
using CityOrganisations.Services.DataBase;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;


namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BaseEditableViewModel<BranchModel>
    {
        private List<PointModel> _pointsOnMap;

        public HomePageViewModel(IDatabaseService<BranchModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) :
            base(databaseService, dialogService, eventAggregator)
        {
            _pointsOnMap = new List<PointModel>();
        }

        protected override BranchModel CreateItemCopy(BranchModel item)
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveAdditionalCheck()
        {
            throw new NotImplementedException();
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

            PointModel pointToRemove = FindPointNearPosition(mousePosition);

            if (pointToRemove != null)
            {
                MenuItem removeItem = new MenuItem { Header = "Удалить" };
                removeItem.Click += (s, args) =>
                {
                    RemovePointFromCanvas(pointToRemove, mapCanvas);
                };

                contextMenu.Items.Add(removeItem);
            }

            contextMenu.IsOpen = true;

            contextMenu.PlacementTarget = mapCanvas;
            contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }

        private void AddPointToCanvas(Point position, Canvas mapCanvas)
        {

            Uri uri = new Uri("Images/Point.png", UriKind.Relative);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            Image pointImage = new Image
            {
                Source = bitmapImage,
                Width = 25,
                Height = 25
            };

            Canvas.SetLeft(pointImage, position.X - pointImage.Width / 2);
            Canvas.SetTop(pointImage, position.Y - pointImage.Height / 2);

            mapCanvas.Children.Add(pointImage);

            PointModel pointModel = new PointModel
            {
                Position = position,
                UIElement = pointImage
            };

            _pointsOnMap.Add(pointModel);
        }

        private void RemovePointFromCanvas(PointModel pointModel, Canvas mapCanvas)
        {
            mapCanvas.Children.Remove(pointModel.UIElement);

            _pointsOnMap.Remove(pointModel);
        }

        private PointModel FindPointNearPosition(Point position)
        {
            foreach (var point in _pointsOnMap)
            {
                double distance = Math.Sqrt(Math.Pow(point.Position.X - position.X, 2) + Math.Pow(point.Position.Y - position.Y, 2));

                if (distance <= 20)
                {
                    return point;
                }
            }

            return null;
        }
    }

    public class PointModel
    {
        public Point Position { get; set; }
        public UIElement UIElement { get; set; }
    }
}