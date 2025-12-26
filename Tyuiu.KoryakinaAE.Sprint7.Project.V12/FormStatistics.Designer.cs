using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    partial class FormStatistics
    {
        private IContainer components = null;

        private ComboBox comboBoxMode_KoryakinaAE;
        private DataGridView dataGridViewStats_KoryakinaAE;
        private Button buttonExportCsv_KoryakinaAE;
        private Label labelMode_KoryakinaAE;

        private void InitializeComponent()
        {
            components = new Container();

            comboBoxMode_KoryakinaAE = new ComboBox();
            dataGridViewStats_KoryakinaAE = new DataGridView();
            buttonExportCsv_KoryakinaAE = new Button();
            labelMode_KoryakinaAE = new Label();

            // ComboBox
            comboBoxMode_KoryakinaAE.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMode_KoryakinaAE.Location = new Point(140, 15);
            comboBoxMode_KoryakinaAE.Size = new Size(260, 24);
            comboBoxMode_KoryakinaAE.SelectedIndexChanged += comboBoxMode_KoryakinaAE_SelectedIndexChanged;

            // Label
            labelMode_KoryakinaAE.Text = "Режим статистики:";
            labelMode_KoryakinaAE.Location = new Point(15, 18);
            labelMode_KoryakinaAE.AutoSize = true;

            // Button
            buttonExportCsv_KoryakinaAE.Text = "Экспорт CSV";
            buttonExportCsv_KoryakinaAE.Location = new Point(420, 13);
            buttonExportCsv_KoryakinaAE.Size = new Size(120, 28);
            buttonExportCsv_KoryakinaAE.Click += buttonExportCsv_KoryakinaAE_Click;

            // DataGridView
            dataGridViewStats_KoryakinaAE.Dock = DockStyle.Bottom;
            dataGridViewStats_KoryakinaAE.Height = 420;
            dataGridViewStats_KoryakinaAE.ReadOnly = true;
            dataGridViewStats_KoryakinaAE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Form
            Controls.Add(labelMode_KoryakinaAE);
            Controls.Add(comboBoxMode_KoryakinaAE);
            Controls.Add(buttonExportCsv_KoryakinaAE);
            Controls.Add(dataGridViewStats_KoryakinaAE);

            Text = "Статистика";
            ClientSize = new Size(600, 500);
        }
    }
}