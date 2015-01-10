using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIThreadSample
{
    class ViewModel
    {
        private Model m_model = new Model();
        private bool m_isFetching;

        public ObservableCollection<string> Items { get; private set; }
        public ICommand FetchCommand { get; private set; }

        internal bool IsFetching 
        {
            [UIThreadPolicy]
            get { return m_isFetching; }
            [UIThreadPolicy]
            set 
            {
                m_isFetching = value;
                IsFetchingChanged(this, new EventArgs());
            }
        }
        internal event EventHandler IsFetchingChanged = delegate { };

        [UIThreadPolicy]
        public ViewModel()
        {
            Items = new ObservableCollection<string>();
            FetchCommand = new FetchCommand(this);
        }

        [UIThreadPolicy]
        public async void Fetch()
        {
            IsFetching = true;
            try
            {
                var items = await m_model.Fetch();
                foreach (var item in items)
                    Items.Add(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                IsFetching = false;
            }
        }
    }

    class FetchCommand : ICommand
    {
        private ViewModel m_viewModel;

        public FetchCommand(ViewModel viewModel)
        {
            m_viewModel = viewModel;
            m_viewModel.IsFetchingChanged += IsFetchingChanged;
        }

        public void Execute(object param)
        {
            m_viewModel.Fetch();
        }

        public bool CanExecute(object param)
        {
            return !m_viewModel.IsFetching;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        private void IsFetchingChanged(object sender, EventArgs e)
        {
            CanExecuteChanged(this, new EventArgs());
        }
    }
}
