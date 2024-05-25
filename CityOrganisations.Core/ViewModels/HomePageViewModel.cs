using Core.Models;
using Core.DataBase.Services;
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
using CityOrganisations.Dialogs;
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
        public ICommand MouseLeftButtonCommand { get; }

        public ScaleTransform Scale => _scale;
        
        public bool IsOpenPopup
        {
            get => _isOpenPopup;
            set => SetProperty(ref _isOpenPopup, value);
        }

        public ICommand CloseDialogCommand { get; }
        public ICommand CanvasLoadedCommand { get; }

        private bool _isOpenPopup;

        private float _imageSize = 25;
    
        private ScaleTransform _scale = new();
        private double _maxScale = 5;
        private double _minScale = 1f;

        private ScrollViewer _scrollViewer;
        private Canvas _canvas;
        private Dictionary<BranchModel, PointModel> _points = new();

        public HomePageViewModel(IDatabaseService<BranchModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) :
            base(databaseService, dialogService, eventAggregator)
        {
            MouseWheelCommand = new DelegateCommand<MouseWheelEventArgs>(OnMouseWheel);
            ScrollViewerLoadedCommand = new DelegateCommand<object>(OnScrollViewLoaded);
            ScrollViewerSizeChangedCommand = new DelegateCommand<object>(OnSizeChanged);

            CanvasLoadedCommand = new DelegateCommand<object>(OnCanvasLoaded);
            
            MouseRightButtonCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseRightClick);
            MouseLeftButtonCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseLeftClick);

            CloseDialogCommand = new DelegateCommand(CloseDialog);
            
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
                _scrollViewer = scrollViewer;
        }

        private void OnCanvasLoaded(object sender)
        {
            if (sender is Canvas canvas)
            {
                _canvas = canvas;
                UpdateScaleValues();
            }
        }

        private void OnSizeChanged(object sender)
        {
            UpdateScaleValues();
        }

        private void UpdateScaleValues()
        {
            double scale = _scrollViewer.ActualHeight / _canvas.ActualHeight;
            _minScale = scale;
            
            ChangeScale(Scale.ScaleX);
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


        private void OnMouseLeftClick(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(e.Source as IInputElement);
            PointModel selectedPoint = FindPointNearPosition(position);
            
            if (selectedPoint != null)
            {
                SelectedItem = _points.First(x => x.Value == selectedPoint).Key;
                IsOpenPopup = true;
            }
            else
            {
                IsOpenPopup = false;
            }
        }

        private void CloseDialog()
        {
            IsOpenPopup = false;
        }
        
        private void OnMouseRightClick(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(e.Source as IInputElement);
            ContextMenu contextMenu = new ContextMenu();

            if (!_points.ContainsKey(Items[SelectedIndex]))
            {
                MenuItem addItem = new MenuItem { Header = "Добавить" };
                addItem.Click += (_, _) =>
                {
                    AddPoint(CalculatePosition(position));
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
            float finalX = (float) (mousePosition.X - _imageSize / 2);
            float finalY = (float) (mousePosition.Y - _imageSize);

            return new PointModel(finalX, finalY);
        }
        
        private PointModel FindPointNearPosition(Point position)
        {
            foreach (var point in Points)
            {
                double distance = Math.Pow(point.X - position.X, 2) + Math.Pow(point.Y - position.Y, 2);

                if (distance <= _imageSize * _imageSize)
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
            AddPoint(new PointModel(model.XPos, model.YPos), model);
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