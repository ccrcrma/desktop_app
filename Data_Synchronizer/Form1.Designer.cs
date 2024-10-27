using Data_Synchronizer.Http;
using Data_Synchronizer.Services;

namespace Data_Synchronizer
{
    public partial class Form1 : Form
    {
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private DataGridView dataGridView;
        private Button syncBtn;
        private Button settingBtn;
        private Button locationBtn;
        private Button customerBtn;
        private CustomerService customerService;

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await FetchDataAsync();
        }

        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            dataGridView = new DataGridView();
            syncBtn = new Button();
            settingBtn = new Button();
            locationBtn = new Button();
            customerBtn = new Button();
            customerService = new CustomerService();

            SuspendLayout();
            // 
            // panel1 - Top Navigation
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Dock = DockStyle.Top;
            panel1.Height = 70;
            panel1.Padding = new Padding(10);
            panel1.Controls.Add(new Label
            {
                Text = "Data Synchronizer",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            });

            // 
            // panel2 - Side Menu
            // 
            panel2.BackColor = Color.LightSlateGray;
            panel2.Dock = DockStyle.Left;
            panel2.Width = 200;
            panel2.Padding = new Padding(10);

            syncBtn.Text = "Sync Data";
            syncBtn.Dock = DockStyle.Top;
            syncBtn.Height = 40;
            syncBtn.Click += async (sender, args) =>
            {
                await Task.Delay(200);
                using var customerService = new CustomerService();
                if(!await customerService.SyncAsync())
                    MessageBox.Show("Sync Failed");
                
                MessageBox.Show("Sync successfully");
            };

            customerBtn.Text = "Customers";
            customerBtn.Dock = DockStyle.Top;
            customerBtn.Height = 40;
            customerBtn.Click += async (sender, args) => await FetchDataAsync();

            locationBtn.Text = "Locations";
            locationBtn.Dock = DockStyle.Top;
            locationBtn.Height = 40;
            locationBtn.Click += async (sender, args) =>
            {
                using var locationService = new LocationService();
                dataGridView.DataSource = await locationService.GetAllAsync();
            };

            panel2.Controls.AddRange([
                syncBtn,
                locationBtn,
                customerBtn
            ]);
            // 
            // panel3 - Main Content
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Dock = DockStyle.Fill;

            dataGridView.Dock = DockStyle.Fill;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;

            // Consistent column headers styling
            dataGridView.EnableHeadersVisualStyles = false; // Disable default system theme
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.ColumnHeadersHeight = 30;

            // Rows styling
            dataGridView.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Grid color and borders
            dataGridView.GridColor = Color.LightGray;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Row template styling
            dataGridView.RowTemplate.Height = 30;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Add DataGridView to panel
            panel3.Controls.Add(dataGridView);
           
            dataGridView.Dock = DockStyle.Fill;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Data Synchronizer";
            ResumeLayout(false);
          
        }

        private async Task FetchDataAsync() // customers
        {
            try
            {
                using var customerService = new CustomerService();

                using var client = new SynHttpClient(Routes.BaseUrl);
                var response = await customerService.GetAllAsync();
                if (response is null)
                {
                    MessageBox.Show($"Failed to fetch data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dataGridView.DataSource = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
