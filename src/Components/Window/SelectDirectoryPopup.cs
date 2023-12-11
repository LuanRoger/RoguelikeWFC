using SadConsole.UI.Controls;

namespace RoguelikeWFC.Components.Window;

/// <summary>
/// Popup window for selecting a directory and file name.
/// </summary>
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

    public string selectedFile { get; private set; }

    public bool skipFileExistCheck { get; set; }
    
    public readonly string? fileExtensionFilter;

    public string selectButtonText { get => _selectButton.Text;
        set => _selectButton.Text = value;
    }

    public SelectDirectoryPopup(string title, string? fileExtensionFilter)
        : base(55, 28)
    {
        Center();
        
        this.Print(1, 1, "Files");

        _directoryListBox = new(Width - 2, Height - 9)
        {
            Position = new(1, 3),
        };
        this.fileExtensionFilter = fileExtensionFilter;

        _directoryListBox.HighlightedExtentions = fileExtensionFilter;
        _directoryListBox.FileFilter = '*' + fileExtensionFilter;
        _directoryListBox.SelectedItemChanged += _directoryListBox_SelectedItemChanged;
        _directoryListBox.OnlyRootAndSubDirs = true;
        _directoryListBox.HideNonFilterFiles = true;
        _directoryListBox.CurrentFolder = Environment.CurrentDirectory;

        this.Print(1, 2, new string((char)196, _directoryListBox.Width));
        this.Print(1, _directoryListBox.Position.Y + _directoryListBox.Height, new string((char)196, _directoryListBox.Width));

        _fileName = new(_directoryListBox.Width)
        {
            Position = new(_directoryListBox.Bounds.X, _directoryListBox.Bounds.MaxExtentY + 3),
        };
        _fileName.TextChanged += _fileName_TextChanged;
        this.Print(_fileName.Bounds.X, _fileName.Bounds.Y - 1, title);

        _selectButton = new(8)
        {
            Text = "Open",
            Position = new(Width - 10, Height - 2),
            IsEnabled = false
        };
        _selectButton.Click += _selectButton_Action;

        _cancelButton = new(8)
        {
            Text = "Cancel",
            Position = new(2, Height - 2)
        };
        _cancelButton.Click += _cancelButton_Action;

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

    private void _cancelButton_Action(object sender, EventArgs e)
    {
        DialogResult = false;
        Hide();
    }

    private void _selectButton_Action(object sender, EventArgs e)
    {
        if (_fileName.Text == string.Empty) return;
        
        selectedFile = Path.Combine(_directoryListBox.CurrentFolder, _fileName.Text);
        string extension = Path.GetExtension(selectedFile);
        if(string.IsNullOrWhiteSpace(extension))
            selectedFile += fileExtensionFilter;
        
        if(!skipFileExistCheck)
        {
            bool fileExist = File.Exists(selectedFile);
            if(!fileExist)
            {
                selectedFile = string.Empty;
                return;
            }
        }

        DialogResult = true;
        Hide();
    }

    private void _directoryListBox_SelectedItemChanged(object sender, ListBox.SelectedItemEventArgs e)
    {
        if (e.Item is FileInfo)
            _fileName.Text = ((FileInfo)e.Item).Name;
        else if (e.Item is FileDirectoryListbox.HighlightedExtFile)
            _fileName.Text = ((FileDirectoryListbox.HighlightedExtFile)e.Item).Name;
        else
            _fileName.Text = "";
    }

    private void _fileName_TextChanged(object sender, EventArgs e)
    {
        _selectButton.IsEnabled = _fileName.Text != "" && (skipFileExistCheck || File.Exists(Path.Combine(_directoryListBox.CurrentFolder, _fileName.Text)));
    }
}