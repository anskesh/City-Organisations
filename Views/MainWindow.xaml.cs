using System.Windows;
using Prism.Regions;
using System;
using System.Data.SQLite;

namespace CityOrganisations.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Путь к файлу базы данных
            string databasePath = "C:\\fork\\City-Organisations\\Data\\organization_branch.db";

            // Создание базы данных и таблиц
            CreateDatabaseAndTables(databasePath);
        }

        private void CreateDatabaseAndTables(string databasePath)
        {
            // Создайте соединение с базой данных
            using (var connection = new SQLiteConnection($"Data Source={databasePath}; Version=3;"))
            {
                // Откройте соединение
                connection.Open();

                // Создайте команду для выполнения запросов
                using (var command = new SQLiteCommand(connection))
                {
                    // Создание таблицы "Organization"
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Organization (
                            OrgId INTEGER PRIMARY KEY,
                            OrgName TEXT NOT NULL
                        );
                    ";
                    command.ExecuteNonQuery();

                    // Создание таблицы "Branch"
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Branch (
                            BranchId INTEGER PRIMARY KEY,
                            OrgId INTEGER NOT NULL,
                            LegalAddress TEXT NOT NULL,
                            PhysicalAddress TEXT NOT NULL,
                            BranchDirector TEXT NOT NULL,
                            TaxId TEXT NOT NULL,
                            FOREIGN KEY(OrgId) REFERENCES Organization(OrgId)
                        );
                    ";
                    command.ExecuteNonQuery();

                    Console.WriteLine("База данных и таблицы успешно созданы.");
                }

                // Закройте соединение
                connection.Close();
            }
        }
    }
}