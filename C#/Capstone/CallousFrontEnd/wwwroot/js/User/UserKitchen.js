
$(document).ready(function () {
    console.log("Kitchen  A C T I V A T E D");
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
        $.ajax({
            type: 'GET',
            url: "EatFoodView",
            data: {
                "fId": FoodId
            },
            success: function (result) {
                $("#EatFoodBody").html(result);
            }

        });
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
                console.log(result);
                $("#Kitchens").html(result);
                console.log("Refresh");
            }

        });
    });
});



