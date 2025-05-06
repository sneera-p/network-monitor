using M31.FluentApi.Attributes;
using ScottPlot;
using ScottPlot.Plottables;

namespace App.Report.Settings;

[FluentApi("{Name}Builder")]
public sealed class TablePieChart
{
    [FluentMember(0)]
    public float DonutFraction { get; set; }

    [FluentMember(1)]
    public float LineWidth { get; set; }

    [FluentMember(2)]
    public Alignment TextAlignment { get; set; }

    [FluentMember(3)]
    public float FontSize { get; set; }

    [FluentMember(4)]
    public float Height { get; set; }

    public void Apply(Pie pie)
    {
        pie.DonutFraction = DonutFraction;
        pie.LineWidth = LineWidth;
    }

    public void Apply(Text text)
    {
        text.LabelFontSize = FontSize;
        text.Alignment = TextAlignment;
    }

    public void Apply(Plot plot)
    {
        plot.Axes.Frameless();
        plot.HideAxesAndGrid();
    }
}