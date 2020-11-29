$("#btSearchbar").on("keyup", function () {
    var search = $(this).val().toLowerCase();
    console.log($("#foodRow"));
    $("#foodRow").filter("[data-foodname='Garlic']").css("background-color", "yellow");

});