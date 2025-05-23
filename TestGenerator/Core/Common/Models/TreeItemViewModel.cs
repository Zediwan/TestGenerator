using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

/// <summary>
/// Represents a node in a hierarchical tree structure for source or test items,
/// supporting selection, parent/child relationships, and property change notifications.
/// </summary>
public class TreeItemViewModel(string name, object tag) : INotifyPropertyChanged
{
    private bool _isChecked;
    private bool _isInternalChange;

    /// <summary>
    /// Gets or sets the display name of the tree item.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the tag object associated with this item (e.g., DirectoryInfo, FileInfo, SyntaxNode).
    /// </summary>
    public object Tag { get; set; } = tag;

    /// <summary>
    /// Gets or sets the parent <see cref="TreeItemViewModel"/> of this item.
    /// </summary>
    public TreeItemViewModel? Parent { get; set; }

    /// <summary>
    /// Gets or sets the collection of child <see cref="TreeItemViewModel"/> items.
    /// </summary>
    public ObservableCollection<TreeItemViewModel> Children { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether this item is checked/selected.
    /// Setting this property propagates the value to children and updates the parent as needed.
    /// </summary>
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
                foreach (var child in Children)
                    child.IsChecked = value;

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

    /// <summary>
    /// Gets the icon string representing the type of the item, based on its <see cref="Tag"/>.
    /// </summary>
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

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event for the specified property.
    /// </summary>
    /// <param name="name">The name of the property that changed. This is optional and will be automatically provided by the compiler if omitted.</param>
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    /// <summary>
    /// Determines whether this item or any of its children are selected (checked).
    /// </summary>
    /// <returns><c>true</c> if this item or any child is checked; otherwise, <c>false</c>.</returns>
    public bool SelfOrAnyChildrenSelected()
    {
        return IsChecked || Children.Any(child => child.SelfOrAnyChildrenSelected());
    }
}