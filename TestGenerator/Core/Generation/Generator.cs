using System.IO;
using System.Windows;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;
using MessageBox = System.Windows.MessageBox;

namespace TestGenerator.Core.Generation;

public class Generator(string pathToTestRootFolder, string pathToSourceRootFolder)
{
    #region Generators

    /// <summary>
    /// The <see cref="FileGenerator"/> to be used for generating the test files.
    /// </summary>
    public FileGenerator? FileGenerator;

    #endregion

    /// <summary>
    /// Gets the path to the root folder of the source code.
    /// </summary>
    private string PathToSourceRootFolder { get; } = pathToSourceRootFolder;

    /// <summary>
    /// Gets the path to the root folder of the test code.
    /// </summary>
    private string PathToTestRootFolder { get; } = pathToTestRootFolder;

    /// <summary>
    /// Generates the test files for the given items.
    /// </summary>
    /// <param name="items">A list of <see cref="TreeItemViewModel"/> items to generate tests for.</param>
    public void Generate(List<TreeItemViewModel> items)
    {
        foreach (var treeItemViewModel in items) Generate(treeItemViewModel);
    }

    /// <summary>
    /// Generates test files for the specified <see cref="TreeItemViewModel"/> item and its children.
    /// </summary>
    /// <param name="item">The <see cref="TreeItemViewModel"/> to process.</param>
    /// <exception cref="InvalidOperationException">Thrown if <see cref="FileGenerator"/> is not initialized.</exception>
    private void Generate(TreeItemViewModel item)
    {
        var obj = item.Tag;
        switch (obj)
        {
            case DirectoryInfo directoryInfo:
                foreach (var child in item.Children) Generate(child);
                break;
            case ParsedFile parsedFile:
                if (FileGenerator == null)
                    throw new InvalidOperationException("FileGenerator is not initialized.");
                // TODO: make this test earlier (maybe when the generate button is pressed check what types of items are selected and then check the generators
                if (string.IsNullOrEmpty(FileGenerator.Prefix) && string.IsNullOrEmpty(FileGenerator.Suffix))
                {
                    // TODO: improve all message boxes that have been used so far
                    var result = MessageBox.Show(
                        "You haven't set any Prefix or Suffix for the Test File Names. Are you sure you want to continue?",
                        "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.No) return;
                }

                FileGenerator.Create(parsedFile);
                break;
            case MethodDeclarationSyntax methodSyntax:
                throw new NotImplementedException();
                break;
            case ClassDeclarationSyntax classSyntax:
                throw new NotImplementedException();
                break;
            case ConstructorDeclarationSyntax constructorSyntax:
                throw new NotImplementedException();
                break;
            case PropertyDeclarationSyntax propertySyntax:
                throw new NotImplementedException();
                break;
        }
    }
}