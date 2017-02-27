/// <reference path="../typings/jquery/jquery.d.ts" />
var CustomerViewModel = (function () {
    function CustomerViewModel() {
    }
    return CustomerViewModel;
}());
function initTreatmentReport() {
    $("#TallennaAsiakasNotes").click(function () {
        //alert("Toimii!");
        var userIdentity = $("#UserIdentity").val();
        var phoneNum = $("#Phone.PhoneNum_1").val();
        alert("U: " + userIdentity + ", P:" + phoneNum);
        //määritetään muuttuja:
        var data = new Personnel();
        data.UserIdentity = userIdentity;
        data.Phone.PhoneNum_1 = phoneNum;
        //lähetetään JSON-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Personnel/PersonnelInfo",
            data: JSON.stringify(data),
            contentType: "application/json;utf-8",
            success: function (data) {
                if (data.success === true) {
                    alert("Personnel successfully assigned.");
                }
                else {
                    alert("There was an error: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}
//# sourceMappingURL=Logic.js.map