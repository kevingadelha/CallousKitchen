$("#btnSearchRecipe").on("click", function () {
    console.log("click search");
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