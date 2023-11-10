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

        private List<ItemModel> ToolbaritemsProds = new List<ItemModel>();
        private List<ItemModel> ToolbaritemsPoliza = new List<ItemModel>();
        private List<ItemModel> ToolbaritemsCoexpo = new List<ItemModel>();

        protected override async Task OnInitializedAsync()
        {
            exportheader = await ExportHeaderService.ExportHeader_GetOne(InvoiceNo);
            detalleproducto = await DetalleProductoService.DetalleProductoList();
            polizaimportacion = await PolizaImportacionService.PolizaImportacionList();
            facturacoexpo = await FacturaCoexpoService.FacturaCoexpoList();

            exportheader.SerialNo = "E6E5577F-1161446804";


            #region ToolbarItems :: Prods, Poliza, Coexpo, 

            ToolbaritemsProds.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsProds.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsProds.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-add" });

            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-add" });

            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-add" });
            #endregion



        }

        #region ToolbarClickHandler :: Prods, Poliza, Coexpo,
        public void ToolbarClickHandlerProds(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                //code for add
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
        public void ToolbarClickHandlerPoliza(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                //code for add
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
        public void ToolbarClickHandlerCoexpo(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                //code for add
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


    }
}
