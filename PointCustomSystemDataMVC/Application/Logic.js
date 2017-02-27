var CustomerViewModel = (function () {
    function CustomerViewModel() {
    }
    return CustomerViewModel;
}());
function initTreatmentReport() {
    $("#TallennaAsiakasNotes").click(function () {
        //alert("Toimii!");
        var treatmentReport = $("#Treatmentreport").val();
        //var assetCode = $("#AssetCode").val();
        alert("T: " + treatmentReport);
        //määritetään muuttuja:
        var data = new CustomerViewModel();
        data.TreatmentReport = treatmentReport;
        //data.AssetCode = assetCode;
        //lähetetään JSON-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Customers/SavedReport",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (data.success == true) {
                    alert("Hoitotiedot tallennettu.");
                }
                else {
                    alert("Virhe tallennuksessa: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}