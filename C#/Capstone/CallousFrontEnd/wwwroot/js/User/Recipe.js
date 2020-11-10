$("#btnSearchRecipe").on("click", function () {
    $.ajax({
        type: 'Post',
        url: "SearchRecipes",
        data: {
            "search": $("#tbRecipeSearch").val()
        },
        success: function (result) {
            $("#recipeContainer").html(result);
        }
    });
});

$("#btnFeelingLucky").on("click", function () {
    $.ajax({
        type: 'Post',
        url: "FeelingLucky",
        success: function (result) {
            $("#recipeContainer").html(result);
        }
    });
});