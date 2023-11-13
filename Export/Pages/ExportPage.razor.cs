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
using System.Linq;

namespace Export.Pages
{
    public partial class ExportPage : ComponentBase
    {
        [Parameter]
        public int InvoiceNo { get; set; }
        [Inject]
        IExportHeaderService ExportHeaderService { get; set; }
        [Inject]
        IDetalleProductoService DetalleProductoService { get; set; }
        [Inject]
        IPolizaImportacionService PolizaImportacionService { get; set; }
        [Inject]
        IFacturaCoexpoService FacturaCoexpoService { get; set; }

        ExportHeader exportheader = new ExportHeader();

        IEnumerable<DetalleProducto> detalleproducto;
        IEnumerable<PolizaImportacion> polizaimportacion;
        IEnumerable<FacturaCoexpo> facturacoexpo;

        List<DetalleProducto> productosList = new List<DetalleProducto>();

        #region Relleno
        //CLASES RELLENO, SOLO SERVIRAN PARA ESTA PARTE YA QUE NO SE CUENTA CON LAS DEBIDAS TABLAS EN SQL
        public class HTS
        {
            public int htsID { get; set; }
            public string  htsCodigo { get; set; }
            public string htsDescripcion { get; set; }
            public string categoria { get; set; }
        }
        public class Medida
        {
            public int medidaID { get; set; }
            public string medida { get; set; }
        }

        private List<HTS> hts = new List<HTS>() {
            new HTS(){htsID=1, htsCodigo="6109100000", htsDescripcion="CAMISETA PARA DAMA TEJIDO ALGODON", categoria = "339"},
            new HTS(){htsID=2, htsCodigo="6110200000", htsDescripcion="PLAYERA PARA CABALLERO TEJIDO ALGODON (PULLOVERS)", categoria = "338"},
            new HTS(){htsID=3, htsCodigo="6110300000", htsDescripcion="PLAYERA CABALLERO TEJIDO SINTETICO (PULLOVERS)", categoria = "638"},
            new HTS(){htsID=4, htsCodigo="6106100000", htsDescripcion="Playera para dama tejido algodon", categoria = "339"},
            new HTS(){htsID=5, htsCodigo="6109100000", htsDescripcion="PLAYERA PARA CABALLERO / NINO TEJIDO ALGODON", categoria = "338"},
        };
        private List<Medida> medida = new List<Medida>() {
            new Medida() {medidaID=1, medida="Cm"},
            new Medida() {medidaID=2, medida="EA"},
            new Medida() {medidaID=3, medida="Kgs"},
            new Medida() {medidaID=4, medida="M"},
            new Medida() {medidaID=5, medida="mm"},
            new Medida() {medidaID=6, medida="Mt2"},
            new Medida() {medidaID=7, medida="Pcs"},
            new Medida() {medidaID=8, medida="Yds"},
        };

        #endregion


        string WarningHeaderMessage = "";
        string WarningContentMessage = "";
        WarningPage Warning;

        SfDialog DialogAddEditProduct;
        SfDialog DialogAddEditPolizaImportacion;
        SfDialog DialogAddEditFacturaCoex;

        SfGrid<DetalleProducto> detalleProductoGrid;

        public DetalleProducto addeditDetalleProduct = new DetalleProducto();
        public PolizaImportacion addeditPolizaImportacion = new PolizaImportacion();
        public FacturaCoexpo addeditFacturaCoexpo = new FacturaCoexpo();

        private List<ItemModel> ToolbaritemsProds = new List<ItemModel>();
        private List<ItemModel> ToolbaritemsPoliza = new List<ItemModel>();
        private List<ItemModel> ToolbaritemsCoexpo = new List<ItemModel>();
        protected override async Task OnInitializedAsync()
        {
            exportheader = await ExportHeaderService.ExportHeader_GetOne(InvoiceNo);
            detalleproducto = await DetalleProductoService.DetalleProductoList(InvoiceNo);
            polizaimportacion = await PolizaImportacionService.PolizaImportacionList();
            facturacoexpo = await FacturaCoexpoService.FacturaCoexpoList();

            productosList = detalleproducto.ToList();

            exportheader.SerialNo = "E6E5577F-1161446804";

            #region ToolbarItems :: Prods, Poliza, Coexpo, 

            ToolbaritemsProds.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsProds.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-edit" });
            ToolbaritemsProds.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-delete" });

            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-edit" });
            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-delete" });

            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-edit" });
            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-delete" });
            #endregion
        }

        #region ToolbarClickHandler :: Prods, Poliza, Coexpo,
        public async void ToolbarClickHandlerProds(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                //code for add
                addeditDetalleProduct = new DetalleProducto();
                addeditDetalleProduct.InvoiceNo = InvoiceNo;
                await this.DialogAddEditProduct.ShowAsync();
            }
            if (args.Item.Text == "Edit")
            {
                //code for edit
            }
            if (args.Item.Text == "Delete")
            {
                //Code for delete
            }
        }
        public async void ToolbarClickHandlerPoliza(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                //code for add
                await this.DialogAddEditPolizaImportacion.ShowAsync();

            }
            if (args.Item.Text == "Edit")
            {
                //code for edit
            }
            if (args.Item.Text == "Delete")
            {
                //Code for delete
            }
        }
        public async void ToolbarClickHandlerCoexpo(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                //code for add
                await this.DialogAddEditFacturaCoex.ShowAsync();

            }
            if (args.Item.Text == "Edit")
            {
                //code for edit
            }
            if (args.Item.Text == "Delete")
            {
                //Code for delete
            }
        }
        #endregion

        #region MyRegion
        private async Task ProductSave()
        {
            int Success = await DetalleProductoService.DetalleProductoInsert(InvoiceNo,
                                                                             addeditDetalleProduct.CodigoHTS,
                                                                             addeditDetalleProduct.Description,
                                                                             addeditDetalleProduct.Categoria,
                                                                             addeditDetalleProduct.Cantidad,
                                                                             addeditDetalleProduct.Medida,
                                                                             addeditDetalleProduct.PricePerUnit
                                                                             );

            if (Success != 0)
            {
                WarningHeaderMessage = "Warning!";
                WarningContentMessage = "This Country Name already exists; it cannot be added again.";
                Warning.OpenDialog();
                // Data is left in the dialog so the user can see the problem.
            }
            else
            {
                productosList.Add(new DetalleProducto {
                    InvoiceNo = addeditDetalleProduct.InvoiceNo,
                    CodigoHTS = addeditDetalleProduct.CodigoHTS,
                    Description = addeditDetalleProduct.Description,
                    Categoria= addeditDetalleProduct.Categoria,
                    Cantidad = addeditDetalleProduct.Cantidad,
                    Medida = addeditDetalleProduct.Medida,
                    PricePerUnit = addeditDetalleProduct.PricePerUnit
                });
                detalleProductoGrid.Refresh();
                StateHasChanged();
                addeditDetalleProduct = new DetalleProducto();
            }
        }
        private void PolizaImportacionSave()
        {

        }
        private void FacturaCoexpoSave()
        {

        }

        private async Task CloseDialog()
        {
            this.DialogAddEditProduct.HideAsync();
            this.DialogAddEditPolizaImportacion.HideAsync();
            this.DialogAddEditFacturaCoex.HideAsync();
        }

        #endregion

        private void OnChange(Syncfusion.Blazor.DropDowns.SelectEventArgs<HTS> args)
        {
            this.addeditDetalleProduct.Description = args.ItemData.htsDescripcion;
            this.addeditDetalleProduct.Categoria = args.ItemData.categoria;
        }

    }
}
