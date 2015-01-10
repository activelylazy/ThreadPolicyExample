using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIThreadSample
{
    class ViewModel
    {
        private Model m_model = new Model();

        public ObservableCollection<string> Items { get; private set; }
        public ICommand FetchCommand { get; private set; }

        public ViewModel()
        {
            Items = new ObservableCollection<string>();
            FetchCommand = new FetchCommand(this);
        }

        public async void Fetch()
        {
            var items = await m_model.Fetch();
            foreach (var item in items)
                Items.Add(item);
        }
    }

    class FetchCommand : ICommand
    {
        private ViewModel m_viewModel;

        public FetchCommand(ViewModel viewModel)
        {
            m_viewModel = viewModel;
        }

        public void Execute(object param)
        {
            m_viewModel.Fetch();
        }

        public bool CanExecute(object param)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
