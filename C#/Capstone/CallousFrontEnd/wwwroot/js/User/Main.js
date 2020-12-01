$("#btnRecipeModal").on("click", function () {
    console.log("recipe");
    $.ajax({
        type: 'Get',
        url: "RecipeSearchView",
        success: function (result) {
            $("#recipeSearchBody").html(result);
        }
    });
});
$("#btnShoppingListModal").on("click", function () {
    $.ajax({
        type: 'Post',
        url: "ShoppingList",
        success: function (result) {
            $("#shoppingListBody").html(result);
        }
    });
});
