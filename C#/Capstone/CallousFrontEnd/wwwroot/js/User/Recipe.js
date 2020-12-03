const loading = $("#loading");
loading.hide();

function recipeSearch() {
    console.log("new search");
    if ($("#tbRecipeSearch").val()) {
        $.ajax({
            type: 'Post',
            url: "SearchRecipes",
            data: {
                "search": $("#tbRecipeSearch").val()
            },
            beforeSend: function () {
                loading.show();
            },
            success: function (result) {
                $("#recipeContainer").html(result);
                loading.hide();
            }
        });
    }
}

$("#btnSearchRecipe").on("click", function () {
    recipeSearch();
});

$("#btnFeelingLucky").on("click", function () {
    $.ajax({
        type: 'Post',
        url: "FeelingLucky",
        beforeSend: function () {
            loading.show();
        },
        success: function (result) {
            $("#recipeContainer").html(result);
            loading.hide();
        }
    });

});

$("#tbRecipeSearch").on("keydown", function (e) {
    if (e.keyCode == 13) { // if pressed enter
        recipeSearch();
    }
});
