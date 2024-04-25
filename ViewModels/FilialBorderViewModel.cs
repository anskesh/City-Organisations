using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using CityOrganisations.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace CityOrganisations.ViewModels
{
    public class FilialBorderViewModel : BindableBase
    {
        private IEnumerable<string> _allBranches;

        public ObservableCollection<OrganizationModel> Organizations { get; set; }

        public FilialBorderViewModel()
        {
            Organizations = new ObservableCollection<OrganizationModel>();
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            // Путь к файлу базы данных
            string databasePath = "C:\\fork\\City-Organisations\\Data\\organization_branch.db";

            // Создание соединения с базой данных
            using (var connection = new SQLiteConnection($"Data Source={databasePath}; Version=3;"))
            {
                // Открытие соединения
                connection.Open();

                // Запрос для объединения таблиц Organization и Branch
                string query = @"
                    SELECT o.OrgId, o.OrgName, b.BranchId, b.PhysicalAddress, b.LegalAddress, b.BranchDirector, b.TaxId
                    FROM Organization o
                    JOIN Branch b ON o.OrgId = b.OrgId;
                ";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // Словарь для отслеживания организаций по их идентификатору
                        var orgDict = new Dictionary<int, OrganizationModel>();

                        while (reader.Read())
                        {
                            int orgId = reader.GetInt32(0);
                            string orgName = reader.GetString(1);
                            int branchId = reader.GetInt32(2);
                            string physicalAddress = reader.GetString(3);
                            string legalAddress = reader.GetString(4);
                            string branchDirector = reader.GetString(5);
                            string taxId = reader.GetString(6);

                            // Находим или создаем организацию
                            if (!orgDict.TryGetValue(orgId, out OrganizationModel orgModel))
                            {
                                orgModel = new OrganizationModel(orgName);
                                orgDict[orgId] = orgModel;
                                Organizations.Add(orgModel);
                            }

                            // Создаем экземпляр BranchModel и добавляем его в список организации
                            var branch = new BranchModel($"Branch {branchId}");
                            branch.Information.Add(new InformationItem { LabelText = "Физический адрес:", TextBoxText = physicalAddress });
                            branch.Information.Add(new InformationItem { LabelText = "Юридический адрес:", TextBoxText = legalAddress });
                            branch.Information.Add(new InformationItem { LabelText = "Директор филиала:", TextBoxText = branchDirector });
                            branch.Information.Add(new InformationItem { LabelText = "ИНН:", TextBoxText = taxId });

                            orgModel.Branches.Add(branch);
                        }
                    }
                }

                // Закрываем соединение
                connection.Close();
            }
        }

        // Свойство для получения данных в виде "Наименование организации, адрес филиала"
        public IEnumerable<string> AllBranches
        {
            get
            {
                var result = new List<string>();

                foreach (var organization in Organizations)
                {
                    foreach (var branch in organization.Branches)
                    {
                        // Добавляем строку в формате "Наименование организации, адрес филиала" в список
                        result.Add($"{organization.Name}, {branch.Information.FirstOrDefault(info => info.LabelText == "Физический адрес:").TextBoxText}");
                    }
                }
                return result;
            }
        }

        // Обработка нажатия всплывающего окна
        private DelegateCommand _openFilterCommand;
        public DelegateCommand OpenFilterCommand => _openFilterCommand ??= new DelegateCommand(ExecuteOpenFilterCommand);

        private void ExecuteOpenFilterCommand()
        {
            IsFilterPopupOpen = true;
        }

        private DelegateCommand _applyFilterCommand;
        public DelegateCommand ApplyFilterCommand => _applyFilterCommand ??= new DelegateCommand(ExecuteApplyFilterCommand);

        private void ExecuteApplyFilterCommand()
        {
            IsFilterPopupOpen = false;
        }

        private bool _isFilterPopupOpen;
        public bool IsFilterPopupOpen
        {
            get => _isFilterPopupOpen;
            set => SetProperty(ref _isFilterPopupOpen, value);
        }
    }
}