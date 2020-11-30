$("#tbOther").on("input", function () {
    var input = $(this).val();
    if (input.includes("|")) {
        $("#btnSubmit").prop("disabled", true);
    }
    else {
        $("#btnSubmit").prop("disabled", false);
    }
});