/// <reference path="../typings/jquery/jquery.d.ts" />
var CustomerViewModel = (function () {
    function CustomerViewModel() {
    }
    return CustomerViewModel;
}());
function initTreatmentReport() {
    $("#TallennaAsiakasNotes").click(function () {
        //alert("Toimii!");
        var customerText = $("#CustomerText").val();
        var treatmentText = $("#TreatmentText").val();
        alert("C: " + customerText + ", T:" + treatmentText);
        //määritetään muuttuja:
        var data = new TreatmentReport();
        data.Customer_id = customerText;
        data.TreatmentText = treatmentText;
        //lähetetään JSON-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Customer/SavedReport",
            data: JSON.stringify(data),
            contentType: "application/json;utf-8",
            success: function (data) {
                if (data.success === true) {
                    alert("Hoitoraportti tallennettu.");
                }
                else {
                    alert("Tallennusvirhe: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}
//# sourceMappingURL=Logic.js.map