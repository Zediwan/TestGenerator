using TestGenerator.UI.Helpers;

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
                canContinue = canContinue && MessageBoxHelper.ConfirmFileNaming();

            return canContinue;
        }
    }
}
