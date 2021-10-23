using System.Collections.ObjectModel;
using System.Windows.Input;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.WPF.ViewModels
{
    public class MainWindowViewModel : TitledViewModel
    {
        private readonly IRepository<DataSource> _DataSources;

        public MainWindowViewModel(IRepository<DataSource> DataSources)
        {
            _DataSources = DataSources;
            Title = "Главное окно программы";
        }

        public ObservableCollection<DataSource> DataSources { get; } = new();

        #region Command LoadDataSourcesCommand - Загрузить данные по источникам

        /// <summary>Загрузить данные по источникам</summary>
        private LambdaCommand _LoadDataSourcesCommand;

        /// <summary>Загрузить данные по источникам</summary>
        public ICommand LoadDataSourcesCommand => _LoadDataSourcesCommand ??= new(OnLoadDataSourcesCommandExecuted);

        /// <summary>Логика выполнения - Загрузить данные по источникам</summary>
        private async void OnLoadDataSourcesCommandExecuted(object p)
        {
            DataSources.Clear();
            foreach (var source in await _DataSources.GetAll()) 
                DataSources.Add(source);
        }

        #endregion
    }
}
