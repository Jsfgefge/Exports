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
    public partial class ConsignatariosPage : ComponentBase {
        [Inject]
        IConsignatariosService ConsignatariosService { get; set; }

        IEnumerable<Consignatarios> consignatario;
        private List<ItemModel> Toolbaritems = new List<ItemModel>();

        SfDialog DialogAddEditConsignatario;
        WarningPage Warning;
        SfTextBox txtConsignatario;

        Consignatarios addeditConsignatario = new Consignatarios();
        public int SelectedConsignatarioId { get; set; } = 0;

        string HeaderText = "";
        string WarningHeaderMessage = "";
        string WarningContentMessage = "";

        protected override async Task OnInitializedAsync() {
            consignatario = await ConsignatariosService.ConsignatariosList();

            Toolbaritems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new Consignatario", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit a Consignatario", PrefixIcon = "e-edit" });
        }
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args) {
            if (args.Item.Text == "Add") {
                addeditConsignatario = new Consignatarios();
                HeaderText = "Add Consignatario";
                await this.DialogAddEditConsignatario.ShowAsync();
            }
            if (args.Item.Text == "Edit") {
                if (SelectedConsignatarioId == 0) {
                    WarningHeaderMessage = "Warning";
                    WarningContentMessage = "Please select a Consignatario from the grid.";
                    Warning.OpenDialog();
                }
                else {
                    HeaderText = "Edit Consignatario";
                    addeditConsignatario = await ConsignatariosService.Consignatarios_GetOne(SelectedConsignatarioId);
                    await this.DialogAddEditConsignatario.ShowAsync();
                }
            }
        }

        public async Task RowSelectHandler(RowSelectEventArgs<Consignatarios> args) {
            SelectedConsignatarioId = args.Data.ConsignatarioID;
        }

        public async Task ConsignatarioSave() {
            if (addeditConsignatario.ConsignatarioID == 0) {
                int Success = await ConsignatariosService.ConsignatariosInsert(addeditConsignatario.NombreConsignatario);
                if (Success != 0) {
                    //Consignatario already exists
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Consignatario already exists; it cannot be added again.";
                    Warning.OpenDialog();
                    // Data is left in the dialog so the user can see the problem.
                }
                else {
                    addeditConsignatario = new Consignatarios();
                }
            }
            else {
                //edit
                int Success = await ConsignatariosService.ConsignatariosUpdate(addeditConsignatario.NombreConsignatario, addeditConsignatario.ConsignatarioID);
                if(Success != 0) {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Consignatario already exists; it cannot be added again.";
                    Warning.OpenDialog();
                }
                else {
                    await this.DialogAddEditConsignatario.HideAsync();
                    this.StateHasChanged();
                    addeditConsignatario = new Consignatarios();
                    SelectedConsignatarioId = 0;
                }
            }
            addeditConsignatario = new Consignatarios();
            consignatario = await ConsignatariosService.ConsignatariosList();
            StateHasChanged();
            txtConsignatario.FocusAsync();
        }
        private async Task CloseDialog() {
            await this.DialogAddEditConsignatario.HideAsync();
        }
    }
}
