//Function for Global ajax calls
function fnAjax(Url, methodType, objdata, fnSuccess, fnError, dataType) {
    $.ajax({
        url: Url,
        method: methodType,
        //dataType: (DataType != null ? DataType : "json"),
        contentType: 'application/json',
        dataType: (dataType ? dataType : "json"),
        data: (dataType ? (dataType.toLowerCase() == "json" ? JSON.stringify(objdata) : objdata) : objdata),
        success: function (result) {
            fnSuccess(result);
        },
        error: function (xhr, error, data) {
            function fnError() { }
        }
    });
}
