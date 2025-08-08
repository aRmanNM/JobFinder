
let globalCounter = {
};

$("#button").click(function () {

    $("#loading").attr("hidden", false);
    $("#loadMore").attr("hidden", true);
    $("#nav-tab").attr("hidden", true);

    $("#nav-tab").empty();
    $("#nav-tabContent").empty();

    globalCounter.Jobinja = 1;
    globalCounter.Quera = 1;
    globalCounter.Jobvision = 1;

    ajax("Jobinja", globalCounter.Jobinja, true);
    ajax("Quera", globalCounter.Quera, false);
    ajax("Jobvision", globalCounter.Jobvision, false);
})

$("#loadMore").click(function () {

    $("#loadMore").attr("hidden", true);
    $("#loading").attr("hidden", false);

    let website = $(".nav-item").filter(".active").attr("id");

    console.log(globalCounter[website]);

    ajax(website, globalCounter[website]);
})

$(document).on("click", ".getDetailButton", function () {
    let url = $(this).attr("data-url");
    let title = $(this).attr("data-title");
    let website = $(".nav-item").filter(".active").attr("id"); // service name

    $(".modal-button-ad-link").attr("href", url);
    $("#exampleModalLongTitle").text(title);

    $("#loading-modal").attr("hidden", false);
    $("#loading-modal-container").attr("hidden", false);

    // reset state
    $(".modal-body").html(null);

    let myData = new FormData();
    myData.append('url', url);
    myData.append('name', website);

    $.ajax({
        type: "POST",
        url: `Home/AdDetail`,
        data: myData,
        processData: false,
        contentType: false,
        success: function (result) {
            $("#loading-modal").attr("hidden", true);
            $("#loading-modal-container").attr("hidden", true);
            $(".modal-body").html(result);
        }
    })
})

function ajax(website, pageNumber, isActive) {

    let myData = {
        SearchString: $("#searchQuery").val(),
        PageNumber: pageNumber
    };

    return $.ajax({
        type: "GET",
        url: `Home/${website}Result`,
        data: myData,
        dataType: "html",
        success: function (result) {
            AddContent(result, website, pageNumber, isActive);
        },
        error: function () {
            AddContent(null, website, pageNumber, isActive);
        }
    });

}

function AddContent(result, website, pageNumber, isActive) {

    $("#loading").attr("hidden", true);

    if (result == null)
        return;

    if (result.includes("card") || globalCounter[website] == 1)
        globalCounter[website] += 1;

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