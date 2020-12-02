const passResult = $("#passwordResult");
const passBtn = $("#btnPassword");
console.log("settings");
$("#tbOther").on("input", function () {
    var input = $(this).val();
    if (input.includes("|")) {
        $("#btnSubmit").prop("disabled", true);
    }
    else {
        $("#btnSubmit").prop("disabled", false);
    }
});
$(".passChange").on("keyup", function () {
    console.log("first");

    const pass1 = $("#tbNewPassword").val();
    const pass2 = $("#tbConfirmNewPassword").val();
    if (pass1 && pass2) {
        if (pass1 != pass2) {
            passResult.html("Passwords do not match!");
            passBtn.prop('disabled', true);
            console.log("here");
        }
        else {
            passResult.html("");
            passBtn.prop('disabled', false);
            console.log("there");

        }
    }
    else {
        passResult.html("");
        passBtn.prop('disabled', true);
        console.log("where");

    }
});