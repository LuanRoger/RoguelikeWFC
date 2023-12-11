using SadConsole.UI.Controls;

namespace RoguelikeWFC.Components.Window;

/// <summary>
/// Popup window for selecting a directory and file name.
/// </summary>
/// <see cref="https://github.com/Thraka/SadConsole/blob/master/Samples/ThemeEditor/Windows/SelectEditorFilePopup.cs"/>
public class SelectDirectoryPopup : SadConsole.UI.Window
{
    private FileDirectoryListbox _directoryListBox;
    private TextBox _fileName;
    private Button _selectButton;
    private Button _cancelButton;

    public string currentFolder
    {
        get => _directoryListBox.CurrentFolder;
        set => _directoryListBox.CurrentFolder = value;
    }
    public bool allowCancel
    {
        set => _cancelButton.IsEnabled = value;
    }
    public string preferredExtensions
    {
        get => _directoryListBox.HighlightedExtentions;
        set => _directoryListBox.HighlightedExtentions = value;
    }
    public string selectButtonText 
    { 
        get => _selectButton.Text;
        set => _selectButton.Text = value;
    }

    public string? selectedFile { get; private set; }
    private readonly bool _skipFileExistCheck;
    
    private readonly string? _fileExtensionFilter;

    public SelectDirectoryPopup(string title, string? fileExtensionFilter, bool skipFileExistCheck = false)
        : base(55, 28)
    {
        _fileExtensionFilter = fileExtensionFilter;
        _skipFileExistCheck = skipFileExistCheck;
        
        Center();
        
        this.Print(1, 1, "Files");

        _directoryListBox = new(Width - 2, Height - 9)
        {
            Position = new(1, 3),
            HighlightedExtentions = fileExtensionFilter,
            FileFilter = '*' + fileExtensionFilter,
            OnlyRootAndSubDirs = true,
            HideNonFilterFiles = true,
            CurrentFolder = Environment.CurrentDirectory
        };
        _directoryListBox.SelectedItemChanged += DirectoryListBoxOnSelectedItemChanged;

        this.Print(1, 2, new string((char)196, _directoryListBox.Width));
        this.Print(1, _directoryListBox.Position.Y + _directoryListBox.Height, new string((char)196, _directoryListBox.Width));

        _fileName = new(_directoryListBox.Width)
        {
            Position = new(_directoryListBox.Bounds.X, _directoryListBox.Bounds.MaxExtentY + 3),
        };
        _fileName.TextChanged += FileNameOnTextChanged;
        this.Print(_fileName.Bounds.X, _fileName.Bounds.Y - 1, title);

        _selectButton = new(8)
        {
            Text = "Open",
            Position = new(Width - 10, Height - 2),
            IsEnabled = false
        };
        _selectButton.Click += SelectButtonOnClick;

        _cancelButton = new(8)
        {
            Text = "Cancel",
            Position = new(2, Height - 2)
        };
        _cancelButton.Click += CancelButtonOnClick;

        Controls.Add(_directoryListBox);
        Controls.Add(_fileName);
        Controls.Add(_selectButton);
        Controls.Add(_cancelButton);

        Title = "Select File";
    }

    public override void Show(bool modal)
    {
        selectedFile = "";
        base.Show(modal);
    }
    
    private void CancelButtonOnClick(object? sender, EventArgs e)
    {
        DialogResult = false;
        Hide();
    }
    
    private void SelectButtonOnClick(object? sender, EventArgs e)
    {
        if (_fileName.Text == string.Empty) return;
        
        selectedFile = Path.Combine(_directoryListBox.CurrentFolder, _fileName.Text);
        string extension = Path.GetExtension(selectedFile);
        if(string.IsNullOrWhiteSpace(extension))
            selectedFile += _fileExtensionFilter;
        
        if(!_skipFileExistCheck)
        {
            bool fileExist = File.Exists(selectedFile);
            if(!fileExist)
            {
                Message("File does not exist.", "OK");
                selectedFile = string.Empty;
                return;
            }
        }

        DialogResult = true;
        Hide();
    }
    
    private void DirectoryListBoxOnSelectedItemChanged(object? sender, ListBox.SelectedItemEventArgs e)
    {
        _fileName.Text = e.Item switch
        {
            FileInfo info => info.Name,
            FileDirectoryListbox.HighlightedExtFile file => file.Name,
            _ => ""
        };
    }
    
    private void FileNameOnTextChanged(object? sender, EventArgs e)
    {
        bool fileExists = true;
        if(!_skipFileExistCheck)
        {
            fileExists = File.Exists(Path.Combine(_directoryListBox.CurrentFolder, _fileName.Text));
        }
        _selectButton.IsEnabled = !string.IsNullOrWhiteSpace(_fileName.Text) && fileExists;
    }
}