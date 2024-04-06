using System.Collections.ObjectModel;

namespace CityOrganisations.Models
{
    public class BranchModel
    {
        public ObservableCollection<InformationItem> Information { get; set; }
        public string Name { get; set; }

        public BranchModel(string name)
        {
            Name = name;

            Information = new ObservableCollection<InformationItem>()
            {
                new() {LabelText = "Название организации:", TextBoxText = "Череповецкий государственный университет"},
                new() {LabelText = "Юридический адрес:", TextBoxText = "Советский проспект, д.8"},
                new() {LabelText = "Физический адрес:", TextBoxText = "г. Череповец, д. 10"},
                new() {LabelText = "Директор филиала:", TextBoxText = "Целикова"},
                new() {LabelText = "ИНН:", TextBoxText = "23982938192819"},
            };
        }
    }
}