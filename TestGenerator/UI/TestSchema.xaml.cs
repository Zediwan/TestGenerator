using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace TestGenerator.UI
{
    /// <summary>
    /// Interaction logic for TestSchema.xaml
    /// </summary>
    public partial class TestSchema
    {
        public TestSchema()
        {
            InitializeComponent();
        }

        public bool HasFileNaming => !string.IsNullOrEmpty(FilePrefix.Text) || !string.IsNullOrEmpty(FileSuffix.Text);

        public bool CheckSchema()
        {
            var canContinue = true;
            if (!HasFileNaming)
            {
                var result = MessageBox.Show(
                    "You haven't set any Prefix or Suffix for the Test File Names. Are you sure you want to continue?",
                    "Warning",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    canContinue = true;
            }

            return canContinue;
        }
    }
}
