using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    public partial class FormChart : Form
    {
        private sealed class StatItem
        {
            public string Name { get; init; } = "—";
            public int Count { get; init; }
            public double AvgRam { get; init; }
            public double AvgPrice { get; init; }
        }

        // Четыре панели для графиков
        private readonly Panel panelTopLeft = new Panel();
        private readonly Panel panelTopRight = new Panel();
        private readonly Panel panelBottomLeft = new Panel();
        private readonly Panel panelBottomRight = new Panel();

        private readonly List<StatItem> byFirmRam;
        private readonly List<StatItem> byFirmCount;
        private readonly List<StatItem> byFirmPrice;
        private readonly List<StatItem> byProcessorCount;

        public FormChart(DataTable table)
        {
            InitializeComponent();

            Text = "Аналитика персональных ЭВМ";
            WindowState = FormWindowState.Maximized;
            BackColor = Color.White;
            DoubleBuffered = true;

            // Построение статистики
            byFirmRam = BuildStatistics(table, "Фирма-изготовитель", "Объем ОЗУ");
            byFirmCount = BuildStatistics(table, "Фирма-изготовитель", null, countMode: true);
            byFirmPrice = BuildStatistics(table, "Фирма-изготовитель", "Цена", priceMode: true);
            byProcessorCount = BuildStatistics(table, "Процессор", null, countMode: true);

            // Настройка панели TopLeft
            panelTopLeft.Dock = DockStyle.Top;
            panelTopLeft.Height = ClientSize.Height / 2;
            panelTopLeft.Paint += (s, e) => DrawBarChart(e.Graphics, byFirmRam, panelTopLeft.ClientRectangle, "Средний ОЗУ по фирмам", x => x.AvgRam, "{0:N1} GB");

            // Настройка панели TopRight
            panelTopRight.Dock = DockStyle.Top;
            panelTopRight.Height = ClientSize.Height / 2;
            panelTopRight.Paint += (s, e) => DrawBarChart(e.Graphics, byFirmCount, panelTopRight.ClientRectangle, "Количество ПК по фирмам", x => x.Count, "{0}");

            // Настройка панели BottomLeft
            panelBottomLeft.Dock = DockStyle.Bottom;
            panelBottomLeft.Height = ClientSize.Height / 2;
            panelBottomLeft.Paint += (s, e) => DrawBarChart(e.Graphics, byFirmPrice, panelBottomLeft.ClientRectangle, "Средняя цена по фирмам", x => x.AvgPrice, "{0:N0} ₽");

            // Настройка панели BottomRight
            panelBottomRight.Dock = DockStyle.Bottom;
            panelBottomRight.Height = ClientSize.Height / 2;
            panelBottomRight.Paint += (s, e) => DrawBarChart(e.Graphics, byProcessorCount, panelBottomRight.ClientRectangle, "Количество ПК по процессору", x => x.Count, "{0}");

            // Добавление панелей на форму
            Controls.Add(panelTopLeft);
            Controls.Add(panelTopRight);
            Controls.Add(panelBottomLeft);
            Controls.Add(panelBottomRight);

            // Обновление панели при изменении размера окна
            this.Resize += (s, e) => { panelTopLeft.Invalidate(); panelTopRight.Invalidate(); panelBottomLeft.Invalidate(); panelBottomRight.Invalidate(); };
        }

        private static List<StatItem> BuildStatistics(DataTable table, string groupColumn, string valueColumn, bool countMode = false, bool priceMode = false)
        {
            if (!table.Columns.Contains(groupColumn)) return new List<StatItem>();

            var groups = table.AsEnumerable()
                              .GroupBy(r => r[groupColumn]?.ToString() ?? "—");

            var result = new List<StatItem>();

            foreach (var g in groups)
            {
                double avgValue = 0;

                if (valueColumn != null)
                {
                    var values = g.Select(r => double.TryParse(r[valueColumn]?.ToString(), out double v) ? v : (double?)null)
                                  .Where(v => v.HasValue)
                                  .Select(v => v!.Value)
                                  .ToList();

                    avgValue = values.Any() ? values.Average() : 0;
                }

                result.Add(new StatItem
                {
                    Name = g.Key,
                    Count = g.Count(),
                    AvgRam = valueColumn == "Объем ОЗУ" ? avgValue : 0,
                    AvgPrice = priceMode ? avgValue : 0
                });
            }

            return result.OrderByDescending(x => countMode ? x.Count : (priceMode ? x.AvgPrice : x.AvgRam)).ToList();
        }

        private void DrawBarChart(Graphics g, List<StatItem> stats, Rectangle rect, string title, Func<StatItem, double> valueSelector, string format)
        {
            g.Clear(Color.White);

            // Заголовок
            g.DrawString(title, new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Black, rect.X + 10, rect.Y + 10);

            if (stats.Count == 0)
            {
                g.DrawString("Нет данных", new Font("Segoe UI", 10), Brushes.Gray, rect.X + 10, rect.Y + 40);
                return;
            }

            int barWidth = Math.Max(20, (rect.Width - 40) / Math.Max(stats.Count, 1) - 10);
            double maxValue = stats.Max(valueSelector);

            for (int i = 0; i < stats.Count; i++)
            {
                var s = stats[i];
                int barHeight = maxValue > 0 ? (int)(valueSelector(s) / maxValue * (rect.Height - 60)) : 0;
                int x = rect.X + 20 + i * (barWidth + 10);
                int y = rect.Y + rect.Height - barHeight - 20;

                g.FillRectangle(Brushes.SteelBlue, x, y, barWidth, barHeight);
                g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight);

                // Значение сверху
                g.DrawString(string.Format(format, valueSelector(s)), new Font("Segoe UI", 8), Brushes.Black, x, y - 18);

                // Повернутый текст снизу
                g.TranslateTransform(x + barWidth / 2, rect.Y + rect.Height - 10);
                g.RotateTransform(-45);
                g.DrawString(s.Name, new Font("Segoe UI", 8), Brushes.Black, 0, 0);
                g.ResetTransform();
            }
        }
    }
}