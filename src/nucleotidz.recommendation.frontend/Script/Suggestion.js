$(document).ready(function () {
    $('#loader-overlay').removeClass('hidden');
    $("#user-email").text(localStorage.getItem("userEmail"));
    var serviceUrl = api + "/Product/suggest?email=" + localStorage.getItem("userEmail") + "";
    $.ajax({
        url: serviceUrl,
        type: "GET",
        crossDomain: true,
        success: function (data) {
            // Loop through each product in the response
            $.each(data, function (index, item) {
                // Create HTML for each product row
                var listItem = `<li class="bg-white p-4 rounded shadow-md">${item}</li>`;
                $('#text-list').append(listItem);
            });

        },
        error: function () {
            alert("Failed to load products from the service.");
        },
        complete: function () {
            // Hide loader after upload completes
            $('#loader-overlay').addClass('hidden');
        }
    });
});