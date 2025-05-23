using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Linq;

namespace TestGenerator.Core.Common.Models;

public class TreeItemViewModel : INotifyPropertyChanged
{
    private bool _isChecked;

    public string Name { get; set; }
    public object Tag { get; set; }
    public TreeItemViewModel? Parent { get; set; }
    public ObservableCollection<TreeItemViewModel> Children { get; set; } = [];
    private bool _isInternalChange = false;
    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            if (_isChecked == value) return;

            _isChecked = value;
            OnPropertyChanged();

            // Only propagate to children if this is a direct user action (not from a child)
            if (!_isInternalChange)
            {
                foreach (var child in Children)
                {
                    child.IsChecked = value;
                }
            }

            // Upward: only update parent, do not propagate to siblings
            if (Parent == null) return;

            Parent._isInternalChange = true;
            if (value)
            {
                // If all siblings are checked, check the parent
                if (Parent.Children.All(c => c.IsChecked))
                    Parent.IsChecked = true;
            }
            else
            {
                // Always uncheck the parent if any child is unchecked
                if (Parent.IsChecked)
                    Parent.IsChecked = false;
            }

            Parent._isInternalChange = false;
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
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public bool SelfOrAnyChildrenSelected()
    {
        return IsChecked || Children.Any(child => child.SelfOrAnyChildrenSelected());
    }
}
