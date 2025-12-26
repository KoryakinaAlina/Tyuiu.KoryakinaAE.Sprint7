using System.ComponentModel;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    partial class FormMain
    {
        private IContainer components = null;

        private MenuStrip menuStripMain_KAE;
        private ToolStrip toolStripMain_KAE;
        private DataGridView dataGridViewComputers_KAE;
        private ToolStripComboBox toolStripComboBoxTables_KAE;
        private ToolStripTextBox toolStripTextBoxSearch_KAE;

        private ToolStripMenuItem toolStripMenuItemFile_KAE;
        private ToolStripMenuItem toolStripMenuItemOpenCsv_KAE;
        private ToolStripMenuItem toolStripMenuItemOpenFolder_KAE;
        private ToolStripMenuItem toolStripMenuItemSaveCsv_KAE;
        private ToolStripMenuItem toolStripMenuItemExit_KAE;

        private ToolStripMenuItem toolStripMenuItemHelp_KAE;
        private ToolStripMenuItem toolStripMenuItemUserGuide_KAE;
        private ToolStripMenuItem toolStripMenuItemAbout_KAE;

        private void InitializeComponent()
        {
            components = new Container();

            menuStripMain_KAE = new MenuStrip();
            toolStripMain_KAE = new ToolStrip();
            dataGridViewComputers_KAE = new DataGridView();
            toolStripComboBoxTables_KAE = new ToolStripComboBox();
            toolStripTextBoxSearch_KAE = new ToolStripTextBox();

            // ===== MENU =====
            toolStripMenuItemFile_KAE = new ToolStripMenuItem("Файл");
            toolStripMenuItemOpenCsv_KAE = new ToolStripMenuItem("Открыть CSV", null, OpenCsv_Click);
            toolStripMenuItemOpenFolder_KAE = new ToolStripMenuItem("Открыть папку CSV", null, OpenFolder_Click);
            toolStripMenuItemSaveCsv_KAE = new ToolStripMenuItem("Сохранить CSV", null, SaveCsv_Click);
            toolStripMenuItemExit_KAE = new ToolStripMenuItem("Выход", null, Exit_Click);

            toolStripMenuItemHelp_KAE = new ToolStripMenuItem("Справка");
            toolStripMenuItemUserGuide_KAE = new ToolStripMenuItem("Руководство", null, UserGuide_Click);
            toolStripMenuItemAbout_KAE = new ToolStripMenuItem("О программе", null, About_Click);

            toolStripMenuItemFile_KAE.DropDownItems.AddRange(new ToolStripItem[]
            {
        toolStripMenuItemOpenCsv_KAE,
        toolStripMenuItemOpenFolder_KAE,
        toolStripMenuItemSaveCsv_KAE,
        new ToolStripSeparator(),
        toolStripMenuItemExit_KAE
            });

            toolStripMenuItemHelp_KAE.DropDownItems.AddRange(new ToolStripItem[]
            {
        toolStripMenuItemUserGuide_KAE,
        toolStripMenuItemAbout_KAE
            });

            menuStripMain_KAE.Items.AddRange(new ToolStripItem[]
            {
        toolStripMenuItemFile_KAE,
        toolStripMenuItemHelp_KAE
            });

            // ===== TOOLSTRIP =====
            toolStripMain_KAE.Items.AddRange(new ToolStripItem[]
            {
        toolStripComboBoxTables_KAE,
        new ToolStripSeparator(),
        new ToolStripButton("Добавить", null, Add_Click),
        new ToolStripButton("Изменить", null, Edit_Click),
        new ToolStripButton("Удалить", null, Delete_Click),
        new ToolStripSeparator(),
        new ToolStripLabel("Поиск:"),
        toolStripTextBoxSearch_KAE,
        new ToolStripButton("✕", null, ClearSearch_Click),
        new ToolStripSeparator(),
        // ===== НОВЫЕ КНОПКИ =====
        new ToolStripButton("Статистика", null, Statistics_Click),
        new ToolStripButton("График", null, Chart_Click)
            });

            toolStripTextBoxSearch_KAE.TextChanged += Search_TextChanged;
            toolStripComboBoxTables_KAE.SelectedIndexChanged += Tables_SelectedIndexChanged;

            // ===== DATAGRID =====
            dataGridViewComputers_KAE.Dock = DockStyle.Fill;
            dataGridViewComputers_KAE.ReadOnly = true;
            dataGridViewComputers_KAE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ===== ADD CONTROLS =====
            Controls.Add(dataGridViewComputers_KAE);
            Controls.Add(toolStripMain_KAE);
            Controls.Add(menuStripMain_KAE);

            MainMenuStrip = menuStripMain_KAE;
            Text = "Персональные ЭВМ — Вариант 11";
            ClientSize = new Size(1000, 600);
        }
    }
}