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
    public partial class CargadorPage :ComponentBase{
        [Inject]
        ICargadorService CargadorService { get; set; }

        IEnumerable<Cargador> cargador;
        public List<ItemModel> Toolbaritems = new List<ItemModel>();


        SfDialog DialogAddEditCargador;
        SfTextBox txtCargador;
        Cargador addeditCargador = new Cargador();

        public int SelectedCargadorId { get; set; } = 0;

        WarningPage Warning;
        string WarningHeaderMessage = "";
        string WarningContentMessage = "";
        string HeaderText = "";
        protected override async Task OnInitializedAsync() {
            cargador = await CargadorService.CargadorList();

            Toolbaritems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new Cargador", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit a Cargador", PrefixIcon = "e-edit" });
        }


        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args) {
            if (args.Item.Text == "Add") {
                //Code for adding goes here
                addeditCargador = new Cargador();             // Ensures a blank form when adding
                HeaderText = "Add Cargador";
                await this.DialogAddEditCargador.Show();
            }
            if (args.Item.Text == "Edit") {
                //Code for editing goes here
                //Check that a Tax Rate has been selected
                if (SelectedCargadorId == 0) {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please select a Tax Rate from the grid.";
                    Warning.OpenDialog();
                }
                else {
                    //populate addeditTax (temporary data set used for the editing process)
                    HeaderText = "Edit Tax Rate";
                    addeditCargador = await CargadorService.Cargador_GetOne(SelectedCargadorId);
                    await this.DialogAddEditCargador.Show();
                }

            }
        }

        public async Task RowSelectHandler(RowSelectEventArgs<Cargador> args) {
            SelectedCargadorId = args.Data.CargadorID;
        }

        protected async Task CargadorSave() {
            if (addeditCargador.CargadorID == 0) {
                int Success = await CargadorService.CargadorInsert(addeditCargador.Descripcion);
                if (Success != 0) {
                    //Tax Rate already exists
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Tax Description already exists; it cannot be added again.";
                    Warning.OpenDialog();
                    // Data is left in the dialog so the user can see the problem.
                }
                else {
                    // Clears the dialog and is ready for another entry
                    // User must specifically close or cancel the dialog
                    addeditCargador = new Cargador();
                }
            }
            else {
                // Item is being edited
                int Success = await CargadorService.CargadorUpdate(addeditCargador.Descripcion, SelectedCargadorId);
                if (Success != 0) {
                    //Tax Rate already exists
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Aduana already exists; it cannot be added again.";
                    Warning.OpenDialog();
                }
                else {
                    await this.DialogAddEditCargador.Hide();
                    this.StateHasChanged();
                    addeditCargador = new Cargador();
                    SelectedCargadorId = 0;
                }
            }

            //Always refresh datagrid
            cargador = await CargadorService.CargadorList();
            StateHasChanged();
            txtCargador.FocusAsync();
        }

        private async Task CloseDialog() {
            await this.DialogAddEditCargador.HideAsync();
        }
    }
}
