const passResult = $("#passwordResult");
const passBtn = $("#btnPassword");
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
        }
        else {
            passResult.html("");
            passBtn.prop('disabled', false);
        }
    }
    else {
        passResult.html("");
        passBtn.prop('disabled', true);
    }
});

passBtn.on("click", function () {
    const newPass = $("#tbNewPassword").val();
    const oldPass = $("#tbOldPassword").val();
    const id = $("#Id").val();
    $.ajax({
        type: 'Post',
        url: "ChangePassword",
        data: {
            "userId": Number(id),
            "old": oldPass,
            "newPass": newPass
        },
        success: function (result) {
            passResult.html(result);
        }
    });
});