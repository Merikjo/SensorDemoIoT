/// <reference path="../typings/jquery/jquery.d.ts" />
var AssignLocationModel = /** @class */ (function () {
    function AssignLocationModel() {
    }
    return AssignLocationModel;
}());
function initAssetAssignment() {
    $("#AssignAssetButton").click(function () {
        //alert("Toimii!");
        var locationCode = $("#LocationCode").val();
        var assetCode = $("#AssetCode").val();
        alert("L: " + locationCode + ", A:" + assetCode);
        //määritetään muuttuja:
        var data = new AssignLocationModel();
        data.LocationCode = locationCode;
        data.AssetCode = assetCode;
        //lähetetään JSON-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Asset/AssignLocation",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (data.success === true) {
                    alert("Asset successfully assigned.");
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