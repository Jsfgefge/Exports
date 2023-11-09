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
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Export.Pages {
    public partial class Index :ComponentBase {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject]
        IExportHeaderService ExportHeaderService { get; set; }
        [Inject]
        IConsignatariosService ConsignatariosService { get; set; }
        [Inject]
        IAduanasService AduanasService { get; set; }
        [Inject]
        ICountryService CountryService { get; set; }
        [Inject]
        ICargadorService CargadorService { get; set; }

        IEnumerable<ExportHeader> exportheader;
        IEnumerable<Consignatarios> consignatarios;
        IEnumerable<Aduanas> aduanas;
        IEnumerable<Country> country;
        IEnumerable<Cargador> cargador;

        #region Relleno
        //CLASES RELLENO, SOLO SERVIRAN PARA ESTA PARTE YA QUE NO SE CUENTA CON LAS DEBIDAS TABLAS EN SQL
        public class Incoterms {
            public int incotermID { get; set; }
            public string incoterm { get; set; }
        }
        public class docType {
            public int docID { get; set; }
            public string docName { get; set; }
        }
        private List<Incoterms> incoterms = new List<Incoterms>() {
            new Incoterms(){incotermID=1, incoterm="DDP"},
            new Incoterms(){incotermID=2, incoterm="EXW"},
            new Incoterms(){incotermID=3, incoterm="FCA"},
            new Incoterms(){incotermID=4, incoterm="FOB"},
            new Incoterms(){incotermID=5, incoterm="LDP"},
        };
        private List<docType> doctype = new List<docType>() {
            new docType() {docID=1, docName="MR"},
            new docType() {docID=2, docName="DA"},
            new docType() {docID=3, docName="DC"},
            new docType() {docID=4, docName="DL"},
            new docType() {docID=5, docName="DP"},
            new docType() {docID=6, docName="DR"},
            new docType() {docID=7, docName="ZQ"},
            new docType() {docID=8, docName="ZR"},
            new docType() {docID=9, docName="ZT"},
        };

        #endregion

        private int newerInvoiceNo;
        bool isChecked = false;
        string docTypelabel = "Tipo \nde\n documento";
        string incotermslabel = "Incoterm";
        private List<ItemModel> Toolbaritems = new List<ItemModel>();
        API_Obj rate = new API_Obj();

        ExportHeader addExport = new ExportHeader();
        SfDialog DialogAddExport;

        protected override async Task OnInitializedAsync() {
            exportheader = await ExportHeaderService.ExportHeaderList();
            consignatarios = await ConsignatariosService.ConsignatariosList();
            aduanas = await AduanasService.AduanasList();
            country = await CountryService.CountryList();
            cargador = await CargadorService.CargadorList();

            addExport.InvoiceDate = DateTime.Now;
            addExport.BoardingDate = DateTime.Now.AddDays(1);

            addExport.DocTypeID = 1;
            addExport.IncotermID = 4;

            newerInvoiceNo = exportheader.ToList()[0].InvoiceNo;

            if (Rates.Import()) {


                //addExport.ExchangeRate = (decimal)rate.conversion_rates.GTQ;
            }
            Toolbaritems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-add" });
        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args) {
            if (args.Item.Text == "Add") {
                //Code for add
                DialogAddExport.ShowAsync();
            }
            if (args.Item.Text == "Edit") {
                //Code for edit
            }
            if (args.Item.Text == "Delete") {
                //Code for delete
            }
        }

        private async void exportSave() {


            addExport.InvoiceNo = newerInvoiceNo + 1;
            await ExportHeaderService.ExportHeaderInsert(addExport);
            NavigationManager.NavigateTo($"/exportHeader/{addExport.InvoiceNo}");
        }

        private void Cancel() {
            this.DialogAddExport.HideAsync();
        }

    }
}
