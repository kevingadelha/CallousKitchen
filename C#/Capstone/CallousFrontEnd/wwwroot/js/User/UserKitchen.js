// Author: Peter Szadurski
$(document).ready(function () {
    console.log("Activated");
    $(".addFoodBtn").click(function () {
        var KitchenId = $(this).data("kitchenId");
        $.ajax({
            type: 'GET',
            url: "AddEditFoodView",
            data: {
                "kId": KitchenId,
                "fId": 0
            },
            success: function (result) {
                $("#AddEditFoodBody").html(result);
            }

        });
    });
    $(".editFoodBtn").click(function () {
        var KitchenId = $(this).data("kitchenId");
        var FoodId = $(this).data("foodId");
        console.log("Edit food");
        $.ajax({
            type: 'GET',
            url: "AddEditFoodView",
            data: {
                "kId": KitchenId,
                "fId": FoodId
            },
            success: function (result) {
                $("#AddEditFoodBody").html(result);
            }

        });
    });
    $(".eatFoodBtn").click(function () {
        var FoodId = $(this).data("foodId");
        var isVegan = $(this).data("vegan");
        var isVeg = $(this).data("veg");
        var userVegan = $("#isVegan").val();
        var userVeg = $("#isVeg").val();

        // hidden inputs are always strings, check string instead of bool

        if ((userVeg == "True" && isVeg != 1) || (userVegan == "True" && isVegan != 1)) {
            var com = false;
            $.ajax({
                type: 'GET',
                url: "EatFoodView",
                data: {
                    "fId": FoodId
                },
                beforeSend: function () {

                    com = (confirm("This food does not match your diet preferences."));
                },
                success: function (result) {
                    if (com) {
                        $('#EatFood').modal('show');
                        $("#EatFoodBody").html(result);
                    }
                }
            });
        }
        else {
            $.ajax({
                type: 'GET',
                url: "EatFoodView",
                data: {
                    "fId": FoodId
                },
                success: function (result) {
                    $('#EatFood').modal('show');
                    $("#EatFoodBody").html(result);

                }
            });
        }
    });
    $(".deleteFoodBtn").click(function () {
        var FoodId = $(this).data("foodId");
        console.log("Delete: " + FoodId);
        $.ajax({
            type: 'Delete',
            url: "DeleteFood",
            data: {
                "fId": FoodId
            },
            success: function (result) {
                $("#Kitchens").html(result);
                console.log("Refresh");
            }
            ,
            complete: function (result) {
                console.log(result);
            }

        });
    });

});

$("#AddFood").on("click", "#btnBarcode", function () {
    var nameTb = $("#Food_Name");
    var barcode = $("#Food_Barcode");

    console.log("barcode: " + barcode.val());
    console.log("barcode: " + $("#Food_Barcode").val());


    $.ajax({
        type: 'Get',
        url: "GetBarcodeData",
        data: {
            "barcode": barcode.val()
        },
        success: function (result) {
            console.log(result);
            nameTb.val(result);
        }
    });

});






$("#tbSearchbar").on("keyup", function () {
    var search = $(this).val().toLowerCase();
    if (search.length !== 0) {
        $(".foodRow").hide();
        $("[data-foodname*=" + search + "]").show();
    }
    else {
        $(".foodRow").show();
    }

});

// sorting stuff
$("#btnName").on("click", function () {
    var fridge = $("#Fridge");
    var cellar = $("#Cellar");
    var pantry = $("#Pantry");
    var cuboard = $("#Cuboard");
    var other = $("#Other");
    var freezer = $("#Freezer");
});

