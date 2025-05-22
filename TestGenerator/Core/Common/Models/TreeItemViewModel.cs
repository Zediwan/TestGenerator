using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace TestGenerator.Core.Common.Models;

public class TreeItemViewModel : INotifyPropertyChanged
{
    private bool? _isChecked = false;

    public string Name { get; set; }
    public object Tag { get; set; }
    public ObservableCollection<TreeItemViewModel> Children { get; set; } = [];

    public bool? IsChecked
    {
        get => _isChecked;
        set
        {
            if (_isChecked == value) return;

            _isChecked = value;
            OnPropertyChanged();

            // Cascade to children
            foreach (var child in Children)
                child.IsChecked = value;
        }
    }

    public string Icon => Tag switch
    {
        DirectoryInfo => "\uE188",
        FileInfo => "\uE132",
        ClassDeclarationSyntax => "\uE8A5",
        MethodDeclarationSyntax => "\uE945",
        ConstructorDeclarationSyntax => "\uE822",
        PropertyDeclarationSyntax => "\uE713",
        AccessorDeclarationSyntax { Keyword.Text: "get" } => "\uE8FB",
        AccessorDeclarationSyntax { Keyword.Text: "set" } => "\uE8AC",
        _ => ""
    };

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}