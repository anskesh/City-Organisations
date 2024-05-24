using CityOrganisations.Models;
using CityOrganisations.Services.DataBase;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Commands;

namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BaseEditableViewModel<BranchModel>
    {
        public float ImageSize
        {
            get => _imageSize;
            set => SetProperty(ref _imageSize, value);
        }
        
        public ObservableCollection<PointModel> Points { get; } = new();
        public ICommand MouseWheelCommand { get; }
        public ICommand ScrollViewerLoadedCommand { get; }
        public ICommand ScrollViewerSizeChangedCommand { get; }
        public ICommand MouseRightButtonCommand { get; }

        public ScaleTransform Scale => _scale;

        private float _imageSize = 25;
    
        private ScaleTransform _scale = new();
        private double _maxScale = 5;
        private double _minScale = 1f;

        private ScrollViewer _scrollViewer;
        private Dictionary<BranchModel, PointModel> _points = new();

        public HomePageViewModel(IDatabaseService<BranchModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) :
            base(databaseService, dialogService, eventAggregator)
        {
            MouseWheelCommand = new DelegateCommand<MouseWheelEventArgs>(OnMouseWheel);
            ScrollViewerLoadedCommand = new DelegateCommand<object>(OnScrollViewLoaded);
            ScrollViewerSizeChangedCommand = new DelegateCommand<object>(OnSizeChanged);
            MouseRightButtonCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseRightClick);
            
            InitializePoints();
            
            Items.CollectionChanged += ItemsOnCollectionChanged;
        }

        private void ItemsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is BranchModel model)
                        RemovePoint(model);
                }
            }
        }

        private void InitializePoints()
        {
            foreach (var branchModel in Items)
            {
                if (branchModel.XPos != 0 && branchModel.YPos != 0)
                    RestorePoint(branchModel);
            }
        }
        
        protected override BranchModel CreateItemCopy(BranchModel item)
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveAdditionalCheck()
        {
            throw new NotImplementedException();
        }

        private void OnScrollViewLoaded(object sender)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                _scrollViewer = scrollViewer;
                UpdateScaleValues();
            }
        }

        private void OnSizeChanged(object sender)
        {
            UpdateScaleValues();
        }

        private void UpdateScaleValues()
        {
            if (_scrollViewer.Content is Canvas canvas)
            {
                double scale = _scrollViewer.ActualHeight / canvas.ActualHeight;
                _minScale = scale;
                
                ChangeScale(Scale.ScaleX);
            }
        }

        private void ChangeScale(double newScale)
        {
            if (newScale > _maxScale)
                newScale = _maxScale;
            
            if (newScale < _minScale) 
                newScale = _minScale;
            
            Scale.ScaleX = newScale;
            Scale.ScaleY = newScale;
        }
        
        private void OnMouseWheel(MouseWheelEventArgs e)
        {
            float scaleRate = 1.1f;

            if (e.Delta > 0)
                ChangeScale(Scale.ScaleX * scaleRate);
            else
                ChangeScale(Scale.ScaleX / scaleRate);
            
            e.Handled = true;
        }
        
        private void OnMouseRightClick(MouseButtonEventArgs e)
        {
            PointModel position = CalculatePosition(e.GetPosition(e.Source as IInputElement));
            ContextMenu contextMenu = new ContextMenu();

            if (!_points.ContainsKey(Items[SelectedIndex]))
            {
                MenuItem addItem = new MenuItem { Header = "Добавить" };
                addItem.Click += (_, _) =>
                {
                    AddPoint(position);
                };

                contextMenu.Items.Add(addItem);
            }
            
            PointModel pointToRemove = FindPointNearPosition(position);

            if (pointToRemove != null)
            {
                MenuItem removeItem = new MenuItem { Header = "Удалить" };
                removeItem.Click += (_, _) =>
                {
                    RemovePoint(pointToRemove);
                };

                contextMenu.Items.Add(removeItem);
            }

            if (contextMenu.Items.Count > 0)
                contextMenu.IsOpen = true;

            contextMenu.PlacementTarget = _scrollViewer;
            contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }

        private PointModel CalculatePosition(Point mousePosition)
        {
            float finalX = (float) (mousePosition.X );
            float finalY = (float) (mousePosition.Y );

            return new PointModel(finalX, finalY, 1);
        }
        
        private PointModel FindPointNearPosition(PointModel position)
        {
            foreach (var point in Points)
            {
                double distance = Math.Sqrt(Math.Pow(point.X - position.X, 2) + Math.Pow(point.Y - position.Y, 2));

                if (distance <= _imageSize)
                    return point;
            }

            return null;
        }
        
        private void AddPoint(PointModel pointModel)
        {
            int selectedIndex = SelectedIndex;
            
            BranchModel branchModel = Items[selectedIndex];
            branchModel.XPos = pointModel.X;
            branchModel.YPos = pointModel.Y;

            DatabaseService.Update(branchModel, branchModel);
            SelectedIndex = selectedIndex; // востанавливаем индекс, так как он сбросился после обновления элемента
            
            AddPoint(pointModel, branchModel);
        }
        
        private void RestorePoint(BranchModel model)
        {
            AddPoint(new PointModel(model.XPos, model.YPos, 1), model);
        }

        private void AddPoint(PointModel pointModel, BranchModel branchModel)
        {
            _points.Add(branchModel, pointModel);
            Points.Add(pointModel);
        }

        private void RemovePoint(BranchModel branchModel)
        {
            RemovePoint(_points[branchModel], branchModel);
        }
        
        private void RemovePoint(PointModel pointModel)
        {
            BranchModel branchModel = _points.First(x => x.Value == pointModel).Key;
            branchModel.XPos = 0;
            branchModel.YPos = 0;

            DatabaseService.Update(branchModel, branchModel);
            
            RemovePoint(pointModel, branchModel);
        }

        private void RemovePoint(PointModel pointModel, BranchModel branchModel)
        {
            _points.Remove(branchModel);
            Points.Remove(pointModel);
        }
    }
}