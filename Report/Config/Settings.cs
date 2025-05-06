using M31.FluentApi.Attributes;

namespace App.Report.Settings;

#nullable disable

[FluentApi("{Name}Builder")]
public sealed class ApSettings
{
    [FluentMember(0)]
    public Global Global { get; set; }

    [FluentMember(1)]
    public Formats Format { get; set; }

    [FluentMember(2)]
    public AppColor Colors { get; set; }

    [FluentMember(3)]
    public TablePieChart TablePieChart { get; set; }
}

[FluentApi("{Name}Builder")]
public sealed class Global
{
    [FluentMember(0)]
    public float TextFontSize { get; set; }

    [FluentMember(1)]
    public float Padding { get; set; }
}

[FluentApi("{Name}Builder")]
public class AppColor
{
    public static AppColor _Green => new(0xff1c7039);
    public static AppColor _Yellow => new(0xffd9a701);
    public static AppColor _Red => new(0xffb02020);

    [FluentMember(0)]
    public AppColor Green { get; set; }

    [FluentMember(1)]
    public AppColor Yellow { get; set; }

    [FluentMember(2)]
    public AppColor Red { get; set; }

    private readonly uint _hex;

    public AppColor(uint hex)
    {
        _hex = hex;
    }

    public ScottPlot.Color ScottPlot() => new(_hex);
    public QuestPDF.Infrastructure.Color QuestPDF() => new(_hex);
}

[FluentApi("{Name}Builder")]
public class Formats
{
    [FluentMember(0)]
    public uint PercentageDecimals { get; set; }

    public string PercentageStr(float value) => string.Format($"{{0:F{PercentageDecimals}}}%", value);
}