

let globalCounter = {
};

$("#button").click(function () {

    $("#loading").attr("hidden", false);

    $("#nav-tab").empty();
    $("#nav-tabContent").empty();

    globalCounter.Jobinja = 1;
    globalCounter.Quera = 1;

    ajax("Jobinja", globalCounter.Jobinja, true);
    ajax("Quera", globalCounter.Quera, false);
})

$("#loadMore").click(function(){

    $("#loadMore").attr("hidden", true);
    $("#loading").attr("hidden", false);

    let website = $(".nav-item").filter(".active").attr("id");

    globalCounter[website] += 1;
    console.log(globalCounter[website]);

    ajax(website, globalCounter[website]);
})

function ajax(website, pageNumber, isActive) {

    let myData = {
        SearchString : $("#searchQuery").val(),
        PageNumber : pageNumber
    };

    return $.ajax({
        type: "GET",
        url: `Home/${website}Result`,
        data: myData,
        dataType: "html",
        success: function(result){
            AddContent(result, website, pageNumber, isActive);
        }
    });

}

function AddContent(result, website, pageNumber, isActive) {

    $("#loading").attr("hidden", true);
    $("#nav-tab").attr("hidden", false);
    if (pageNumber == 1) {
        $("#nav-tab").append($(result).filter(`#${website}`));
        $("#nav-tabContent").append($(result).filter(`#nav-${website}`));
        $("#loadMore").attr("hidden", false);
        if (isActive == true) {
            $(`#${website}`).addClass("active");
            $(`#nav-${website}`).addClass("active");
        }
    } else {
        $(`#nav-${website}`).append($(result).filter(`#nav-${website}`).html());
        $("#loadMore").attr("hidden", false);
    }

}



