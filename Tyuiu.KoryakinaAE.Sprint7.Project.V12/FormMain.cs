using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tyuiu.KoryakinaAE.Sprint7.Project.V12.Lib;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    public partial class FormMain : Form
    {
        private readonly DataService dataService_KAE = new();
        private readonly BindingSource bindingSource_KAE = new();
        private DataTable table_KAE = new();
        private readonly Dictionary<string, string> csvFiles_KAE = new();
        private string currentPath_KAE = string.Empty;
        private bool isEditMode_KAE = false;

        public FormMain()
        {
            InitializeComponent();

            dataGridViewComputers_KAE.AutoGenerateColumns = true;
            dataGridViewComputers_KAE.DataSource = bindingSource_KAE;

            ApplyIcons(); // Загрузка иконок для кнопок и меню
        }

        // ===== ICONS =====
        private void ApplyIcons()
        {
            // Иконки для кнопок ToolStrip
            foreach (ToolStripItem item in toolStripMain_KAE.Items)
            {
                if (item is not ToolStripButton btn) continue;

                btn.Image = btn.Text switch
                {
                    "Добавить" => LoadIcon("add.png"),
                    "Изменить" => LoadIcon("edit.png"),
                    "Удалить" => LoadIcon("delete.png"),
                    "Статистика" => LoadIcon("stats.png"),
                    "График" => LoadIcon("chart.png"),
                    _ => null
                };

                btn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.AutoSize = false;
                btn.Size = new Size(120, 38);
                btn.Padding = new Padding(6, 0, 6, 0);
            }

            // Иконки для пунктов меню
            toolStripMenuItemOpenCsv_KAE.Image = LoadIcon("open.png");
            toolStripMenuItemOpenFolder_KAE.Image = LoadIcon("folder.png");
            toolStripMenuItemSaveCsv_KAE.Image = LoadIcon("save.png");
            toolStripMenuItemExit_KAE.Image = LoadIcon("exit.png");
            toolStripMenuItemUserGuide_KAE.Image = LoadIcon("help.png");
            toolStripMenuItemAbout_KAE.Image = LoadIcon("about.png");
        }

        private Image LoadIcon(string fileName)
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Icons",
                fileName
            );

            return File.Exists(path) ? Image.FromFile(path) : null;
        }

        // ===== FILE =====
        private void OpenCsv_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new()
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            LoadTable(dlg.FileName);
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dlg = new();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            csvFiles_KAE.Clear();
            toolStripComboBoxTables_KAE.Items.Clear();

            foreach (var file in Directory.GetFiles(dlg.SelectedPath, "*.csv"))
            {
                string key = Path.GetFileNameWithoutExtension(file);
                csvFiles_KAE[key] = file;
                toolStripComboBoxTables_KAE.Items.Add(key);
            }

            if (toolStripComboBoxTables_KAE.Items.Count > 0)
                toolStripComboBoxTables_KAE.SelectedIndex = 0;
        }

        private void SaveCsv_Click(object sender, EventArgs e)
        {
            if (table_KAE.Rows.Count == 0) return;

            using SaveFileDialog dlg = new()
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            dataService_KAE.SaveCsv(table_KAE, dlg.FileName);
        }

        private void Exit_Click(object sender, EventArgs e) => Close();

        // ===== TABLE =====
        private void LoadTable(string path)
        {
            table_KAE = LoadCsv(path);
            bindingSource_KAE.DataSource = table_KAE;
            currentPath_KAE = path;
            dataGridViewComputers_KAE.ReadOnly = true;
        }

        private DataTable LoadCsv(string path)
        {
            var table = new DataTable();
            var lines = File.ReadAllLines(path);

            if (lines.Length == 0) return table;

            foreach (var h in lines[0].Split(';'))
                table.Columns.Add(h);

            for (int i = 1; i < lines.Length; i++)
                table.Rows.Add(lines[i].Split(';'));

            return table;
        }

        // ===== CRUD =====
        private void Add_Click(object sender, EventArgs e)
        {
            table_KAE.Rows.Add(table_KAE.NewRow());
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            isEditMode_KAE = !isEditMode_KAE;
            dataGridViewComputers_KAE.ReadOnly = !isEditMode_KAE;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewComputers_KAE.SelectedRows)
                if (row.DataBoundItem is DataRowView view)
                    view.Row.Delete();
        }

        // ===== SEARCH =====
        private void Search_TextChanged(object sender, EventArgs e)
        {
            string text = toolStripTextBoxSearch_KAE.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                table_KAE.DefaultView.RowFilter = "";
                return;
            }

            var filters = table_KAE.Columns
                .Cast<DataColumn>()
                .Select(c => $"[{c.ColumnName}] LIKE '%{text.Replace("'", "''")}%'");

            table_KAE.DefaultView.RowFilter = string.Join(" OR ", filters);
        }

        private void ClearSearch_Click(object sender, EventArgs e)
        {
            toolStripTextBoxSearch_KAE.Text = "";
            table_KAE.DefaultView.RowFilter = "";
        }

        private void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxTables_KAE.SelectedItem is string key &&
                csvFiles_KAE.ContainsKey(key))
            {
                LoadTable(csvFiles_KAE[key]);
            }
        }

        // ===== HELP =====
        private void UserGuide_Click(object sender, EventArgs e)
        {
            // Создаём форму справки
            using FormUserGuide guideForm = new FormUserGuide();
            guideForm.ShowDialog(); // показываем модально
        }

        private void About_Click(object sender, EventArgs e)
        {
            // Создаём форму "О программе"
            using FormAbout aboutForm = new FormAbout();
            aboutForm.ShowDialog(); // показываем модально
        }

       

        // ===== STATISTICS =====
        private void Statistics_Click(object sender, EventArgs e)
        {
            if (table_KAE == null || table_KAE.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для статистики", "Статистика", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormStatistics formStats = new FormStatistics(table_KAE);
            formStats.ShowDialog();
        }

        // ===== CHART =====
        private void Chart_Click(object sender, EventArgs e)
        {
            if (table_KAE == null || table_KAE.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для графика", "График", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormChart formChart = new FormChart(table_KAE);
            formChart.ShowDialog();
        }

        private void toolStripMenuItemHelp_KAE_Click(object sender, EventArgs e)
        {

        }
    }
}