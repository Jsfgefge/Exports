using Export.Data;
using Export.Shared;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Diagrams;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Schedule.Internal;
using System.Runtime.CompilerServices;

namespace Export.Pages {
    public partial class ExportPage : ComponentBase {
        [Parameter]
        public int InvoiceNo { get; set; }
        [Inject]
        IExportHeaderService ExportHeaderService { get; set; }

        ExportHeader exportheader = new ExportHeader();

        string consignatario = "";
        string aduana = "";
        string cargador = "";
        string pais = "";
        string exchangeRate = "";
        string Descripcion = "";
        


        protected override async Task OnInitializedAsync() {
            exportheader = await ExportHeaderService.ExportHeader_GetOne(InvoiceNo);

            consignatario = exportheader.ConsignatarioID.ToString();
            aduana = exportheader.AduanasID.ToString();
            cargador = exportheader.CargadorID.ToString();
            pais = exportheader.CountryID.ToString();
            exchangeRate = exportheader.ExchangeRate.ToString();
            Descripcion = exportheader.Description.ToString();
        }


    }
}
