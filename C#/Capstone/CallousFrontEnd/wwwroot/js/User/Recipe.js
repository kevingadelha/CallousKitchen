function recipeSearch() {
    $.ajax({
        type: 'Post',
        url: "SearchRecipes",
        data: {
            "search": $("#tbRecipeSearch").val()
        },
        success: function (result) {
            $("#recipeContainer").html(result);
            console.log("recipe searched");
        }
    });
}

$("#btnSearchRecipe").on("click", function () {
    recipeSearch();
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

$("#tbRecipeSearch").on("keydown", function (e) {
    if (e.keyCode == 13) { // if pressed enter
        recipeSearch();
    }
});
