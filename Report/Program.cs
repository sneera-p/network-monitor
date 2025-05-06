using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using App.Report.Components;
using QuestPDF.Infrastructure;
using App.Report.Settings;
using System.Collections.Frozen;

QuestPDF.Settings.License = LicenseType.Community;

var settings = ApSettingsBuilder
    .WithGlobal(builder =>
        builder
            .WithTextFontSize(12.0f)
            .WithPadding(8.0f)
    ).WithFormat(builder =>
        builder
            .WithPercentageDecimals(1)
    )
    .WithColors(builder =>
        builder
            .WithGreen(new AppColor(0xff1c7039))
            .WithYellow(new AppColor(0xffd9a701))
            .WithRed(new AppColor(0xffb02020))
    )
    .WithTablePieChart(builder =>
        builder
            .WithDonutFraction(0.4f)
            .WithLineWidth(0.0f)
            .WithTextAlignment(ScottPlot.Alignment.MiddleRight)
            .WithFontSize(12.0f)
            .WithHeight(40.0f)
    );


var firewalls = new Dictionary<string, float[]>{
    {"ADV-EXBYUIERGT", [98.127f, 84.2f, 1f]},
    {"ADV-EXBYURGT", [100.00f, 53.8f, 1f]},
    {"ADV-EXBYERGT", [84.34f, 67.23f, 1f]},
    {"ADV-EXBYUIET", [0f, 12.46f, 1f]}
};


var document = Document.Create(document =>
{

    PageSize @pageSize = PageSizes.A4;
    float @pageMargin = 28.0f;

    document.Page(page => 
    {

        page.Size(@pageSize);
        page.Margin(@pageMargin);

        page.Header()
            .MinHeight(50)
            .Text("WAN Firewalls Availabiliy")
            .FontColor(Color.FromRGB(0xcb, 0x77, 0x44))
            .SemiBold()
            .FontSize(18)
            .AlignCenter();

        page.Content()
            .Component(new ExtFwsComponent
            (
                settings,
                firewalls
                    .AsParallel()
                    .Select(p => new KeyValuePair<ReadOnlyMemory<char>, float[]>(p.Key.AsMemory(), p.Value))
                    .ToFrozenDictionary()
            ));


    });

});

document.ShowInCompanion();