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
using System.Runtime.InteropServices;
using System.Collections;
using ExchangeRate.Controllers;

namespace Export.Pages
{
    public partial class ExportPage : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }
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
        [Inject]
        IConsignatariosService ConsignatariosService { get; set; }
        [Inject]
        IAduanasService AduanasService { get; set; }
        [Inject]
        ICountryService CountryService { get; set; }
        [Inject]
        ICargadorService CargadorService { get; set; }

        ExportHeader exportheader = new ExportHeader();
        ExportHeader editExport = new ExportHeader();
        ExchangeRateController exchangeRate = new ExchangeRateController();  

        IEnumerable<DetalleProducto> detalleproducto;
        IEnumerable<PolizaImportacion> polizaimportacion;
        IEnumerable<FacturaCoexpo> facturacoexpo;
        IEnumerable<Consignatarios> consignatarios;
        IEnumerable<Aduanas> aduanas;
        IEnumerable<Country> country;
        IEnumerable<Cargador> cargador;

        List<DetalleProducto> productosList = new List<DetalleProducto>();
        List<FacturaCoexpo> facturaCoexList = new List<FacturaCoexpo>();



        #region Relleno
        //CLASES RELLENO, SOLO SERVIRAN PARA ESTA PARTE YA QUE NO SE CUENTA CON LAS DEBIDAS TABLAS EN SQL
        public class HTS
        {
            public int htsID { get; set; }
            public string htsCodigo { get; set; }
            public string htsDescripcion { get; set; }
            public string categoria { get; set; }
        }
        public class Medida
        {
            public int medidaID { get; set; }
            public string medida { get; set; }
        }
        public class Proveedor
        {
            public int proveedorID { get; set; }
            public string nombreProveedor { get; set; }
            public int tipoProveedorID { get; set; }
        }
        public class TipoProveedor
        {
            public int tipoProveedorID { get; set; }
            public string tipoProveedor { get; set; }
        }
        public class Incoterms
        {
            public int incotermID { get; set; }
            public string incoterm { get; set; }
        }
        public class docType
        {
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
        private List<Proveedor> proveedor = new List<Proveedor>() {
            new Proveedor(){proveedorID = 1, nombreProveedor="RECEPSA", tipoProveedorID=1},
            new Proveedor(){proveedorID = 2, nombreProveedor="Prov1", tipoProveedorID=2},
            new Proveedor(){proveedorID = 3, nombreProveedor="Prov2", tipoProveedorID=3},
            new Proveedor(){proveedorID = 4, nombreProveedor="Prov3", tipoProveedorID=4},
            new Proveedor(){proveedorID = 5, nombreProveedor="Prov4", tipoProveedorID=2},
            new Proveedor(){proveedorID = 6, nombreProveedor="Prov5", tipoProveedorID=2},
            new Proveedor(){proveedorID = 7, nombreProveedor="Prov6", tipoProveedorID=1},
            new Proveedor(){proveedorID = 8, nombreProveedor="Prov7", tipoProveedorID=2},
            new Proveedor(){proveedorID = 9, nombreProveedor="Prov8", tipoProveedorID=3},
            new Proveedor(){proveedorID = 10,nombreProveedor="Prov9", tipoProveedorID=4},
            new Proveedor(){proveedorID = 11,nombreProveedor="Prov10", tipoProveedorID=2},
            new Proveedor(){proveedorID = 12,nombreProveedor="Prov11", tipoProveedorID=1},
            new Proveedor(){proveedorID = 13,nombreProveedor="Prov12", tipoProveedorID=3},
        };

        private List<TipoProveedor> tipoproveedor = new List<TipoProveedor>() {
            new TipoProveedor() {tipoProveedorID=1, tipoProveedor="Co-Ex"},
            new TipoProveedor() {tipoProveedorID=2, tipoProveedor="Complementariedad"},
            new TipoProveedor() {tipoProveedorID=3, tipoProveedor="Nacional"},
            new TipoProveedor() {tipoProveedorID=4, tipoProveedor="Subcontratista"},
        };


        #endregion


        private string currentStateProccess = "";
        private int productRowSelected = -1;
        private int facturaRowSelected = -1;
        private int polizaRowSelected = -1;
        private int currentRow;

        public bool[] isEnabled = {true,true};

        public bool isEnableOnEdit = true;
        public bool isEnableOnDelete = true;
        public bool isAnnuled=false;
        public string lblWarningDelete = "";
        private bool isCheckedClosed = false;

        string docTypelabel = "Tipo \nde\n documento";
        string incotermslabel = "Incoterm";

        string WarningHeaderMessage = "";
        string WarningContentMessage = "";
        WarningPage Warning;

        SfDialog DialogAddEditProduct;
        SfDialog DialogAddEditPolizaImportacion;
        SfDialog DialogAddEditFacturaCoex;
        SfDialog DialogEditExport;

        SfNumericTextBox<decimal> txtExchangeRate;

        SfGrid<DetalleProducto> detalleProductoGrid;
        SfGrid<FacturaCoexpo> facturaCoexpoGrid;
        SfGrid<PolizaImportacion> polizaImportacionGrid;

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
            polizaimportacion = await PolizaImportacionService.PolizaImportacionList(InvoiceNo);
            facturacoexpo = await FacturaCoexpoService.FacturaCoexpoList(InvoiceNo);

            consignatarios = await ConsignatariosService.ConsignatariosList();
            aduanas = await AduanasService.AduanasList();
            country = await CountryService.CountryList();
            cargador = await CargadorService.CargadorList();


            productosList = detalleproducto.ToList();
            facturaCoexList = facturacoexpo.ToList();

            isAnnuled = exportheader.isAnnuled;
            isCheckedClosed = exportheader.Closed;

            #region ToolbarItems :: Prods, Poliza, Coexpo, 

            ToolbaritemsProds.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add", Disabled = isAnnuled});
            ToolbaritemsProds.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-edit", Disabled = isAnnuled});
            ToolbaritemsProds.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-delete", Disabled = isAnnuled});

            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add", Disabled = isAnnuled });
            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-edit", Disabled = isAnnuled });
            ToolbaritemsPoliza.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-delete", Disabled = isAnnuled });

            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new export", PrefixIcon = "e-add", Disabled = isAnnuled });
            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Edit", TooltipText = "Add a new export", PrefixIcon = "e-edit", Disabled = isAnnuled });
            ToolbaritemsCoexpo.Add(new ItemModel() { Text = "Delete", TooltipText = "Add a new export", PrefixIcon = "e-delete", Disabled = isAnnuled });
            #endregion
        }

        #region ToolbarClickHandler :: Prods, Poliza, Coexpo,
        public async Task ToolbarClickHandlerProds(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            currentStateProccess = args.Item.Text;
            if (args.Item.Text == "Add")
            {
                //code for add
                addeditDetalleProduct = new DetalleProducto();
                addeditDetalleProduct.InvoiceNo = InvoiceNo;
                await this.DialogAddEditProduct.ShowAsync();
            }
            if (args.Item.Text == "Edit")
            {
                if (productRowSelected == -1)
                {
                    WarningHeaderMessage = "Warning";
                    WarningContentMessage = "Ningun elemento seleccionado de la tabla. Por favor seleccionar un elemento";
                    Warning.OpenDialog();
                }
                else
                {
                    isEnableOnEdit = false;
                    isEnableOnDelete = true;
                    //code for edit
                    addeditDetalleProduct = await DetalleProductoService.DetalleProducto_GetOne(productRowSelected);
                    await this.DialogAddEditProduct.ShowAsync();
                }

            }
            if (args.Item.Text == "Delete")
            {
                if (productRowSelected == -1)
                {
                    WarningHeaderMessage = "Warning";
                    WarningContentMessage = "Ningun elemento seleccionado de la tabla. Por favor seleccionar un elemento";
                    Warning.OpenDialog();
                }
                else
                {
                    isEnableOnEdit = false;
                    isEnableOnDelete = false;
                    //code for edit
                    addeditDetalleProduct = await DetalleProductoService.DetalleProducto_GetOne(productRowSelected);
                    await this.DialogAddEditProduct.ShowAsync();
                }
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
            currentStateProccess = args.Item.Text;
            if (args.Item.Text == "Add")
            {
                //code for add
                addeditFacturaCoexpo = new FacturaCoexpo();
                addeditFacturaCoexpo.InvoiceNo = InvoiceNo;
                await this.DialogAddEditFacturaCoex.ShowAsync();
            }
            if (args.Item.Text == "Edit")
            {
                if (facturaRowSelected == -1)
                {
                    WarningHeaderMessage = "Warning";
                    WarningContentMessage = "Ningun elemento seleccionado de la tabla. Por favor seleccionar un elemento";
                    Warning.OpenDialog();
                }
                else
                {
                    isEnableOnEdit = false;
                    isEnableOnDelete = false;
                    //code for edit
                    addeditFacturaCoexpo = await FacturaCoexpoService.FacturaCoexpo_GetOne(facturaRowSelected);
                    await this.DialogAddEditFacturaCoex.ShowAsync();
                }


            }
            if (args.Item.Text == "Delete")
            {

                if (facturaRowSelected == -1)
                {
                    WarningHeaderMessage = "Warning";
                    WarningContentMessage = "Ningun elemento seleccionado de la tabla. Por favor seleccionar un elemento";
                    Warning.OpenDialog();
                }
                else
                {
                    isEnableOnEdit = false;
                    isEnableOnDelete = false;
                    //code for edit
                    addeditFacturaCoexpo = await FacturaCoexpoService.FacturaCoexpo_GetOne(facturaRowSelected);
                    await this.DialogAddEditFacturaCoex.ShowAsync();



                    ////Code for delete
                    //facturaCoexList.RemoveAt(currentRow);

                    //await FacturaCoexpoService.FacturaCoexpoDelete(facturaRowSelected);

                    //facturaCoexpoGrid.Refresh();
                    //StateHasChanged();
                    //addeditFacturaCoexpo = new FacturaCoexpo();
                    //addeditFacturaCoexpo.InvoiceNo = InvoiceNo;
                    //facturaRowSelected = -1;
                }

            }
        }
        #endregion

        #region MyRegion
        private async Task ProductSave()
        {
            if (currentStateProccess == "Add")
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
                    DetalleProducto tempProd = new DetalleProducto();

                    tempProd = await DetalleProductoService.DetalleProductoGetLastRecord(InvoiceNo);

                    productosList.Add(new DetalleProducto
                    {
                        ProductoID = tempProd.ProductoID,
                        InvoiceNo = addeditDetalleProduct.InvoiceNo,
                        CodigoHTS = addeditDetalleProduct.CodigoHTS,
                        Description = addeditDetalleProduct.Description,
                        Categoria = addeditDetalleProduct.Categoria,
                        Cantidad = addeditDetalleProduct.Cantidad,
                        Medida = addeditDetalleProduct.Medida,
                        PricePerUnit = addeditDetalleProduct.PricePerUnit
                    });
                    detalleProductoGrid.Refresh();
                    StateHasChanged();
                    addeditDetalleProduct = new DetalleProducto();
                    addeditDetalleProduct.InvoiceNo = InvoiceNo;
                }
            }
            if (currentStateProccess == "Edit")
            {
                await DetalleProductoService.DetalleProductoUpdate(addeditDetalleProduct);
                this.DialogAddEditProduct.HideAsync();

                int tempID = productosList[currentRow].ProductoID;

                productosList.RemoveAt(currentRow);

                productosList.Insert(currentRow, new DetalleProducto
                {
                    ProductoID = tempID,
                    InvoiceNo = addeditDetalleProduct.InvoiceNo,
                    CodigoHTS = addeditDetalleProduct.CodigoHTS,
                    Description = addeditDetalleProduct.Description,
                    Categoria = addeditDetalleProduct.Categoria,
                    Cantidad = addeditDetalleProduct.Cantidad,
                    Medida = addeditDetalleProduct.Medida,
                    PricePerUnit = addeditDetalleProduct.PricePerUnit
                });

                detalleProductoGrid.Refresh();
                StateHasChanged();
                addeditDetalleProduct = new DetalleProducto();
                addeditDetalleProduct.InvoiceNo = InvoiceNo;
                productRowSelected = -1;
            }
            if(currentStateProccess == "Delete")
            {
                //Code for delete
                productosList.RemoveAt(currentRow);

                await DetalleProductoService.DetalleProductoDelete(productRowSelected);

                detalleProductoGrid.Refresh();
                StateHasChanged();
                addeditDetalleProduct = new DetalleProducto();
                addeditDetalleProduct.InvoiceNo = InvoiceNo;
                productRowSelected = -1;
            }
        }
        private async Task PolizaImportacionSave()
        {

        }
        private async Task FacturaCoexpoSave()
        {            
            if (currentStateProccess == "Add")
            {
                int Success = await FacturaCoexpoService.FacturaCoexpoInsert(InvoiceNo, addeditFacturaCoexpo.Proveedor, addeditFacturaCoexpo.TipoProveedor, addeditFacturaCoexpo.Factura, addeditFacturaCoexpo.Amount);

                if (Success != 0)
                {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Country Name already exists; it cannot be added again.";
                    Warning.OpenDialog();
                    // Data is left in the dialog so the user can see the problem.
                }
                else
                {
                    FacturaCoexpo tempFact = new FacturaCoexpo();

                    tempFact = await FacturaCoexpoService.FacturaCoexpoGetLastRecord(InvoiceNo);

                    facturaCoexList.Add(new FacturaCoexpo
                    {
                        CoexpoID = tempFact.CoexpoID,
                        InvoiceNo = addeditFacturaCoexpo.InvoiceNo,
                        Proveedor = addeditFacturaCoexpo.Proveedor,
                        TipoProveedor = addeditFacturaCoexpo.TipoProveedor,
                        Factura = addeditFacturaCoexpo.Factura,
                        Amount = addeditFacturaCoexpo.Amount
                    });
                    facturaCoexpoGrid.Refresh();
                    StateHasChanged();
                    addeditFacturaCoexpo = new FacturaCoexpo();
                    addeditFacturaCoexpo.InvoiceNo = InvoiceNo;
                }
            }
            if (currentStateProccess == "Edit")
            {
                int Success =
                await FacturaCoexpoService.FacturaCoexpoUpdate(addeditFacturaCoexpo);

                if(Success != 0)
                {

                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Factura Name already exists for this invoice; it cannot be added again.";
                    Warning.OpenDialog();
                    // Data is left in the dialog so the user can see the problem.
                }
                else
                {
                    this.DialogAddEditFacturaCoex.HideAsync();

                    int tempID = facturaCoexList[currentRow].CoexpoID;

                    facturaCoexList.RemoveAt(currentRow);

                    facturaCoexList.Insert(currentRow, new FacturaCoexpo
                    {
                        CoexpoID = tempID,
                        InvoiceNo = addeditFacturaCoexpo.InvoiceNo,
                        Proveedor = addeditFacturaCoexpo.Proveedor,
                        TipoProveedor = addeditFacturaCoexpo.TipoProveedor,
                        Factura = addeditFacturaCoexpo.Factura,
                        Amount = addeditFacturaCoexpo.Amount
                    });

                    facturaCoexpoGrid.Refresh();
                    StateHasChanged();
                    addeditFacturaCoexpo = new FacturaCoexpo();
                    addeditFacturaCoexpo.InvoiceNo = InvoiceNo;
                    facturaRowSelected = -1;
                }
            }
            if (currentStateProccess == "Delete")
            {
                //Code for delete
                facturaCoexList.RemoveAt(currentRow);

                await FacturaCoexpoService.FacturaCoexpoDelete(facturaRowSelected);

                facturaCoexpoGrid.Refresh();
                StateHasChanged();
                addeditFacturaCoexpo = new FacturaCoexpo();
                addeditFacturaCoexpo.InvoiceNo = InvoiceNo;
                facturaRowSelected = -1;
            }
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

        public void RowSelectHandlerProducto(RowSelectEventArgs<DetalleProducto> args)
        {
            productRowSelected = args.Data.ProductoID;
            currentRow = args.RowIndex;
        }
        public void RowSelectHandlerFactura(RowSelectEventArgs<FacturaCoexpo> args)
        {
            facturaRowSelected = args.Data.CoexpoID;
            currentRow = args.RowIndex;
        }
        protected async Task editSave()
        {
            editExport.InvoiceNo = InvoiceNo;
            editExport.Headerid = exportheader.Headerid;

            int Success =
            await ExportHeaderService.ExportHeaderUpdate(editExport.Headerid,
                                                         editExport.InvoiceDate,
                                                         editExport.SerialNo,
                                                         editExport.ConsignatarioID,
                                                         editExport.AduanasID,
                                                         editExport.CountryID,
                                                         editExport.CargadorID,
                                                         editExport.BoardingDate,
                                                         editExport.ExchangeRate,
                                                         editExport.Description,
                                                         editExport.DocTypeID,   
                                                         editExport.DuaSimplificada,
                                                         editExport.Complementaria,   //falta
                                                         editExport.IncotermID,   
                                                         editExport.Closed = isCheckedClosed   
                                                         );
            if (Success != 0)
            {
                WarningHeaderMessage = "Warning!";
                WarningContentMessage = "This [dato] Name already exists for this invoice; it cannot be added again.";
                Warning.OpenDialog();
                // Data is left in the dialog so the user can see the problem.
            }
            else
            {
                exportheader = await ExportHeaderService.ExportHeader_GetOne(InvoiceNo);
                this.DialogEditExport.HideAsync();
                this.StateHasChanged();
                editExport = new ExportHeader();
            }
        }

        private async Task openEdit()
        {
            //code for add
            editExport = await ExportHeaderService.ExportHeader_GetOne(InvoiceNo);
            isCheckedClosed = editExport.Closed;
            await this.DialogEditExport.ShowAsync();
        }

        private void Cancel()
        {
            this.DialogEditExport.HideAsync();
        }

        private async Task ExchangeRateRefresh()
        {
            decimal temp =  await exchangeRate.GetAsync();
            txtExchangeRate.Value = temp;
            editExport.ExchangeRate = temp;
            this.StateHasChanged();
        }

    }
}
