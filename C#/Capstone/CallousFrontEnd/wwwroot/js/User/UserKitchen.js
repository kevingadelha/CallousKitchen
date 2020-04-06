
$(document).ready(function () {
    console.log("Kitchen  A C T I V A T E D");
    $(".addFoodBtn").click(function () {
        var KitchenId = $(this).data("kitchenId");

        console.log("go");
        var KitchenId = $(this).data("kitchenId");
        console.log(KitchenId);
        $.ajax({
            type: 'GET',
            url: "AddEditFoodView",
            data: {
                "kId": KitchenId,
                "fId": 0
            },
            success: function (result) {
                $("#AddEditFoodBody").replaceWith(result);
            }

        });
    });
    
});