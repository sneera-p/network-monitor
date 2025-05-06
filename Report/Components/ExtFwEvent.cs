using System.Collections.Frozen;
using App.Report.Settings;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ScottPlot;

namespace App.Report.Components;

public sealed class ExtFwsComponent(ApSettings settings, FrozenDictionary<ReadOnlyMemory<char>, float[]> firewalls) : IComponent
{
    private readonly FrozenDictionary<ReadOnlyMemory<char>, float[]> _data = firewalls;
    private readonly ApSettings _settings = settings;

    public void Compose(IContainer container)
    {
        container.Table(table =>
        {

            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(3);
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
            });

            table.Header(header =>
            {
                string[] headers = ["Host", "Avaiability", "Uptime"];

                foreach (var h in headers)
                {
                    header.Cell()
                        .BorderBottom(1)
                        .Padding(_settings.Global.Padding)
                        .AlignCenter()
                        .AlignMiddle()
                        .Text(h)
                        .FontSize(_settings.Global.TextFontSize);
                }
            });

            foreach (var fw in _data)
            {
                table.Cell()
                    .Padding(_settings.Global.Padding)
                    .Text(fw.Key.ToString())
                    .FontSize(_settings.Global.TextFontSize);

                table.Cell()

                    .MaxHeight(_settings.TablePieChart.Height)
                    .Svg(size =>
                    {
                        return CreateAvailabilityPieChart(fw.Value[0])
                            .GetSvgXml((int)size.Width, (int)size.Height);
                    });

                table.Cell()
                    .MaxHeight(_settings.TablePieChart.Height)
                    .Svg(size =>
                    {
                        return CreateStatusPieChart(fw.Value[1], fw.Value[2])
                            .GetSvgXml((int)size.Width, (int)size.Height);
                    });
            }

        });
    }

    private Plot CreateAvailabilityPieChart(float available)
    {
        if (available > 100.0f)
        {
            throw new InvalidDataException($"float available {available} is a percentage and must be below 100");
        }

        Plot plot = new();

        List<PieSlice> slices =
        [
            new PieSlice()
            {
                Value = available,
                FillColor = _settings.Colors.Green.ScottPlot(),
            },
            new PieSlice()
            {
                Value = 100.00f - available,
                FillColor = _settings.Colors.Red.ScottPlot(),
            },
        ];

        if (available == 100.00f)
        {
            slices.RemoveAt(1);
        }

        var pie = plot.Add.Pie(slices);
        var text = plot.Add.Text(_settings.Format.PercentageStr(available), 4.8f, 0);

        _settings.TablePieChart.Apply(pie);
        _settings.TablePieChart.Apply(text);
        _settings.TablePieChart.Apply(plot);

        return plot;
    }

    private Plot CreateStatusPieChart(float online, float maintainance)
    {
        if (online + maintainance > 100.00f)
        {
            throw new InvalidDataException($"is a percentage and must be below 100");
        }

        Plot plot = new();

        List<PieSlice> slices =
        [
            new PieSlice()
            {
                Value = online,
                FillColor = _settings.Colors.Green.ScottPlot(),
            },
            new PieSlice()
            {
                Value = maintainance,
                FillColor = _settings.Colors.Yellow.ScottPlot(),
            },
            new PieSlice()
            {
                Value = 100.00f - online - maintainance,
                FillColor = _settings.Colors.Red.ScottPlot(),
            },
        ];


        if (online + maintainance == 100.00f)
        {
            slices.RemoveAt(2);
        }

        if (online == 100.00f)
        {
            slices.RemoveAt(1);
        }

        var pie = plot.Add.Pie(slices);
        var text = plot.Add.Text(_settings.Format.PercentageStr(online), 4.8f, 0);

        _settings.TablePieChart.Apply(pie);
        _settings.TablePieChart.Apply(text);
        _settings.TablePieChart.Apply(plot);

        return plot;
    }
}